using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

public class Simulation
{
    public Monde monde { get; private set; }
    private List<string> plantesPossibles;

    // Ajout des saisons
    public Saison saison { get; set; }
    private bool exit = false; // Variable qui permet de quitter le jeu pendant la partie
    public static bool peutSemer;

    public Simulation(Monde unMonde, List<string> uneListe)
    {
        monde = unMonde;
        plantesPossibles = uneListe;
        this.saison = new Saison(this.monde);
    }

    public void Simuler(Monde monde, int tour)
    {
        for (int i = 1; i <= tour; i++)
        {
            if (!exit)
            {
                // Météo du jour
                // TO DO : proba sur l'ensemble des météos possibles
                saison.DeterminerSaison();
                saison.AnnoncerSaison();

                saison.meteo.Pleuvoir(); // La météo change selon la saison
                saison.meteo.AfficherHumiditeTerrain();

                saison.meteo.DeterminerVariables();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nJour {i}\n");
                Console.ForegroundColor = ConsoleColor.White;

                monde.AfficherGrille(saison.meteo);
                ProposerActionJoueur();

                foreach (var plante in monde.listePlante)
                {
                    plante.Croitre(monde, saison.meteo);
                    InitierMaladie(saison, plante);
                }

                for (int x = monde.listePlante.Count - 1; x >= 0; x--)
                {
                    if (monde.listePlante[x].estMorte)
                    {
                        monde.grillePlante![monde.listePlante[x].xPlante, monde.listePlante[x].yPlante] = null!;
                        monde.listePlante.RemoveAt(x);
                    }
                }

                foreach (var plante in monde.listePlante.ToList())
                {
                    if (!plante.estMorte && plante is PlanteEnvahissante envahissante)
                    {
                        envahissante.SePropager(); // La fonction ajoute directement la nouvelle plante à ListePlante
                    }
                }

                foreach (var animal in monde.listeAnimal)
                {
                    animal.SeDeplacerAlea();
                }
                monde.AjouterAnimal(saison, monde);

                saison.temps++; // Un jour s'est écoulé
                Thread.Sleep(1000);
            }
            else break;
        }
        FinirPartie();
    }

    public void ProposerActionJoueur()
    {
        Console.WriteLine("1 - Semer"); 
        Console.WriteLine("2 - Arroser");
        Console.WriteLine("3 - Mettre de l'engrais");
        Console.WriteLine("4 - Deherber");
        Console.WriteLine("5 - Traiter");
        Console.WriteLine("6 - Recolter");
        Console.WriteLine("7 - Faire fuir animal");

        Console.WriteLine("\n8 - Passer la journée");
        Console.WriteLine("9 - Quitter la partie");

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Quelle action souhaitez-vous effectuer : ");
        Console.ForegroundColor = ConsoleColor.White;

        bool entreeValide = false;
        int[] coordonnees;

        do
        {
            string texte = Console.ReadLine()!;
            try
            {
                int action = Convert.ToInt32(texte);
                if (action >= 1 && action <= 8)
                {
                    entreeValide = true;
                    switch (action)
                    {
                        case 1:
                            ChoisirPlante();
                            break;
                        case 2:
                            coordonnees = ChoisirCoordonnees();
                            monde.ArroserTerrain(coordonnees[0], coordonnees[1]);
                            break;
                        case 3:
                            coordonnees = ChoisirCoordonnees();
                            monde.DeposerEngrais(coordonnees[0], coordonnees[1]);
                            break;
                        case 4:
                            coordonnees = ChoisirCoordonnees();
                            monde.Desherber(coordonnees[0], coordonnees[1]);
                            break;
                        case 5: 
                            do{
                                coordonnees = ChoisirCoordonnees();
                            } 
                            while(monde.grillePlante![coordonnees[0], coordonnees[1]] == null);
                            monde.TraiterPlante(coordonnees[0], coordonnees[1]);
                            break;
                        case 6:
                            do{
                                coordonnees = ChoisirCoordonnees();
                            } 
                            while(monde.grillePlante![coordonnees[0], coordonnees[1]] == null);
                            monde.Recolter(coordonnees[0], coordonnees[1]);
                            break;
                        case 7:
                            coordonnees = ChoisirCoordonnees();
                            monde.FaireFuirAnimal(coordonnees[0], coordonnees[1]);
                            break;
                        case 8:
                            // Passer la journée (ne rien faire)
                            break;
                        case 9:
                            FinirPartie();
                            exit = true;
                            break;
                    }
                }
                else Console.WriteLine("Veuillez entrer un nombre entre 1 et 8");
            }
            catch
            {
                Console.WriteLine("Veuillez entrer un nombre entier valide.");
            }
        }
        while (!entreeValide);
    }

    public void ChoisirPlante()
    {
        Console.WriteLine();
        for (int j = 0; j < plantesPossibles.Count; j++)
        {
            Type type = Type.GetType(plantesPossibles[j])!;                                 // Récupérer le type dans la liste
            Plante planteTemp = (Plante)Activator.CreateInstance(type, monde, 0, 0)!;       // Créer plante temporaire
            Console.WriteLine($"{j + 1}. {plantesPossibles[j]} {planteTemp.ToString()}");   // Affichage des caractéristiques avec le ToString
        }

        bool entreeValide = false;
        int numPlante = -1;

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Quelle plante souhaitez-vous semer : ");
        Console.ForegroundColor = ConsoleColor.White;

        do
        {
            string texte = Console.ReadLine()!;
            try
            {
                numPlante = Convert.ToInt32(texte);
                if (numPlante > 0 && numPlante <= plantesPossibles.Count)
                    entreeValide = true;
            }
            catch { }
        }
        while (!entreeValide);

        Type typePlante = Type.GetType(plantesPossibles[numPlante - 1])!;
        do
        {
            peutSemer = true;
            int[] coordonnees = ChoisirCoordonnees();
            Plante nouvellePlante = (Plante)Activator.CreateInstance(typePlante, monde, coordonnees[0], coordonnees[1])!;
            monde.AjouterPlante(nouvellePlante, coordonnees[0], coordonnees[1]);
            if (nouvellePlante.estMorte)
            {
                Console.WriteLine("Votre plante ne peut pas pousser dans ces conditions!\nRéalisez une autre action");
                peutSemer = false;
            }
        } while (!peutSemer);
    }

    public int[] ChoisirCoordonnees()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("\nNuméro de ligne : ");
        Console.ForegroundColor = ConsoleColor.White;

        bool entreeValide = false; int ligne = -1;
        do
        {
            string texte = Console.ReadLine()!;
            try
            {
                ligne = Convert.ToInt32(texte);
                if (ligne > 0 && ligne <= monde.ligne) entreeValide = true;
            }
            catch { }
        }
        while (!entreeValide);

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Numéro de colonne : ");
        Console.ForegroundColor = ConsoleColor.White;

        entreeValide = false; int colonne = -1;
        do
        {
            string texte = Console.ReadLine()!;
            try
            {
                colonne = Convert.ToInt32(texte);
                if (colonne > 0 && colonne <= monde.colonne) entreeValide = true;
            }
            catch { }
        }
        while (!entreeValide);


        return [ligne - 1, colonne - 1];
    }

    public void FinirPartie()
    {
        Console.Clear();
        //Visuel.AnnoncerFinDuJeu(); // Permet de récupérer l'animation de fin (à décommenter à la fin)

    }

    public void InitierMaladie(Saison saison, Plante plante)
    {
        Random rng = new Random(); int probaMaladie = -1;
        if(saison.libelle == "Printemps") probaMaladie = rng.Next(10);
        else if(saison.libelle == "Ete") probaMaladie = rng.Next(6);
        else if(saison.libelle == "Automne") probaMaladie = rng.Next(10);
        else probaMaladie = rng.Next(4);

        if(probaMaladie == 0){            
            plante.maladie = true;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"La plante ({plante.xPlante+1},{plante.yPlante+1}) est malade...");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
