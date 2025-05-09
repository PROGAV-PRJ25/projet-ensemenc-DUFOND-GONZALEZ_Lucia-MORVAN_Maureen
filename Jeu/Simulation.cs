using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

public class Simulation
{
    public Monde monde { get; private set; }
    private List<string> plantesPossibles;

    //Ajout des saisons
    public Saison saison { get; set; }



    public Simulation(Monde unMonde, List<string> uneListe)
    {
        monde = unMonde;
        plantesPossibles = uneListe;
    }


    public void Simuler(Monde monde, int tour)
    {
        Random rng = new Random(); int probaAnimal = -1;
        for (int i = 1; i <= tour; i++)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nJour {i}\n");
            Console.ForegroundColor = ConsoleColor.White;

            // Météo du jour
            // TO DO : proba sur l'ensemble des meteos possibles
            Saison saison = new Saison();
            saison.AvancerSaison();
            saison.AnnoncerSaison();
            MeteoHumide meteoHumide = new MeteoHumide(monde);
            meteoHumide?.Pleuvoir();
            meteoHumide?.AfficherHumiditeTerrain();

            monde.AfficherGrille();
            ProposerActionJoueur();

            foreach (var plante in monde.listePlante)
            {
                plante.Croitre(monde);
                // TO DO : méthode maladie av proba ? Dire quelle plante est malade ? 
            }

            for (int x = monde.listePlante.Count - 1; x >= 0; x--)
            {
                if (monde.listePlante[x].estMorte)
                {
                    monde.grillePlante[monde.listePlante[x].xPlante, monde.listePlante[x].yPlante] = null;
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
            if (probaAnimal == 0) monde.AjouterAnimal(monde);
            Thread.Sleep(1000);
        }

        // TO DO : méthode fin de partie - récap recolte
        monde.AfficherGrille();
        Saison.temps++; // Un jour s'est écoulé
    }

    public void ProposerActionJoueur()
    {
        Console.WriteLine("1 - Semer");
        Console.WriteLine("2 - Faire fuir animal");
        Console.WriteLine("3 - Passer la journée");
        Console.WriteLine("4 - Quitter le jeu"); // TO DO
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Quelle action souhaitez-vous effectuer : ");
        Console.ForegroundColor = ConsoleColor.White;

        bool entreeValide = false; int[] coordonnees;
        do
        {
            string texte = Console.ReadLine()!;
            try
            {
                if (Convert.ToInt32(texte) > 0 && Convert.ToInt32(texte) < 5) // TO DO : Adapter au nb d'actions
                {
                    entreeValide = true;
                    int action = Convert.ToInt32(texte);
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
                            break;
                        case 4:
                            break;
                    }
                }
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
            Console.WriteLine($"{j + 1}. {plantesPossibles[j].ToString()}"); // TO DO : Utiliser ToString
        }
        bool entreeValide = false; int numPlante = -1;

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Quelle plante souhaitez-vous semer : ");
        Console.ForegroundColor = ConsoleColor.White;
        do
        {
            string texte = Console.ReadLine()!;
            try
            {
                numPlante = Convert.ToInt32(texte);
                if (numPlante > 0 && numPlante <= plantesPossibles.Count) entreeValide = true;
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
}
