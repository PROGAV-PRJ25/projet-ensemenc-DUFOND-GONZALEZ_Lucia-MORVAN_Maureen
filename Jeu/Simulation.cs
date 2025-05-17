using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

public class Simulation
{
    public Monde monde { get; private set; }
    // Ajout des saisons
    public Saison saison { get; set; }
    private bool exit = false; // Variable qui permet de quitter le jeu pendant la partie
    public Simulation(Monde unMonde)
    {
        monde = unMonde;
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


                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nJour {i}\n");
                Console.ForegroundColor = ConsoleColor.White;

                saison.meteo.AfficherHumiditeTerrain();
                // saison.meteo.DeterminerVariables();
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


                if (!exit)
                {
                    Console.WriteLine("\nAppuyer sur une Entree pour continuer");
                    Console.ReadLine();
                }
            }
        }
        FinirPartie();
    }

    public void ProposerActionJoueur()
    {
        List<string> actions = new List<string>
        {
            "1 - Semer",
            "2 - Arroser",
            "3 - Mettre de l'engrais",
            "4 - Deherber",
            "5 - Traiter",
            "6 - Recolter",
            "7 - Faire fuir animal",
            "",
            "8 - Passer la journée",
            "9 - Quitter la partie"
        };

        foreach (string action in actions)
        {
            Console.WriteLine(action);
        }

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Quelle action souhaitez-vous effectuer ? ");
        Console.ForegroundColor = ConsoleColor.White;

        bool entreeValide = false;
        int[] coordonnees;

        do
        {
            string texte = Console.ReadLine()!;
            try
            {
                int action = Convert.ToInt32(texte);
                if (action >= 1 && action <= 9)
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
                            Console.WriteLine("Liste des plantes malades :");
                            int cpt = 0;
                            foreach (var plante in monde.listePlante)
                            {
                                if (plante.maladie)
                                {
                                    Console.WriteLine($"- ({plante.xPlante + 1},{plante.yPlante + 1})");
                                    cpt++;
                                }
                            }
                            if (cpt > 0)
                            {
                                do
                                {
                                    coordonnees = ChoisirCoordonnees();
                                }
                                while (monde.grillePlante![coordonnees[0], coordonnees[1]] == null);
                                monde.TraiterPlante(coordonnees[0], coordonnees[1]);
                            }
                            break;
                        case 6:
                            do
                            {
                                coordonnees = ChoisirCoordonnees();
                            }
                            while (monde.grillePlante![coordonnees[0], coordonnees[1]] == null);
                            monde.Recolter(coordonnees[0], coordonnees[1]);
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            AfficherRecolte();
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case 7:
                            coordonnees = ChoisirCoordonnees();
                            monde.FaireFuirAnimal(coordonnees[0], coordonnees[1]);
                            break;
                        case 8:
                            // Passer la journée (ne rien faire)
                            break;
                        case 9:
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
        for (int j = 0; j < monde.plantesPossible.Count; j++)
        {
            Type type = Type.GetType(monde.plantesPossible[j])!;                                 // Récupérer le type dans la liste
            Plante planteTemp = (Plante)Activator.CreateInstance(type, monde, 0, 0)!;            // Créer plante temporaire
            Console.WriteLine($"{j + 1}. {monde.plantesPossible[j]} {planteTemp.ToString()}");   // Affichage des caractéristiques avec le ToString
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
                if (numPlante > 0 && numPlante <= monde.plantesPossible.Count)
                    entreeValide = true;
            }
            catch { }
        }
        while (!entreeValide);

        Type typePlante = Type.GetType(monde.plantesPossible[numPlante-1])!;
        int[] coordonnees = ChoisirCoordonnees();

        Plante nouvellePlante = (Plante)Activator.CreateInstance(typePlante, monde, coordonnees[0], coordonnees[1])!;
        monde.AjouterPlante(nouvellePlante, coordonnees[0], coordonnees[1]);
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

    public void InitierMaladie(Saison saison, Plante plante)
    {
        Random rng = new Random(); int probaMaladie = -1;
        if (saison.libelle == "Printemps") probaMaladie = rng.Next(12);
        else if (saison.libelle == "Ete") probaMaladie = rng.Next(6);
        else if (saison.libelle == "Automne") probaMaladie = rng.Next(12);
        else probaMaladie = rng.Next(6);

        if (probaMaladie == 0)
        {
            plante.maladie = true;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"La plante ({plante.xPlante + 1},{plante.yPlante + 1}) est malade...");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public void AfficherRecolte()
    {
        Console.WriteLine("\n🌾 RÉCAPITULATIF DES RÉCOLTES 🌾\n");

        Console.WriteLine("+---------------------------+-------------+");
        Console.WriteLine("| Plante                                  |");
        Console.WriteLine("+---------------------------+-------------+");

        for (int i = 0; i < monde.plantesPossible.Count; i++)
        {
            string nom = monde.plantesPossible[i];
            int quantite = monde.recolte[i];
            Console.WriteLine($"| {nom.PadRight(25)} | {quantite.ToString().PadLeft(11)} |");
        }
        Console.WriteLine("+---------------------------+-------------+");
    }

    public void FinirPartie()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("Vous êtes arrivé à la fin de la partie.");
        AfficherRecolte();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\nAppuyer sur une Entree pour continuer");
        Console.ReadLine();
    }
}
