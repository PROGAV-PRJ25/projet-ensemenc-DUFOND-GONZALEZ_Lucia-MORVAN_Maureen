public class Tulipe : Plante
{
    public Tulipe(Monde monde, int x, int y) : base(monde, x, y)
    {
        idType = 1;
        quantiteEau = 30;
        tauxLuminosite = 60;
        terrainPrefere = 2; // TerrainSableux
        eauSeuilInf = 5;
        eauSeuilSup = 100;
        tempSeuilInf = 2;
        tempSeuilSup = 25;
        esperanceVie = 4;
        nbFruit = 2;
        visuelPlante = new string[4] { "🌱", "🌸", "🌷", "🍃" };
    }
}