public class Cerise : Plante
{
    public Cerise(Monde monde, int x, int y) : base(monde, x, y)
    {
        quantiteEau = 80;
        tauxLuminosite = 40;
        terrainPrefere = 3; // TerrainTerreux
        esperanceVie = 2;
        nbFruit = 3;
        visuelPlante = new string[] { "🌱", "🌿", "🍒", "🍂" };
    }
}