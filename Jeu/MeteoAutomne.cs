public class MeteoAutomne : Meteo
{
    public MeteoAutomne(Monde monde) : base(monde)
    {
        this.monde = monde;
    }

    public override void DeterminerVariables()
    {
        // La catastrpophe en automne correspond à une tempête
        Random random = new Random();
        if (catastrophe)
        {
            probaPleuvoir = 90;
            temperature = random.Next(0, 10);
            niveauVent = random.Next(85, 100);
            Console.WriteLine("Attention, la tempête est là...");
        }
        else
        {
            probaPleuvoir = 60;
            temperature = random.Next(10, 20);
            niveauVent = random.Next(10, 60);
        }
    }

    public override void AfficherEvenement()
    {
        if (catastrophe)
        {
            Console.WriteLine("C'est la tempête");
        }

    }
}