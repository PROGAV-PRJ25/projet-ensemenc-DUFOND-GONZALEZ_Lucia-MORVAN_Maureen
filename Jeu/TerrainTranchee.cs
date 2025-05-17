public class TerrainTranchee : Terrain
{
    public TerrainTranchee() : base("Tranchee",5,0,0,0,"⬛​"){}

    public override string ToString()
    {
        string message = $"{visuelTerrain} - {type}";
        return message;
    }
}