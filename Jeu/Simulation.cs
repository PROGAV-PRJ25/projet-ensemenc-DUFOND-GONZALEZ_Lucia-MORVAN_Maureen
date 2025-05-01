using System.ComponentModel;

public class Simulation
{
    public Monde monde { get; private set; }
    public List<PlanteEnvahissante> nouvellesPlantes = new List<PlanteEnvahissante>();

    public Simulation(Monde unMonde)
    {
        this.monde = unMonde;
    }

    public void Simuler(Monde monde, int tour)
    {
        for(int i = 1; i<=tour; i++)
        {
            Console.Clear();
            Console.WriteLine($"Jour {i}");
            monde.AfficherGrille();
            
            Console.WriteLine("Choisis une action à effectuer");
            // Proposer la liste d'action au joueur
            // Récupérer le numéro qu'il a rentré (si c'est planter => ToString des plantes pour voir conditions de pousse), 
            // Choisir coordonnées, vérifier entreeValides (action, coor) -> exécuter
            // Rhododendron plante2 = new Rhododendron(monde, 1, i);
            // monde.AjouterPlante(plante2, plante2.xPlante, plante2.yPlante);
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("\n\n");
            
            foreach (Plante plante in monde.listePlante.ToList()) // ToList créé une copie de la liste au moment de l'appel
            {
                if (plante is PlanteEnvahissante planteEnvahissante)
                {
                    plante.SePropager();
                    nouvellesPlantes.Add(planteEnvahissante);
                    //monde.listePlante.Add(planteEnvahissante);
                    // Besoin de faire une autre liste car sinon on a des problèmes de OutOfRange
                }
            }

            foreach (Plante plante in monde.listePlante)
            {
                plante.Croitre(monde);
            }
        }
    }
}
