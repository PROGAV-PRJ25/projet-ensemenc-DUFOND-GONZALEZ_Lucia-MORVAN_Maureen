public class MeteoEte : Meteo
{
    public MeteoEte(Monde monde) : base(monde)
    {
        this.monde = monde;
    }

    public override void DeterminerVariables()
    {
        // En Ete, la catastrophe est la journée de forte chaleur.
        Random random = new Random();
        if (catastrophe)
        {
            probaPleuvoir = 0;
            temperature = random.Next(25, 40);
            niveauVent = random.Next(0, 10);
        }
        else // on retourne sur les valeurs des paramètres classiques pour l'été
        {
            probaPleuvoir = 20;
            temperature = random.Next(20, 30); // le maximum est non-inclus dans Next
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