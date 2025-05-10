// using System.ComponentModel;
// using System.ComponentModel.DataAnnotations.Schema;

// public class Simulation
// {
//     public Monde monde { get; private set; }
//     private List<string> plantesPossibles;

//     //Ajout des saisons
//     public Saison saison { get; set; }
//     private bool JeuEncours = true;


//     bool exit = false; // Variable qui permet de quitter le jeu pendant la partie

//     public Simulation(Monde unMonde, List<string> uneListe)
//     {
//         monde = unMonde;
//         plantesPossibles = uneListe;
//         this.saison = new Saison(this.monde);
//     }

//     public void Simuler(Monde monde, int tour)
//     {
//         Random rng = new Random(); int probaAnimal = -1;
//         for (int i = 1; i <= tour; i++)
//         {
//             if(!exit){            
//                 //Console.Clear();  JE ne sais pas pourquoi mon debugeur ne l'apprécie pas
//                 // Météo du jour
//                 // TO DO : proba sur l'ensemble des meteos possibles

//                 saison.DeterminerSaison();
//                 saison.AnnoncerSaison();
//                 Console.WriteLine($"on est en {saison.libelle}");
//                 Thread.Sleep(3000);
//                 saison.meteo.Pleuvoir(); // La météo change selon la saison
//                 saison.meteo.AfficherHumiditeTerrain();
//                 Console.WriteLine("Juste avant je suis censée avoir l'humidité d'indiquée"); // Pour le test
//                 Thread.Sleep(1500);
//                 saison.meteo.DeterminerTemperature();
//                 Console.WriteLine("Juste avant je suis censée avoir la tempéature d'indiquée"); // Pour tester
//                 Thread.Sleep(1500);

//                 // Pour faire des tests initalement
//                 //MeteoHumide meteoHumide = new MeteoHumide(this.monde); // Premier tour : situation de unhanded situation => REVOIR
//                 //meteoHumide?.Pleuvoir();
//                 //meteoHumide?.AfficherHumiditeTerrain();

//                 Console.ForegroundColor = ConsoleColor.Blue;
//                 Console.WriteLine($"\nJour {i}\n");
//                 Console.ForegroundColor = ConsoleColor.White;

//                 // Météo du jour
//                 // TO DO : proba sur l'ensemble des meteos possibles
//                 MeteoHumide meteoHumide = new MeteoHumide(monde);
//                 meteoHumide?.Pleuvoir();
//                 meteoHumide?.AfficherHumiditeTerrain();
                
//                 monde.AfficherGrille();
//                 ProposerActionJoueur();

//                     foreach (var plante in monde.listePlante) // TO DO Vérifier si listePlante est bien mis à jour car les plantes ne poussent plus
//                     {
//                         plante.Croitre(monde); // TO DO: vérifier cette fonction pour analyser le souci
//                         // TO DO : méthode maladie av proba ? Dire quelle plante est malade ? 
//                     }

//                 for (int x = monde.listePlante.Count - 1; x >= 0; x--)
//                 {
//                     if (monde.listePlante[x].estMorte)
//                     {
//                         monde.grillePlante![monde.listePlante[x].xPlante, monde.listePlante[x].yPlante] = null!;
//                         monde.listePlante.RemoveAt(x);
//                     }
//                 }

//                 foreach (var plante in monde.listePlante.ToList())
//                 {
//                     if (!plante.estMorte && plante is PlanteEnvahissante envahissante)
//                     {
//                         envahissante.SePropager(); // La fonction ajoute directement la nouvelle plante à ListePlante
//                     }
//                 }

//                 foreach (var animal in monde.listeAnimal){
//                     animal.SeDeplacerAlea();
//                 }

//                 // Ajout animal : 1 chance sur 2
//                 probaAnimal = rng.Next(2);
//                 if (probaAnimal == 0) monde.AjouterAnimal(monde);
//                 Thread.Sleep(1000);
//             }
//             else
//             {
//                 i = tour + 1; // pour sortir de la boucle
//                 break;
//             }
//             saison.temps++; // Un jour s'est écoulé

//         }

//         // TO DO : méthode fin de partie - récap recolte
//         if (JeuEncours)
//         {
//             monde.AfficherGrille();
//         }
//     }
//     }

//     public void ProposerActionJoueur()
//     {
//         Console.WriteLine("1 - Semer");
//         Console.WriteLine("2 - Faire fuir animal");
//         Console.WriteLine("3 - Arroser les plantes");
//         Console.WriteLine("4 - Passer la journée");
//         Console.WriteLine("5 - Quitter la partie"); 
//         Console.ForegroundColor = ConsoleColor.Blue;
//         Console.Write("Quelle action souhaitez-vous effectuer : ");
//         Console.ForegroundColor = ConsoleColor.White;
        
//         bool entreeValide = false; int[] coordonnees;
//         do
//         {
//             string texte = Console.ReadLine()!;
//             try{
//                 int action = Convert.ToInt32(texte);
//                 if(action >= 1 && action <= 5) // TO DO : Adapter au nb d'actions
//                 {
//                     entreeValide = true;
//                     switch(action)
//                     {
//                         case 1:
//                             ChoisirPlante();
//                             break;
//                         case 2:
//                             coordonnees = ChoisirCoordonnees();
//                             monde.FaireFuirAnimal(coordonnees[0], coordonnees[1]);
//                             break;
//                         case 3:
//                             coordonnees = ChoisirCoordonnees();
//                             monde.ArroserTerrain(coordonnees[0],coordonnees[1]);
//                             break;
//                         case 4:
//                             FinirJeu();
//                             break;
//                         case 5:
//                             exit = true;
//                             break;
//                     }
//                 }
//                 else Console.WriteLine("Veuillez entrer un nombre entre 1 et 5");
//             }
//             catch
//             {
//                 Console.WriteLine("Veuillez entrer un nombre invalide.");
//             }
//         }
//         while (!entreeValide);
//     }

//     public void ChoisirPlante()
//     {
//         Console.WriteLine();
//         for (int j = 0; j < plantesPossibles.Count; j++)
//         {
//             Type type = Type.GetType(plantesPossibles[j])!;                                 // Recuperer le type dans la liste
//             Plante planteTemp = (Plante)Activator.CreateInstance(type,monde,0,0)!;          // Creer plante temporaire
//             Console.WriteLine($"{j + 1}. {plantesPossibles[j]} {planteTemp?.ToString()}");  // Affichage des caractéristiques avec le ToString
//         }
//         bool entreeValide = false; int numPlante = -1;

//         Console.ForegroundColor = ConsoleColor.Blue;
//         Console.Write("Quelle plante souhaitez-vous semer : ");
//         Console.ForegroundColor = ConsoleColor.White;
//         do
//         {
//             string texte = Console.ReadLine()!;
//             try
//             {
//                 numPlante = Convert.ToInt32(texte);
//                 if (numPlante > 0 && numPlante <= plantesPossibles.Count) entreeValide = true;
//             }
//             catch { }
//         }
//         while (!entreeValide);
//         Type typePlante = Type.GetType(plantesPossibles[numPlante - 1])!;
//         int[] coordonnees = ChoisirCoordonnees();

//         Plante nouvellePlante = (Plante)Activator.CreateInstance(typePlante, monde, coordonnees[0], coordonnees[1])!;
//         monde.AjouterPlante(nouvellePlante, coordonnees[0], coordonnees[1]);
//     }

//     public int[] ChoisirCoordonnees()
//     {
//         Console.ForegroundColor = ConsoleColor.Blue;
//         Console.Write("\nNuméro de ligne : ");
//         Console.ForegroundColor = ConsoleColor.White;

//         bool entreeValide = false; int ligne = -1;
//         do
//         {
//             string texte = Console.ReadLine()!;
//             try
//             {
//                 ligne = Convert.ToInt32(texte);
//                 if (ligne > 0 && ligne <= monde.ligne) entreeValide = true;
//             }
//             catch { }
//         }
//         while (!entreeValide);

//         Console.ForegroundColor = ConsoleColor.Blue;
//         Console.Write("Numéro de colonne : ");
//         Console.ForegroundColor = ConsoleColor.White;

//         entreeValide = false; int colonne = -1;
//         do
//         {
//             string texte = Console.ReadLine()!;
//             try
//             {
//                 colonne = Convert.ToInt32(texte);
//                 if (colonne > 0 && colonne <= monde.colonne) entreeValide = true;
//             }
//             catch { }
//         }
//         while (!entreeValide);
//         return [ligne - 1, colonne - 1];
//     }

//     public void FinirJeu()
//     {
//         JeuEncours = false;
//         Console.Clear();
//         Console.Clear();
//         Console.OutputEncoding = System.Text.Encoding.UTF8;

//         string gardenBorder = "🫐🪻🍇🌷🌸🌺🪷🌹🍓🍒🥕🍊🏵️🌻🍋🌼🍏🥬🌵🌳🌲🌱🌿🍃🍂🍁";

//         string[] gardenArt = new string[]
//         {
//             " __  __               _       _ _                  _      ",
//             "|  \\/  | ___ _ __ ___(_)   __| ( ) __ ___   _____ (_)_ __ ",
//             "| |\\/| |/ _ \\ '__/ __| |  / _` |/ / _` \\ \\ / / _ \\| | '__|",
//             "| |  | |  __/ | | (__| | | (_| | | (_| |\\ V / (_) | | |   ",
//             "|_|_ |_|\\___|_|  \\___|_|  \\__,_|  \\__,_| \\_/ \\___/|_|_|   ",
//             "                                      (_) ___  _   _  /_/                                     ",
//             "                                      | |/ _ \\| | | |/ _ \\                                    ",
//             "                                      | | (_) | |_| |  __/                                    ",
//             "                                     _/ |\\___/ \\__,_|\\___|                                    ",
//             "                                    |__/                                                      ",
//         };

//         // Affichage du haut
//         Console.ForegroundColor = ConsoleColor.Green;
//         Visuel.PrintCentered(gardenBorder);
//         Console.WriteLine();

//         // Art du jardin
//         Console.ForegroundColor = ConsoleColor.Yellow;
//         foreach (string line in gardenArt)
//         {
//             Visuel.PrintCentered(line);
//             Thread.Sleep(150);
//         }

//         Console.WriteLine();
//         Console.ForegroundColor = ConsoleColor.White;
//         Visuel.TypewriterCentered("Merci d'avoir joué à l'ENSemenC !");
//         Visuel.TypewriterCentered("Nous espérons que vous avez apprécié cultiver avec nous !");
//         Console.WriteLine();

//         // Affichage du bas
//         Console.ForegroundColor = ConsoleColor.Green;
//         Visuel.PrintCentered(gardenBorder);

//         Console.ResetColor();
//         Thread.Sleep(3000);
//     }
// }

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

public class Simulation
{
    public Monde monde { get; private set; }
    private List<string> plantesPossibles;

    // Ajout des saisons
    public Saison saison { get; set; }

    private bool exit = false; // Variable qui permet de quitter le jeu pendant la partie

    public Simulation(Monde unMonde, List<string> uneListe)
    {
        monde = unMonde;
        plantesPossibles = uneListe;
        this.saison = new Saison(this.monde);
    }

    public void Simuler(Monde monde, int tour)
    {
        Random rng = new Random();
        int probaAnimal = -1;

        for (int i = 1; i <= tour; i++)
        {
            if (!exit)
            {
                // Météo du jour
                // TO DO : proba sur l'ensemble des météos possibles
                saison.DeterminerSaison();
                saison.AnnoncerSaison();
                Console.WriteLine($"on est en {saison.libelle}");
                Thread.Sleep(3000);

                saison.meteo.Pleuvoir(); // La météo change selon la saison
                saison.meteo.AfficherHumiditeTerrain();
                Console.WriteLine("Juste avant je suis censée avoir l'humidité d'indiquée"); // Pour le test
                Thread.Sleep(1500);

                saison.meteo.DeterminerTemperature();
                Console.WriteLine("Juste avant je suis censée avoir la température d'indiquée"); // Pour tester
                Thread.Sleep(1500);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nJour {i}\n");
                Console.ForegroundColor = ConsoleColor.White;

                monde.AfficherGrille();
                ProposerActionJoueur();

                foreach (var plante in monde.listePlante)
                {
                    plante.Croitre(monde);
                    // TO DO : méthode maladie avec proba ? Dire quelle plante est malade ?
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

                // Ajout animal : 1 chance sur 2
                probaAnimal = rng.Next(2);
                if (probaAnimal == 0)
                    monde.AjouterAnimal(monde);

                saison.temps++; // Un jour s'est écoulé
                Thread.Sleep(1000);
            }
            else break;            
        }
        FinirJeu();
    }

    public void ProposerActionJoueur()
    {
        Console.WriteLine("1 - Semer");
        Console.WriteLine("2 - Faire fuir animal");
        Console.WriteLine("3 - Arroser les plantes");
        Console.WriteLine("4 - Passer la journée");
        Console.WriteLine("5 - Quitter la partie"); 

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
                if (action >= 1 && action <= 5) // TO DO : Adapter au nb d'actions
                {
                    entreeValide = true;
                    switch (action)
                    {
                        case 1:
                            ChoisirPlante();
                            break;
                        case 2:
                            coordonnees = ChoisirCoordonnees();
                            monde.FaireFuirAnimal(coordonnees[0], coordonnees[1]);
                            break;
                        case 3:
                            coordonnees = ChoisirCoordonnees();
                            monde.ArroserTerrain(coordonnees[0], coordonnees[1]);
                            break;
                        case 4:
                            // Passer la journée (ne rien faire)
                            break;
                        case 5:
                            FinirJeu();
                            exit = true;
                            break;
                    }
                }
                else Console.WriteLine("Veuillez entrer un nombre entre 1 et 5");
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
            catch{}
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
            catch{}
        }
        while (!entreeValide);
        return [ligne - 1, colonne - 1];
    }

    public void FinirJeu()
    {
        try { Console.Clear(); } catch { }
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string gardenBorder = "🫐🪻🍇🌷🌸🌺🪷🌹🍓🍒🥕🍊🏵️🌻🍋🌼🍏🥬🌵🌳🌲🌱🌿🍃🍂🍁";

        string[] gardenArt = new string[]
        {
            " __  __               _       _ _                  _      ",
            "|  \\/  | ___ _ __ ___(_)   __| ( ) __ ___   _____ (_)_ __ ",
            "| |\\/| |/ _ \\ '__/ __| |  / _` |/ / _` \\ \\ / / _ \\| | '__|",
            "| |  | |  __/ | | (__| | | (_| | | (_| |\\ V / (_) | | |   ",
            "|_|_ |_|\\___|_|  \\___|_|  \\__,_|  \\__,_| \\_/ \\___/|_|_|   ",
            "                                      (_) ___  _   _  /_/                                     ",
            "                                      | |/ _ \\| | | |/ _ \\                                    ",
            "                                      | | (_) | |_| |  __/                                    ",
            "                                     _/ |\\___/ \\__,_|\\___|                                    ",
            "                                    |__/                                                      ",
        };

        // Affichage du haut
        Console.ForegroundColor = ConsoleColor.Green;
        Visuel.PrintCentered(gardenBorder);
        Console.WriteLine();

        // Art du jardin
        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (string line in gardenArt)
        {
            Visuel.PrintCentered(line);
            Thread.Sleep(150);
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Visuel.TypewriterCentered("Merci d'avoir joué à l'ENSemenC !");
        Visuel.TypewriterCentered("Nous espérons que vous avez apprécié cultiver avec nous !");
        Console.WriteLine();

        // Affichage du bas
        Console.ForegroundColor = ConsoleColor.Green;
        Visuel.PrintCentered(gardenBorder);
        Console.ResetColor();
        Thread.Sleep(3000);
    }
}
