public class Trefle : Plante 
{
    public Trefle(Monde monde, int x, int y) : base(monde, x, y)
    {
        idType = 4;
        quantiteEau = 70;
        tauxLuminosite = 60;
        terrainPrefere = 2; // TerrainHumide
        esperanceVie = 1;
        nbFruit = 1;
        visuelPlante = new string[4] { "🌱", "☘️", "🍀", "🍃" };
    }
}