public class Terrain
{
    public int humidite;
    public string visuelTerrain;

    public Terrain()
    {
        visuelTerrain = "⬜";
    }

    public string AfficherVisuel()
    {
        return (visuelTerrain);
    }
}