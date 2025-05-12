public abstract class Plante
{
    public Monde monde;
    public int xPlante;
    public int yPlante;
    public int idType;
    public int EtapeCroissance;
    protected bool maladie;
    protected int quantiteEau;
    protected int eauSeuilSup;
    protected int eauSeuilInf;
    protected int tauxLuminosite;
    protected int temperatureNecessaire; // A modifier pour toutes les plantes
    protected int tempSeuilInf;
    protected int tempSeuilSup;
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
        // TO DO : Vérifier si 50% des conditions sont respectées (si !vent, ! secheresse, !tempete, saison...)
        int conditionsRespectees = 0; int conditionsTotales = 5;
        Terrain terrain = monde.grilleTerrain[x, y];
        if (quantiteEau >= (terrain.humidite - 20) && quantiteEau <= (terrain.humidite + 20)) conditionsRespectees++;
        if (tauxLuminosite >= terrain.luminosite - 20 && tauxLuminosite <= terrain.luminosite + 20) conditionsRespectees++;
        if (terrainPrefere == terrain.idType) conditionsRespectees++;
        if (terrain.fertilite >= 50) conditionsRespectees++;
        if (!maladie) conditionsRespectees++;
        if (temperatureNecessaire >= (meteo.temperature - 5) && temperatureNecessaire <= (meteo.temperature + 5)) conditionsRespectees++;


        if (conditionsRespectees >= conditionsTotales / 2) return true;
        else return false;
    }

    public bool verifSurvie(int x, int y, Meteo meteo)
    {
        Terrain terrain = monde.grilleTerrain[x, y];
        if (meteo.temperature > tempSeuilSup || meteo.temperature < tempSeuilInf)
        {
            return false;

        }

        else if (terrain.humidite > eauSeuilSup || terrain.humidite < eauSeuilInf)
        {
            return false;
        }

        else
            return true;

    }

    public void Croitre(Monde monde, Meteo meteo)
    {
        if (!verifSurvie(xPlante, yPlante, meteo))
        {
            estMorte = true;
            Console.WriteLine("Votre plante n'a pas respecté les conditions de survie");
        }
        else if (VerifCroissancePossible(xPlante, yPlante, meteo))
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