using System.Security.Cryptography.X509Certificates;

public class Monde
{
    public Plante[,]? grillePlante;
    public Terrain[,] grilleTerrain;
    public int ligne;
    public int colonne;
    public List<Plante> listePlante = new List<Plante>();
    public int recolte = 0;

    public Monde(int ligne, int colonne, List<Terrain> terrainPossible)
    {
        this.ligne = ligne;
        this.colonne = colonne;
        grillePlante = new Plante[ligne, colonne];
        grilleTerrain = new Terrain[ligne, colonne];
        InitialiserTerrain(terrainPossible);
    }

    public void InitialiserTerrain(List<Terrain> terrains)
    {
        for (int i = 0; i < ligne; i++)
        {
            for (int j = 0; j < colonne; j++)
            {
                if (i < (ligne/2) && j < (colonne/2))
                    grilleTerrain[i,j] = terrains[0];
                else if (i < (ligne/2) && j >= (colonne/2))
                    grilleTerrain[i,j] = terrains[1];
                else if (i >= (ligne/2) && j < (colonne/2))
                    grilleTerrain[i,j] = terrains[1];
                else grilleTerrain[i,j] = terrains[0];
            }
        }
    }

    public void AfficherGrille()
    {
        // Affichage numéro de colonnes
        Console.Write($"\n   ");
        for (int i = 1; i < colonne+1; i++)
        {
            if(i<10) Console.Write($" {i}");
            else Console.Write($"{i}");
        }
        Console.WriteLine();
        
        for (int i = 0; i < ligne; i++)
        {
            // Affichage des numéros de lignes 
            if(i<9) Console.Write($" {i+1} ");
            else Console.Write($"{i+1} ");

            for (int j = 0; j < colonne; j++)
            {
                if (grillePlante?[i, j] != null)
                    Console.Write(grillePlante[i, j].AfficherVisuel());
                else
                    Console.Write(grilleTerrain[i, j].visuelTerrain);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public void AjouterPlante(Plante plante, int x, int y)
    {
        if (x >= 0 && x < ligne && y >= 0 && y < colonne)
        {
            if (grillePlante?[x, y] == null) // On suppose que si ça vaut null alors une plante peut y pousser
            {
                grillePlante[x, y] = plante;
                listePlante.Add(plante);
            }
            else{
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Raté, une plante pousse déjà à cet endroit !");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }

    public void Recolter(int x, int y)
    {
        if (x >= 0 && x < ligne && y >= 0 && y < colonne)
        {
            if (grillePlante?[x, y] != null)          // Si la case n'est pas null
            {                
                Plante plante = grillePlante[x, y];   // On récupère la plante sur la case
                if(plante.EtapeCroissance == 2){ // Si la plante est à sa croissance max
                    recolte += plante.nbFruit;
                }                
                if(plante.esperanceVie > 0){    // Si son esperance de vie est supérieur à 0
                    plante.EtapeCroissance = 0;
                    plante.esperanceVie --;
                }
                else{
                    grillePlante[x, y] = null;          // On supprime la plante de la grille
                    listePlante?.Remove(plante);  // On supprime la plante de la liste
                }
            }
        }
    }
}