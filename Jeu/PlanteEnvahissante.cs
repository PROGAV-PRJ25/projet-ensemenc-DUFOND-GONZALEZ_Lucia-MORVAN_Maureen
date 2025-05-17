public class PlanteEnvahissante : Plante
{
    public PlanteEnvahissante(Monde monde, int x, int y) : base(monde, x, y){}

    public virtual void SePropager()
    {
        Random rng = new Random();
        if (EtapeCroissance == 2)
        {
            int nouvelleLigne = xPlante + rng.Next(-1,2);
            int nouvelleColonne = yPlante + rng.Next(-1,2);
            if (nouvelleLigne >= 0 && nouvelleLigne < monde.ligne && nouvelleColonne >= 0 && nouvelleColonne < monde.ligne)
            {
                 // La plante se propage seulement sur les cases vides et non creuser par une tranchée
                if (monde.grillePlante?[nouvelleLigne, nouvelleColonne] == null && monde.grilleTerrain[nouvelleLigne,nouvelleColonne].idType != 5)
                {
                    var nouvellePlante = (PlanteEnvahissante)Activator.CreateInstance (this.GetType(), monde, nouvelleLigne, nouvelleColonne)!;
                    monde.AjouterPlante(nouvellePlante, nouvelleLigne, nouvelleColonne, false);
                }
            }
        }
    }
}