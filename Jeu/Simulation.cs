using System.ComponentModel;

public class Simulation
{
    public Monde monde { get; private set; }
    private List<Plante> plantesPossibles;
    public Simulation(Monde unMonde, List<Plante> uneListe)
    {
        monde = unMonde;
        plantesPossibles = uneListe;        
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
            if(i==1){
                Rhododendron plante2 = new Rhododendron(monde, 3,5);
                monde.AjouterPlante(plante2, plante2.xPlante, plante2.yPlante);
                Cerise plante3 = new Cerise(monde,1,1);
                monde.AjouterPlante(plante3, plante3.xPlante, plante3.yPlante);
                Sapin plante4 = new Sapin(monde,9,6);
                monde.AjouterPlante(plante4, plante4.xPlante, plante4.yPlante);
            }
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("\n\n");
            
            foreach (var plante in monde.listePlante){
                plante.Croitre(monde);
                // pourcentage maladie ? ToString pour dire quelle plante est malade ?
            }

            for (int x = monde.listePlante.Count - 1; x >= 0; x--){
                if(monde.listePlante[x].estMorte) 
                {
                    monde.grille[monde.listePlante[x].xPlante, monde.listePlante[x].yPlante] = null;
                    monde.listePlante.RemoveAt(x);
                }
            }

            foreach (var plante in monde.listePlante.ToList()){
                if (!plante.estMorte && plante is PlanteEnvahissante envahissante)
                {
                    envahissante.SePropager(); // La fonction ajoute directement la nouvelle plante à ListePlante
                }
            }
        }
    }
}
