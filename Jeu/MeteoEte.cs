public class MeteoEte : Meteo
{
    public MeteoEte(Monde monde) : base(monde)
    {
        this.monde = monde;
        if (catastrophe)
        {
            probaPleuvoir = 5; // se lit en pourcentage
        }
        else
            probaPleuvoir = 20; 
    }

    public override void DeterminerVariables()
    {
        // En Ete, la catastrophe est le rique de sécheresse
        Random random = new Random();
        if (catastrophe)
        {
            temperature = random.Next(33, 45);
            niveauVent = random.Next(0, 10);
        }
        else
        {
            temperature = random.Next(25, 35); // le maximum est non-inclus dans Next
            niveauVent = random.Next(0, 30);
        }

    }
}