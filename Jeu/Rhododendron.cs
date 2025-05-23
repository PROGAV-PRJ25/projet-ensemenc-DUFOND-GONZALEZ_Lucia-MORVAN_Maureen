public class Rhododendron : PlanteEnvahissante
{
    public Rhododendron(Monde monde, int x, int y) : base(monde, x, y)
    {
        idType = 3;
        quantiteEau = 80;
        tauxLuminosite = 40;
        terrainPrefere = 1; // TerrainHumide
        esperanceVie = 2;
        nbFruit = 3;
        temperatureNecessaire = 13;
        visuelPlante = new string[] { "🌱", "🌸", "🌺", "🥀" };
    }

    public override void SePropager()
    {
        base.SePropager();
    }
}
