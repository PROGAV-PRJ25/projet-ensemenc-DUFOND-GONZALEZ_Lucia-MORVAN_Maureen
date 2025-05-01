public abstract class Plante
{
    public Monde monde;
    public int xPlante;
    public int yPlante;
    public int EtapeCroissance;
    protected bool maladie;
    protected int quantiteEau;
    protected int tauxLuminosite;
    public int terrainPrefere;
    public int esperanceVie;
    protected int presenceAnimal;
    public int nbFruit;
    protected string[] visuelPlante = new string[4];

    public Plante(Monde unMonde, int x, int y)
    {
        // La plante pousse à un endroit aléatoire
        this.monde = unMonde;
        xPlante = x;
        yPlante = y;
        EtapeCroissance = 0; // Cela ira de 1 (graine) à 4 (mort)
        //visuelPlante = new string[4] { "🌱", "🌿", "🌳", "💀" };
        maladie = false;
        presenceAnimal = 0; // Il n'y a pas d'animal
    }

    public string AfficherVisuel()
    {
        int index = Math.Clamp(EtapeCroissance - 1, 0, visuelPlante.Length - 1); //Clamp permet de ne pas sortir des valeurs min et max -> eviter des erreurs
        return (visuelPlante[index]);
    }

    public void Croitre(Monde monde)
    {
        // Vérifier si 50% des conditions sont respectées (humidite, luminosite...)
        if (EtapeCroissance < 4) EtapeCroissance++;
        else if(EtapeCroissance>=4 && esperanceVie>0){
            EtapeCroissance = 0;
            esperanceVie--;
        }
        else{ // Plante morte et esperance null, on la retire de la grille & de la liste des plantes
            monde.grille[xPlante, yPlante] = null;
            monde.listePlante?.Remove(this);
        }
    }

    public virtual void SePropager()
    {}

    public override string ToString()
    {
        string message = $"{visuelPlante} - (💧 {quantiteEau}% d'humidité, 🌤️​ {tauxLuminosite}% de lumière, 🌱 {nbFruit}nombre de fruit)";
        return message;
    }
}