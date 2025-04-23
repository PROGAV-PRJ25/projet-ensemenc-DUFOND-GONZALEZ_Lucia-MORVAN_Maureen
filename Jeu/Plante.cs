public class Plante // PENSER A METTRE LE ABSTRACT PLUS TARD
{
    public Monde monde;
    public int xPlante;
    public int yPlante;
    protected bool especeEnvahissante;
    public int EtapeCroissance;
    protected bool maladie;
    protected int quantiteEau;
    protected int tauxLuminosite;
    public int terrainPrefere;
    protected int esperanceVie;
    protected string[] visuelPlante;
    protected int presenceAnimal;

    // Je ne me souviens plus de pourquoi je l'avais utilisé mais il doit y avoir une raison pour l'affichage
    // protected bool SeTrouveDansLaGrille(int X, int Y)
    // {
    //     if (X >= 0 && X < monde.X)
    //     {
    //         if (Y >= 0 && Y < monde.Y)
    //         {
    //             return true;
    //         }
    //         else
    //             return false;
    //     }
    //     else
    //         return false;
    // }

    public Plante(Monde unMonde, int x, int y)
    {
        // La plante pousse à un endroit aléatoire
        this.monde = unMonde;
        xPlante = x;
        yPlante = y;
        EtapeCroissance = 0; // Cela ira de 1 (graine) à 4 (mort)
        visuelPlante = new string[4] { "🌱", "🌿", "🌳", "💀" };
        maladie = false;
        presenceAnimal = 0; // Il n'y a pas d'animal
    }

    public string AfficherVisuel()
    {
        int index = Math.Clamp(EtapeCroissance - 1, 0, visuelPlante.Length - 1); //Clamp permet de ne pas sortir des valeurs min et max -> eviter des erreurs
        return (visuelPlante[index]);
    }

    public void Croitre()
    {
        if (EtapeCroissance < 4)
            EtapeCroissance++;
        else EtapeCroissance = 0;
    }

    public virtual void SePropager()
    {

    }
}