public class Chene : Plante 
{
    public Chene(Monde monde, int x, int y) : base(monde, x, y){
        quantiteEau = 40;
        tauxLuminosite = 40;
        terrainPrefere = 0; // TerrainBoise
        esperanceVie = 4;
        nbFruit = 10;
        visuelPlante = new string[4] { "🌱", "🌿", "🌳", "🍂" };
    }
}