public class Noisetier : Plante 
{
    public Noisetier(Monde monde, int x, int y) : base(monde, x, y)
    {
        idType = 1;
        quantiteEau = 40;
        tauxLuminosite = 40;
        terrainPrefere = 0; // TerrainBoise
        esperanceVie = 4;
        nbFruit = 10;
        temperatureNecessaire = 15;
        visuelPlante = new string[4] { "🌱", "🌿", "🌳", "🍂" };
    }
}