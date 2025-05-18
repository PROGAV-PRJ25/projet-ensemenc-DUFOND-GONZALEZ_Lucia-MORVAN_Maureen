public abstract class Plante
{
    public Monde monde;
    public int xPlante;
    public int yPlante;
    public int idType;
    public int EtapeCroissance;
    public bool maladie;
    protected int quantiteEau;
    protected int tauxLuminosite;
    protected int temperatureNecessaire;
    public int terrainPrefere;
    public int esperanceVie;
    public int nbFruit;
    protected string[] visuelPlante = new string[4];
    public bool estMorte;

    public Plante(Monde unMonde, int x, int y)
    {
        monde = unMonde;
        xPlante = x;
        yPlante = y;
        EtapeCroissance = 0;    // Valeur allant de 1 (graine) à 4 (fin de vie)
        maladie = false;
        estMorte = false;
    }

    public string AfficherVisuel()
    {
        int index = Math.Clamp(EtapeCroissance - 1, 0, visuelPlante.Length - 1); // Clamp permet de ne pas sortir des valeurs min et max -> eviter des erreurs
        return (visuelPlante[index]);
    }

    public bool VerifCroissancePossible(int x, int y, Meteo meteo)
    {
        int conditionsRespectees = 0; int conditionsTotales = 6;
        Terrain terrain = monde.grilleTerrain[x, y];
        if (quantiteEau >= (terrain.humidite - 20) && quantiteEau <= (terrain.humidite + 20)) conditionsRespectees++;
        if (tauxLuminosite >= terrain.luminosite - 20 && tauxLuminosite <= terrain.luminosite + 20) conditionsRespectees++;
        if (terrainPrefere == terrain.idType) conditionsRespectees++;
        if (terrain.fertilite >= 50) conditionsRespectees++;
        if (!maladie) conditionsRespectees++;
        if (temperatureNecessaire >= (meteo.temperature - 10) && temperatureNecessaire <= (meteo.temperature + 10)) conditionsRespectees++;

        if (conditionsRespectees >= conditionsTotales / 2) return true;
        else return false;
    }

    public void Croitre(Monde monde, Meteo meteo)
    {
        if (VerifCroissancePossible(xPlante, yPlante, meteo))
        {
            if (EtapeCroissance < 4) EtapeCroissance++;
            else if (EtapeCroissance >= 4 && esperanceVie > 0)
            {
                EtapeCroissance = 0;
                esperanceVie--;
            }
            else
            {
                estMorte = true;
                Console.WriteLine("Votre plante ne pousse pas car ce ne sont pas les circonstances idéales pour elle");
            }
        }
    }

    public override string ToString()
    {
        string[] terrain = { "🟩", "🟦", "🟨", "🟫" };
        string message = $"{visuelPlante[2]} : (💧 {quantiteEau}% d'humidité, 🌤️​  {tauxLuminosite}% de lumière, 🌱  {nbFruit} fruits maximum, {terrain[terrainPrefere]} terrain préféré)";
        return message;
    }
}