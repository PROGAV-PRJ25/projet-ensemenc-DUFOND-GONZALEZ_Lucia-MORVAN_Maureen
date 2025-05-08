public class Cerise : Plante
{
    public Cerise(Monde monde, int x, int y) : base(monde, x, y)
    {
        idType = 4;
        quantiteEau = 60;
        tauxLuminosite = 40;
        terrainPrefere = 4; // TerrainTerreux
        esperanceVie = 2;
        nbFruit = 3;
        visuelPlante = new string[] { "🌱", "🌿", "🍒", "🍂" };
    }
}