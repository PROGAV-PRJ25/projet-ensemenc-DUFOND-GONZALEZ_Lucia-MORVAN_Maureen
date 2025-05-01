public class Rhododendron : PlanteEnvahissante
{
    public Rhododendron(Monde monde, int x, int y) : base(monde, x, y)
    {
        this.especeEnvahissante = true; // Il s'agit d'une espèce envahissante pour l'Irlande
        this.quantiteEau = 80;
        this.tauxLuminosite = 40;
        this.terrainPrefere = 2; // TerrainHumide
        this.esperanceVie = 10;

        // Visuel spécifique à une fleur (c'est mims)
        this.visuelPlante = new string [] { "🌱", "🌸", "🌺", "💀" };
    }

    public override void SePropager()
    {
        // Pas sur que ce soit nécessaire sachant que la fonction est déjà défini dans plante envahissante 
        
        // if (EtapeCroissance >= 2 && EtapeCroissance < 4)
        // {
        //     int nouvelleLigne = xPlante + 1;
        //     int nouvelleColonne = yPlante;
        //     if (nouvelleLigne >= 0 && nouvelleLigne < monde.ligne && nouvelleColonne >= 0 && nouvelleColonne < monde.ligne)
        //     {
        //         if (monde.grille?[nouvelleLigne, nouvelleColonne] == null)
        //         {
        //             Rhododendron planteBis = new Rhododendron(monde, nouvelleLigne, nouvelleColonne );
        //             monde.AjouterPlante(planteBis, nouvelleLigne, nouvelleColonne);
        //             //monde.grille[nouvelleLigne, nouvelleColonne] = new Rhododendron(monde, nouvelleLigne, nouvelleColonne);
                    
        //         }
        //     }
        // }
    }
}
