public class Noisetier : Plante 
{
    public Noisetier(Monde monde, int x, int y) : base(monde, x, y)
    {
        idType = 1;
        quantiteEau = 40;
        tauxLuminosite = 40;
        terrainPrefere = 1; // TerrainBoise
        esperanceVie = 4;
        nbFruit = 10;
        visuelPlante = new string[4] { "🌱", "🌿", "🌳", "🍂" };
    }
}