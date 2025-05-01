public class Sapin : Plante 
{
    public Sapin(Monde monde, int x, int y) : base(monde, x, y){
        quantiteEau = 50;
        tauxLuminosite = 20;
        terrainPrefere = 0; // TerrainBoise
        esperanceVie = 3;
        nbFruit = 7;
        visuelPlante = new string[4] { "🌱", "🌿", "🌲", "🍂" };
    }
}