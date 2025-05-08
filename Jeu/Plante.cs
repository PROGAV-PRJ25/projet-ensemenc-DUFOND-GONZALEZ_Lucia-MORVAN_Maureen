public abstract class Plante
{
    public Monde monde;
    public int xPlante;
    public int yPlante;
    public int idType;
    public int EtapeCroissance;
    protected bool maladie;
    protected int quantiteEau;
    protected int tauxLuminosite;
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
        int index = Math.Clamp(EtapeCroissance - 1, 0, visuelPlante.Length - 1); //Clamp permet de ne pas sortir des valeurs min et max -> eviter des erreurs
        return (visuelPlante[index]);
    }

    public bool VerifCroissancePossible(int x, int y)
    {
        // TO DO : Vérifier si 50% des conditions sont respectées (si !vent, ! secheresse, !tempete, saison...)
        int conditionsRespectees = 0; int conditionsTotales = 5;
        Terrain terrain = monde.grilleTerrain[x,y];
        if(quantiteEau >= terrain.humidite-10 && quantiteEau <= terrain.humidite+10) conditionsRespectees++;
        if(tauxLuminosite >= terrain.luminosite-10 && tauxLuminosite <= terrain.luminosite+10) conditionsRespectees++;
        if(terrainPrefere == terrain.idType) conditionsRespectees++;
        if(terrain.fertilite >= 50) conditionsRespectees++;
        if(!maladie) conditionsRespectees++;
        
        if(conditionsRespectees/conditionsTotales >= 0.5) return true;
        else return false;
    }

    public void Croitre(Monde monde)
    {
        if(VerifCroissancePossible(xPlante, yPlante))
        {
            if (EtapeCroissance < 4) EtapeCroissance++;
            else if(EtapeCroissance >= 4 && esperanceVie > 0){
                EtapeCroissance = 0;
                esperanceVie--;
            }
            else estMorte = true;
        }
    }

    public override string ToString()
    {
        string message = $"{visuelPlante} - (💧 {quantiteEau}% d'humidité, 🌤️​ {tauxLuminosite}% de lumière, 🌱 {nbFruit}nombre de fruit)";
        return message;
    }
}