public class PlanteEnvahissante : Plante
{
    public PlanteEnvahissante(Monde monde, int x, int y) : base(monde, x, y)
    {
        this.especeEnvahissante = true; // Il s'agit d'une espèce envahissante pour l'Irlande
    }

    public override void SePropager()
    {
        if (EtapeCroissance >= 2 && EtapeCroissance < 4)
        {
            int nouvelleLigne = xPlante + 1;
            int nouvelleColonne = yPlante;
            if (nouvelleLigne >= 0 && nouvelleLigne < monde.ligne && nouvelleColonne >= 0 && nouvelleColonne < monde.ligne)
            {
                if (monde.grille?[nouvelleLigne, nouvelleColonne] == null)
                {
                    PlanteEnvahissante planteBis = new PlanteEnvahissante(monde, nouvelleLigne, nouvelleColonne );
                    monde.AjouterPlante(planteBis, nouvelleLigne, nouvelleColonne);
                    //monde.grille[nouvelleLigne, nouvelleColonne] = new PlanteEnvahissante(monde, nouvelleLigne, nouvelleColonne);
                    
                }
            }
        }
    }
}
