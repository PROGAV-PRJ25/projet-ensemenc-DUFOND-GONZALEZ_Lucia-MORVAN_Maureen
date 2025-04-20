public class Simulation
{
    public Monde monde { get; private set; }
    List<PlanteEnvahissante> nouvellesPlantes = new List<PlanteEnvahissante>();

    public Simulation(Monde unMonde)
    {
        this.monde = unMonde;
    }

    public void Simuler(Monde monde)
    {
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
            monde.listePlante.AddRange(nouvellesPlantes);

            Console.WriteLine("Ligne intermédiaire:");
            monde.AfficherGrille();


            foreach (Plante plante in monde.listePlante)
            {
                plante.Croitre();
            }

            // Problème: l'espèce envahissante évolue deux fois trop vite = la fonction croître se passe deux fois??

            Console.WriteLine("Grille qui apparaîtra effectivement:");
            monde.AfficherGrille();
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("\n\n");
            // Console.Clear();
            i++;

        }

    }

}
