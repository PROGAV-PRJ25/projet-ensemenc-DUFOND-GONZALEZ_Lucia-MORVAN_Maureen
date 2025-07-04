public class Tulipe : Plante
{
    public Tulipe(Monde monde, int x, int y) : base(monde, x, y)
    {
        idType = 1;
        quantiteEau = 30;
        tauxLuminosite = 60;
        terrainPrefere = 2; // TerrainSableux
        esperanceVie = 4;
        nbFruit = 2;        
        temperatureNecessaire = 18;
        visuelPlante = new string[4] { "🌱", "🌸", "🌷", "🍃" };
    }
}