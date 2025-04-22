public abstract class Terrain
{
    public int humidite;
    public string visuelTerrain;

    public Terrain(string emojiTerrain, int tauxHumidite)
    {
        visuelTerrain = emojiTerrain;
        humidite = tauxHumidite;
    }

    public string AfficherVisuel()
    {
        return (visuelTerrain);
    }
}