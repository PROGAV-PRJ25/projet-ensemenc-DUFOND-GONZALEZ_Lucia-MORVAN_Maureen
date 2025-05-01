public abstract class Terrain
{
    public string type;
    public int humidite;
    public int fertilite;
    public string visuelTerrain;

    public Terrain(int tauxHumidite, int tauxFertilite)
    {
        humidite = tauxHumidite;
        fertilite = tauxFertilite;
    }

    public string AfficherVisuel()
    {
        return (visuelTerrain);
    }

    public override string ToString()
    {
        string message = $"{visuelTerrain} - Terrain {type} (🌧️  {humidite}% d'humidité, 🌱 {fertilite}% de fertilité)";
        return message;
    }
}