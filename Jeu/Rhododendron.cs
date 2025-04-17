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
        this.visuelPlante = new List<string> { "🌱", "🌸", "🌺", "💀" };
    }

    public override void SePropager()
    {
        if (EtapeCroissance >= 2 && EtapeCroissance < 4)
        {
            int newLigne = xPlante + 1;
            int newColonne = yPlante;
            if (newLigne >= 0 && newLigne < monde.ligne && newColonne >= 0 && newColonne < monde.ligne)
            {
                if (monde.grille?[newLigne, newColonne] == null)
                {
                    monde.grille[newLigne, newColonne] = new Rhododendron(monde, newLigne, newColonne);
                }
            }
        }
    }
}
