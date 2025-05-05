public abstract class Terrain
{
    public string type;
    public int humidite;
    public int fertilite;
    public string visuelTerrain;

    public Terrain(string leType, int tauxHumidite, int tauxFertilite, string leVisuel)
    {
        type = leType;
        humidite = tauxHumidite;
        fertilite = tauxFertilite;
        visuelTerrain = leVisuel;
    }

    public override string ToString()
    {
        string message = $"{visuelTerrain} - Terrain {type} (🌧️  {humidite}% d'humidité, 🌱 {fertilite}% de fertilité)";
        return message;
    }
}