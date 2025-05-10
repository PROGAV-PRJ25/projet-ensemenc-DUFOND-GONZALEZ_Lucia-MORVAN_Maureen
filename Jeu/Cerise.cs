public class Cerise : Plante
{
    public Cerise(Monde monde, int x, int y) : base(monde, x, y)
    {
        idType = 4;
        quantiteEau = 50;
        tauxLuminosite = 50;
        terrainPrefere = 3; // TerrainTerreux
        esperanceVie = 2;
        nbFruit = 4;
        visuelPlante = new string[] { "🌱", "🌿", "🍒", "🍂" };
    }
}