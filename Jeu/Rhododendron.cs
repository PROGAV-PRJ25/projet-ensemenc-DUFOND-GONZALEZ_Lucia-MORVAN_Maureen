public class Rhododendron : Plante
{
    public Rhododendron(Monde monde, int x, int y) : base(monde, x, y)
    {
        this.especeEnvahissante = true; // Il s'agit d'une espèce envahissante pour l'Irlande
        this.quantiteEau = 80;
        this.tauxLuminosite = 40;
        this.terrainPrefere = 2; // Le rhodendron préfère les sols sableaux
        this.esperanceVie = 100; // JE ne sais pas quoi mettre comme valeur

        // Visuel spécifique à une fleur (c'est mims)
        this.visuelPlante = new string [] { "🌱", "🌸", "🌺", "💀" };
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
                    monde.grille[nouvelleLigne, nouvelleColonne] = new Rhododendron(monde, nouvelleLigne, nouvelleColonne);
                }
            }
        }
    }
}
