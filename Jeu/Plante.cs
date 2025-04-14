public class Plante
{
    public Monde monde1;
    protected int xPlante;
    protected int yPlante;
    protected bool especeEnvahissante;
    protected int etapeCroissance;
    protected int quantiteEau;
    protected int tauxLuminosite;
    protected int esperanceVie;
    protected int terrainPrefere;  // on utilise un int, et on associe à chaque terrain un chiffre
    
    protected char[]? visuelPlante;

    // Je ne me souviens plus de pourquoi je l'avais utilisé mais il doit y avoir une raison pour l'affichage
    protected bool SeTrouveDansLaGrille(int X, int Y)
    {
        if (X >= 0 && X < monde1.X)
        {
            if (Y >= 0 && Y < monde1.Y)
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }


    public Plante(Monde monde1)
    {
        // La plante pousse à un endroit aléatoire
        this.monde1 = monde1;
        Random rnd = new Random();
        yPlante = rnd.Next(0, monde1.Y);
        XPlante = rnd.Next(0, monde1.X);
        etapeCroissance = 1; // Cela ira de 1 (graine) à 4 (mort)
        visuelPlante =  new char [3];
    }
    }

    public virtual void SePropager()
    {
    }




}

