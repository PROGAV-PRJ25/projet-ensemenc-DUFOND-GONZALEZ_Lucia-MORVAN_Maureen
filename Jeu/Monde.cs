using System.Security.Cryptography.X509Certificates;

public class Monde
{
    public Plante[,]? grille;
    public Terrain[,] grilleTerrain;
    public int ligne;
    public int colonne;
    public List<Plante> listePlante = new List<Plante>();
    public int recolte = 0;

    public Monde(int ligne, int colonne, List<Terrain> terrainPossible)
    {
        this.ligne = ligne;
        this.colonne = colonne;
        grille = new Plante[ligne, colonne];
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
        for (int i = 0; i < ligne; i++)
        {
            for (int j = 0; j < colonne; j++)
            {
                if (grille?[i, j] != null)
                    Console.Write(grille[i, j].AfficherVisuel());
                else
                    Console.Write(grilleTerrain[i, j].AfficherVisuel());
            }
            Console.WriteLine();
        }
    }

    public void AjouterPlante(Plante plante, int x, int y)
    {
        if (x >= 0 && x < ligne && y >= 0 && y < colonne)
        {
            if (grille?[x, y] == null) // J'ai modifié par égal car on suppose que si ça vaut null alors une plante peut y pousser
            {
                grille[x, y] = plante;
                listePlante.Add(plante);
            }
        }
    }

    public void Recolter(int x, int y)
    {
        if (x >= 0 && x < ligne && y >= 0 && y < colonne)
        {
            if (grille?[x, y] != null)        // Si la case n'est pas null
            {                
                Plante plante = grille[x, y]; // On récupère la plante sur la case
                recolte += plante.nbFruit;
                grille[x, y] = null;          // On supprime la plante de la grille
                listePlante?.Remove(plante);  // On supprimer la plante de la liste
            }
        }
    }
}