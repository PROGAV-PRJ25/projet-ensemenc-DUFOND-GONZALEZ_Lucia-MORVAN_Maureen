public class MeteoEte : Meteo
{
    public MeteoEte(Monde monde) : base(monde)
    {
        this.monde = monde;
    }

    public override void DeterminerVariables()
    {
        // En Ete, la catastrophe est le rique de sécheresse
        Random random = new Random();
        if (catastrophe)
        {
            probaPleuvoir = 5;
            temperature = random.Next(33, 45);
            niveauVent = random.Next(0, 10);
        }
        else
        {
            probaPleuvoir = 20;
            temperature = random.Next(25, 35); // le maximum est non-inclus dans Next
            niveauVent = random.Next(0, 30);
        }

    }

    public override void AfficherEvenement()
    {
        if (catastrophe)
        {
            Console.WriteLine("C'est la sécheresse");
        }

    }
}