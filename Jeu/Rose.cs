public class Rose : Plante 
{
    public Rose(Monde monde, int x, int y) : base(monde, x, y)
    {
        idType = 2;
        quantiteEau = 40;
        tauxLuminosite = 50;
        terrainPrefere = 2; // TerrainSableux
        esperanceVie = 3;
        nbFruit = 4;
        temperatureNecessaire = 11;
        visuelPlante = new string[4] { "🌱", "🌿", "🌹​", "🥀" };
    }
}