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
        FinirPartie();
    }

    public void ProposerActionJoueur()
    {
        Console.WriteLine("1 - Semer");
        Console.WriteLine("2 - Faire fuir animal");
        Console.WriteLine("3 - Arroser les plantes");
        Console.WriteLine("4 - Mettre de l'engrais");
        Console.WriteLine("5 - Deherber");
        Console.WriteLine("\n6 - Passer la journée");
        Console.WriteLine("7 - Quitter la partie"); 

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
                if (action >= 1 && action <= 7)
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
                            coordonnees = ChoisirCoordonnees();
                            monde.DeposerEngrais(coordonnees[0], coordonnees[1]);
                            break;
                        case 5 :
                            coordonnees = ChoisirCoordonnees();
                            monde.Deherber(coordonnees[0], coordonnees[1]);
                            break;
                        case 6:
                            // Passer la journée (ne rien faire)
                            break;
                        case 7:
                            FinirPartie();
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

    public void FinirPartie()
    {
        // TO DO : inventaire des récoltes, état final du monde
    }
}