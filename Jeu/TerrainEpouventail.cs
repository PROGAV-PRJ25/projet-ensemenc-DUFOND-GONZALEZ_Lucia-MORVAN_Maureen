public class TerrainEpouvantail : Terrain
{
    public TerrainEpouvantail() : base("Épouvantail", 6, 0, 0, 0, "🎃") {}

    public override string ToString()
    {
        string message = $"{visuelTerrain} - {type}";
        return message;
    }
}