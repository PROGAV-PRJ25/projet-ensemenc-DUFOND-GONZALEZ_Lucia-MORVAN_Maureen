public abstract class Terrain
{
    public string type;
    public int idType;
    public int humidite;
    public int luminosite;
    public int fertilite;
    public string visuelTerrain;

    public Terrain(string leType, int leIdType, int tauxHumidite, int tauxLuminosite, int tauxFertilite, string leVisuel)
    {
        type = leType;
        idType = leIdType;
        humidite = tauxHumidite;
        luminosite = tauxLuminosite;
        fertilite = tauxFertilite;
        visuelTerrain = leVisuel;
    }

    public override string ToString()
    {
        string message = $"{visuelTerrain} - Terrain {type} (🌧️  {humidite}% d'humidité, ☀️  {luminosite}% de luminosité, 🌱 {fertilite}% de fertilité)";
        return message;
    }
}