public class Rhododendron : PlanteEnvahissante
{
    public Rhododendron(Monde monde, int x, int y) : base(monde, x, y)
    {
        quantiteEau = 80;
        tauxLuminosite = 40;
        terrainPrefere = 2; // TerrainHumide
        esperanceVie = 2;
        nbFruit = 3;
        visuelPlante = new string[] { "🌱", "🌸", "🌺", "🥀" };
    }

    public override void SePropager()
    {
        base.SePropager();
    }
}
