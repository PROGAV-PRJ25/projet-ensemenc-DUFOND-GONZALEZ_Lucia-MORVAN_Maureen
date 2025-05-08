using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

public class Simulation
{
    public Monde monde { get; private set; }
    private List<string> plantesPossibles;

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
            //Console.Clear();
            Console.WriteLine($"Jour {i}");
            MeteoHumide meteoHumide = new MeteoHumide(monde);
            meteoHumide?.Pleuvoir();
            meteoHumide?.AfficherHumiditeTerrain();
            monde.AfficherGrille();

            ProposerActionJoueur();
            ChoisirPlante();

            foreach (var plante in monde.listePlante)
            {
                plante.Croitre(monde);
                // TO DO : méthode maladie av proba ? ToString pour dire quelle plante est malade ? 
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

            foreach (var animal in monde.listeAnimal){
                animal.SeDeplacerAlea();
            }
            probaAnimal = rng.Next(2); // 1 chance sur 2 d'ajouter un animal
            if(probaAnimal==0) monde.AjouterAnimal(monde);

        }
        monde.AfficherGrille(); // Affichage de la grille finale
    }

    public void ProposerActionJoueur()
    {
        // TO DO : Proposer la liste d'action au joueur
        Console.Write("\nQuelle action souhaitez-vous effectuer : ");
        // TO DO : récup num avec gestion des exceptions
        int action = Convert.ToInt32(Console.ReadLine()!);
    }

    public void ChoisirPlante()
    {
        for (int j = 0; j < plantesPossibles.Count; j++)
        {
            Console.WriteLine($"{j + 1}. {plantesPossibles[j].ToString()}"); // TO DO : Utiliser ToString
        }
        bool entreeValide = false; int numPlante = -1;

        Console.Write("\nQuelle plante souhaitez-vous semer : ");
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
        bool entreeValide = false; int ligne = -1;
        Console.Write("Numéro de ligne : ");
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

        entreeValide = false; int colonne = -1;
        Console.Write("Numéro de colonne : ");
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
