public class Plante
{
    public Monde monde;
    public int xPlante;
    public int yPlante;
    protected bool especeEnvahissante;
    public int etapeCroissance {get; protected set;}
    protected int quantiteEau;
    protected int tauxLuminosite;
    protected int esperanceVie;
    protected int terrainPrefere;  // on utilise un int, et on associe à chaque terrain un chiffre
    
    protected string[]? visuelPlante;

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
        etapeCroissance = 1; // Cela ira de 1 (graine) à 4 (mort)
        visuelPlante =  new string[] {"🌱", "🌿", "🌳", "💀"};
    }

    public string AfficherVisuel()
    {
        int index = Math.Clamp(etapeCroissance - 1, 0, visuelPlante.Length - 1); //Clamp permet de ne pas sortir des valeurs min et max -> eviter des erreurs
        return visuelPlante[index];
    }

    public void Croitre()
    {
        if (etapeCroissance < 4)
            etapeCroissance++;
    }

    public virtual void SePropager()
    {

    }
}