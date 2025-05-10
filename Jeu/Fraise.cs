public class Fraise : PlanteEnvahissante
{
    public Fraise(Monde monde, int x, int y) : base(monde, x, y)
    {
        idType = 3;
        quantiteEau = 60;
        tauxLuminosite = 40;
        terrainPrefere = 3; // TerrainTerreux
        esperanceVie = 1;
        nbFruit = 3;
        visuelPlante = new string[] { "🌱", "🌿", "🍓", "🍂" };
    }
}