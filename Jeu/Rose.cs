public class Rose : Plante 
{
    public Rose(Monde monde, int x, int y) : base(monde, x, y)
    {
        idType = 2;
        quantiteEau = 30;
        tauxLuminosite = 70;
        terrainPrefere = 3; // TerrainSableux
        esperanceVie = 3;
        nbFruit = 4;
        visuelPlante = new string[4] { "🌱", "🌿", "🌹​", "🥀" };
    }
}