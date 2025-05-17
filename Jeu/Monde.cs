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
    public List<string> plantesPossible;
    public List<string> animauxPossible;
    public List<Terrain> terrainsPossible;
    public int[] recolte = new int[4];

    public Monde(int ligne, int colonne, List<string> plantes, List<Terrain> terrains, List<string> animaux)
    {
        this.ligne = ligne;
        this.colonne = colonne;
        grillePlante = new Plante[ligne, colonne];
        grilleTerrain = new Terrain[ligne, colonne];
        grilleAnimal = new Animal[ligne, colonne];
        plantesPossible = plantes;
        animauxPossible = animaux;
        terrainsPossible = terrains;
        InitialiserTerrain();
    }

    public void InitialiserTerrain()
    {
        for (int i = 0; i < ligne; i++)
        {
            for (int j = 0; j < colonne; j++)
            {
                if (i < (ligne / 2) && j < (colonne / 2))
                    grilleTerrain[i, j] = terrainsPossible[0];
                else if (i < (ligne / 2) && j >= (colonne / 2))
                    grilleTerrain[i, j] = terrainsPossible[1];
                else if (i >= (ligne / 2) && j < (colonne / 2))
                    grilleTerrain[i, j] = terrainsPossible[1];
                else grilleTerrain[i, j] = terrainsPossible[0];
            }
        }
    }

    public void AfficherGrille(Meteo meteo)
    {

        Console.WriteLine($"Jour {Simulation.jour}");
        // Animation pluie
        if (meteo.estEnTrainDePleuvoir) Visuel.AfficherAnimationPluie(); // Déborde un peu sur la droite, dessous l'encadré
        else Visuel.AfficherAnimationSoleil();

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
        // Si la grille a moins de 8 lignes, afficher le reste de la météo
        for (int i = ligne; i < 8; i++)
        {
            Console.Write("   "); // Pour aligner avec la grille vide
            AfficherMeteo(i, meteo);
        }

        Console.WriteLine();

        List<Terrain> terrainsModifiés = new List<Terrain>();
        for (int i = 0; i < ligne; i++) // grilleTerrain comprend des classes Terrains
        {
            for (int j = 0; j < colonne; j++)
            {
                Terrain terrain = grilleTerrain[i, j];

                if (!terrainsModifiés.Contains(terrain))
                {
                    Console.WriteLine(terrain.ToString());
                    terrainsModifiés.Add(terrain);
                }
            }
        }
        Console.WriteLine();
    }

    public void AjouterPlante(Plante plante, int x, int y, bool affichage)
    {
        // On peut planter si il n'y a pas de plante, d'animal ou de tranchée
        if (grillePlante?[x, y] == null && grilleAnimal?[x, y] == null)
        {
            if(grilleTerrain?[x,y].idType <= 4)
            {                
                grillePlante![x, y] = plante;
                listePlante.Add(plante);
                if(affichage){
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nLes graines ont été semés ! ");
                    Console.ForegroundColor = ConsoleColor.White;
                }                
            }
            else
            {
                if(affichage){
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nRaté, vous ne pouvez pas sémer à cet endroit !");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nRaté, la case est déjà occupée !");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public void AjouterAnimal(Saison saison, Monde monde)
    {
        Random rng = new Random();
        int probaAnimal = -1;   // Probabilité différentes selon la saison
        if (saison.libelle == "Printemps") probaAnimal = rng.Next(4);
        else if (saison.libelle == "Ete") probaAnimal = rng.Next(10);
        else if (saison.libelle == "Automne") probaAnimal = rng.Next(4);
        else probaAnimal = rng.Next(10);

        if (probaAnimal == 0)
        {    // Cas où on ajoute un animal
            int x = rng.Next(2); int y = rng.Next(2);
            if (x == 0) x = 0;    // Coin supérieur
            else x = ligne - 1;   // Coin inférieur
            if (y == 0) y = 0;    // Coin gauche
            else y = colonne - 1; // Coin droite

            Type typeAnimal = Type.GetType(animauxPossible[0])!;
            Animal nouvelAnimal = (Animal)Activator.CreateInstance(typeAnimal, monde, x, y)!;
            grilleAnimal[x, y] = nouvelAnimal;
            listeAnimal.Add(nouvelAnimal);

            if (grillePlante?[x, y] != null) Desherber(x, y);   // S'il y avait une plante, on la supprime

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nAttention, un animal s'est faufillé dans votre potager !");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public void Desherber(int x, int y)
    {
        if(grilleTerrain?[x,y].idType <= 4){
            Plante plante = grillePlante![x, y]; // On récupère la plante sur la case
            listePlante?.Remove(plante);         // On supprime la plante de la liste
            grillePlante[x, y] = null!;          // On supprime la plante de la grille
        }        
        else{
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nVous ne pouvez pas désherber à cet endroit !");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public void Recolter(int x, int y)
    {
        if(grilleTerrain?[x,y].idType <= 4) // Si on est pas sur une tranchée ou un epouventail
        {
            Plante plante = grillePlante![x, y]; // On récupère la plante sur la case

            if (plante.EtapeCroissance == 3)     // Si la plante est à sa croissance max
            {
                for (int i = 0; i < plantesPossible.Count; i++)  // Parcourir du tableau (string) sur l'ensemble des plantes possibles
                {
                    Type type = Type.GetType(plantesPossible[i])!;  // Récupération du type de la plante
                    Plante planteTemp = (Plante)Activator.CreateInstance(type, this, 0, 0)!;

                    if (planteTemp.idType == plante.idType)  // Si les plantes ont le meme id alors elles sont du même type
                    {
                        recolte[i] += plante.nbFruit;       // On stocke le nombre de fruit dans la case du tableau adapté
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nSuper, vous avez récolter {plante.nbFruit} {plantesPossible[i]} !");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

                if (plante.esperanceVie > 0)
                {   // Si son esperance de vie est supérieur à 0    
                    plante.EtapeCroissance = 0;
                    plante.esperanceVie--;
                }
                else Desherber(x, y);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nRaté, la plante n'est pas à sa croissance maximale. Elle ne peut donc pas être récoltée...");
                Console.ForegroundColor = ConsoleColor.White;
            }

        }
        else{
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nIl n'y a aucun fruit à récolter à cet endroit");
            Console.ForegroundColor = ConsoleColor.White;
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
                    {   // S'il y a un animal dessus
                        Animal animal = grilleAnimal[(x + i), (y + j)];  // Recupérer l'animal
                        listeAnimal?.Remove(animal);                     // L'enlever de la liste
                        grilleAnimal[(x + i), (y + j)] = null!;          // Le supprimer de la grille
                    }
                }
            }
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nDes animaux ont été chassés du potager !");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void ArroserTerrain(int x, int y) // Arroser les terrains qui sont dans la zone centrée en (x,y) et de rayon 1 => carré de 3*3
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                // Si la case est dans la grille, que ce n'est pas une tranchée et que l'humidite n'est pas au max
                if ((x + i) >= 0 && (x + i) < ligne && (y + j) >= 0 && (y + j) < colonne && grilleTerrain[x+i, y+j] != null && grilleTerrain[x + i, y + j].humidite < 100)
                {
                    grilleTerrain[x + i, y + j].humidite += 10;
                }
            }
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\nLa zone alentour à la case ({x + 1},{y + 1}) a été arrosée ! ");
        Thread.Sleep(1500);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void DeposerEngrais(int x, int y) // Améliorer la fertilité les terrains qui sont dans la zone centrée en (x,y) et de rayon 1 => carré de 3*3
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if ((x + i) >= 0 && (x + i) < ligne && (y + j) >= 0 && (y + j) < colonne && grilleTerrain[x+i, y+j] != null && grilleTerrain[x + i, y + j].fertilite < 100)
                {
                    grilleTerrain[x + i, y + j].fertilite += 10;
                }
            }
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\nL'engrais a été déposé, la fertilite a été améliorée !");
        Thread.Sleep(1500);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void TraiterPlante(int x, int y)
    {
        Plante plante = grillePlante?[x, y]!;
        plante.maladie = false;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\nLa plante a été traité !");
        Thread.Sleep(1500);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void CreuserTranchee(int x, int y)
    {
        if(grilleTerrain?[x,y].idType != 6) // S'il n'y a pas d'épouventail
        {    
            Desherber(x,y);
            grilleTerrain![x,y] = terrainsPossible[2];
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nLa tranchée a été creusé !");
            Thread.Sleep(1500);
            Console.ForegroundColor = ConsoleColor.White;
        }
        else{
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nLa tranchée ne peut pas être creusé ici !");
            Thread.Sleep(1500);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public void InstallerEpouventail(int x, int y)
    {
        if(grilleTerrain?[x,y].idType != 5) // S'il n'y a pas de tranchée
        {            
            Desherber(x,y);
            grilleTerrain![x,y] = terrainsPossible[3];
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nL'épouventail a été installé !");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else{
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nL'épouventail ne peut pas être installé ici !");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    // ************ Code pour tester ****************
    public void AfficherMeteo(int i, Meteo meteo)
    {
        // Ajout d'un encadré pour annoncer la météo sur la droite
        if (i == 0)
            WriteMeteoLine("+--------------------------+");
        else if (i == 1)
            WriteMeteoLine("|        MÉTÉO DU JOUR     |");
        else if (i == 2)
            WriteMeteoLine("+--------------------------+");
        else if (i == 3)
            WriteMeteoLine($"| Température : {meteo.temperature,3}°C      |");
        else if (i == 4)
            WriteMeteoLine($"| Humidité    : {90,3} %      |"); // Remplacer 90 par une variable si nécessaire
        else if (i == 5)
        {
            string pluie = AfficherPresencePluie();
            WriteMeteoLine($"| Pluie       :  {pluie,-3}       |");

            string AfficherPresencePluie()
            {
                return meteo.estEnTrainDePleuvoir ? "oui" : "non";
            }
        }
        else if (i == 6)
            WriteMeteoLine($"| Vent        : {meteo.niveauVent,3} km/h   |");
        else if (i == 7)
            WriteMeteoLine("+--------------------------+");
        else
            Console.WriteLine();
    }


    public void WriteMeteoLine(string line)
    {
        int posX = 50;
        Console.SetCursorPosition(posX, Console.CursorTop);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(line);
        Console.ResetColor();
    }
}