using System.Drawing;
using System.Security.Cryptography.X509Certificates;

public class Monde
{
    public Plante[,]? grillePlante;
    public Terrain[,] grilleTerrain;
    public Animal[,] grilleAnimal;
    public int ligne;
    public int colonne;
    public List<Plante> listePlante = new List<Plante>();
    public List<Animal> listeAnimal = new List<Animal>();
    public List<string> animauxPossible;
    public int recolte = 0;

    public Monde(int ligne, int colonne, List<Terrain> terrainPossible, List<string> animaux)
    {
        this.ligne = ligne;
        this.colonne = colonne;
        grillePlante = new Plante[ligne, colonne];
        grilleTerrain = new Terrain[ligne, colonne];
        grilleAnimal = new Animal[ligne, colonne];
        animauxPossible = animaux;
        InitialiserTerrain(terrainPossible);
    }

    public void InitialiserTerrain(List<Terrain> terrains)
    {
        for (int i = 0; i < ligne; i++)
        {
            for (int j = 0; j < colonne; j++)
            {
                if (i < (ligne / 2) && j < (colonne / 2))
                    grilleTerrain[i, j] = terrains[0];
                else if (i < (ligne / 2) && j >= (colonne / 2))
                    grilleTerrain[i, j] = terrains[1];
                else if (i >= (ligne / 2) && j < (colonne / 2))
                    grilleTerrain[i, j] = terrains[1];
                else grilleTerrain[i, j] = terrains[0];
            }
        }
    }

    public void AfficherGrille(Meteo meteo)
    {
        // Animation pluie
        if (meteo.estEnTrainDePleuvoir)
        {
            Visuel.AfficherAnimationPluie(); // Déborde un peu sur la droite, dessous l'encadré
        }
        else
        {
            Visuel.AfficherAnimationSoleil();
        }


        // Affichage numéro de colonnes
        Console.Write($"\n   ");
        for (int i = 1; i < colonne + 1; i++)
        {
            if (i < 10) Console.Write($" {i}");
            else Console.Write($"{i}");
        }
        Console.WriteLine();


        for (int i = 0; i < ligne; i++)
        {
            // Affichage des numéros de lignes 
            if (i < 9) Console.Write($" {i + 1} ");
            else Console.Write($"{i + 1} ");

            for (int j = 0; j < colonne; j++)
            {
                if (grillePlante?[i, j] != null)
                    Console.Write(grillePlante[i, j].AfficherVisuel());
                else if (grilleAnimal?[i, j] != null)
                    Console.Write(grilleAnimal[i, j].visuelAnimal);
                else
                    Console.Write(grilleTerrain[i, j].visuelTerrain);
            }
            //Console.WriteLine();
            AfficherMeteo(i, meteo);
        }
        Console.WriteLine();
    }

    public void AjouterPlante(Plante plante, int x, int y)
    {
        if (x >= 0 && x < ligne && y >= 0 && y < colonne)
        {
            if (grillePlante?[x, y] == null && grilleAnimal?[x, y] == null) // On suppose que si ça vaut null alors une plante peut y pousser
            {
                grillePlante![x, y] = plante;
                listePlante.Add(plante);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Raté, la case est déjà occupée !");
                Simulation.peutSemer = false;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }

    public void AjouterAnimal(Monde monde)
    {
        Random rng = new Random();
        int x = rng.Next(2); int y = rng.Next(2);
        if (x == 0) x = 0;   // Coin supérieur
        else x = ligne - 1;   // Coin inférieur
        if (y == 0) y = 0;   // Coin gauche
        else y = colonne - 1; // Coin droite

        Type typeAnimal = Type.GetType(animauxPossible[0])!;
        Animal nouvelAnimal = (Animal)Activator.CreateInstance(typeAnimal, monde, x, y)!;
        grilleAnimal[x, y] = nouvelAnimal;
        listeAnimal.Add(nouvelAnimal);
        if (grillePlante?[x, y] != null) Desherber(x, y);
    }

    public void Desherber(int x, int y)
    {
        Plante plante = grillePlante![x, y]; // On récupère la plante sur la case
        listePlante?.Remove(plante);         // On supprime la plante de la liste
        grillePlante[x, y] = null!;           // On supprime la plante de la grille
    }

    public void Recolter(int x, int y)
    {
        if (x >= 0 && x < ligne && y >= 0 && y < colonne)
        {
            if (grillePlante?[x, y] != null)          // Si la case n'est pas null
            {
                Plante plante = grillePlante[x, y];   // On récupère la plante sur la case
                if (plante.EtapeCroissance == 2)
                {      // Si la plante est à sa croissance max
                    recolte += plante.nbFruit;
                }
                if (plante.esperanceVie > 0)
                {          // Si son esperance de vie est supérieur à 0
                    plante.EtapeCroissance = 0;
                    plante.esperanceVie--;
                }
                else
                {
                    Desherber(x, y);
                }
            }
        }
    }

    public void FaireFuirAnimal(int x, int y) // Fait fuir les animaux dans une zone centrée en (x,y) et de rayon 1 => carré de 3*3
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if ((x + i) >= 0 && (x + i) < ligne && (y + j) >= 0 && (y + j) < colonne)    // Si la case est dans la grille
                {
                    if (grilleAnimal[x + i, y + j] != null)
                    {              // S'il y a un animal dessus
                        Animal animal = grilleAnimal[(x + i), (y + j)];  // Recupérer l'animal
                        listeAnimal?.Remove(animal);                // L'enlever de la liste
                        grilleAnimal[(x + i), (y + j)] = null!;          // Le supprimer de la grille
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Les animaux présent dans cette zone vont être chassés !");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
    }

    public void ArroserTerrain(int x, int y) // Arrose les terrains qui sont dans la zone centrée en (x,y) et de rayon 1 => carré de 3*3
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if ((x + i) >= 0 && (x + i) < ligne && (y + j) >= 0 && (y + j) < colonne && grilleTerrain[x + i, y + j].humidite < 100)   // Si la case est dans la grille et que l'humidite
                {
                    grilleTerrain[x + i, y + j].humidite += 10;
                }
            }
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"La zone alentour à la case ({x + 1},{y + 1}) a été arrosée ! ");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void DeposerEngrais(int x, int y) // Améliore la fertilité les terrains qui sont dans la zone centrée en (x,y) et de rayon 1 => carré de 3*3
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if ((x + i) >= 0 && (x + i) < ligne && (y + j) >= 0 && (y + j) < colonne && grilleTerrain[x + i, y + j].fertilite < 100)   // Si la case est dans la grille et que l'humidite
                {
                    grilleTerrain[x + i, y + j].fertilite += 10;
                }
            }
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"L'engrais a été déposé, la fertilite a été améliorée !");
        Console.ForegroundColor = ConsoleColor.White;
    }



    // ************ Code pour tester ****************
    public void AfficherMeteo(int i, Meteo meteo)
    {


        // Ajout d'un encadré pour annoncer la météo sur la droite
        if (i == 0)
            WriteMeteoLine("+----------------------+");
        else if (i == 1)
            WriteMeteoLine("|      MÉTÉO DU JOUR   |");
        else if (i == 2)
            WriteMeteoLine("+----------------------+");
        else if (i == 3)
            WriteMeteoLine($"| Température : {meteo.temperature}°C   |");
        else if (i == 4)
            WriteMeteoLine($"| Humidité    : 90 %   |");
        else if (i == 5)
            WriteMeteoLine($"| Pluie       : {meteo.estEnTrainDePleuvoir}  |");
        else if (i == 6)
            WriteMeteoLine($"| Vent        : Nord   |"); // à faire
        else if (i == 7)
            WriteMeteoLine("+----------------------+");
        else
            Console.WriteLine(); // Ligne normale
    }

    public void WriteMeteoLine(string line)
    {
        int posX = 50; // Décalage horizontal à ajuster selon ta grille
        Console.SetCursorPosition(posX, Console.CursorTop);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(line);
        Console.ResetColor();
    }


}