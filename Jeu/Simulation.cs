public class Simulation
{
    public Monde monde { get; private set; }
    List<PlanteEnvahissante> nouvellesPlantes = new List<PlanteEnvahissante>();
    public static int jourSuivant = 1;


    public Simulation(Monde unMonde)
    {
        this.monde = unMonde;
    }

    public void Simuler(Monde monde)
    {
        Console.Clear();
        Console.WriteLine("Bienvenue dans votre potager !");
        Thread.Sleep(3000);
        Console.Clear();

        int i = 0;
        while (i < 7)
        {
            foreach (Plante plante in monde.listePlante.ToList()) // ToList créé une copie de la liste au moment de l'appel
            {
                if (plante is PlanteEnvahissante planteEnvahissante)
                {
                    plante.SePropager();
                    nouvellesPlantes.Add(planteEnvahissante);
                    // Besoin de faire une autre liste car sinon on a des problèmes de OutOfRange
                }
            }

            foreach (Plante plante in monde.listePlante)
            {
                plante.Croitre();
                if (plante.EtapeCroissance == 4)
                {
                    monde.grille[plante.xPlante, plante.yPlante] = null; // Il faut qu'on puisse mettre des plantes à l'endroit où elles sont mortes.
                }
            }

            Console.Clear();
            Console.WriteLine($"Jour {jourSuivant}");
            jourSuivant ++;
            monde.AfficherGrille();
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("\n\n");
            i++;

        }

    }

}
