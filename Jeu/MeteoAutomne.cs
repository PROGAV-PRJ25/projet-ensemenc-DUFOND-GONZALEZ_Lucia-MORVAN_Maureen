public class MeteoAutomne : Meteo
{
    public MeteoAutomne(Monde monde) : base(monde)
    {
        this.monde = monde;
        if (catastrophe)
        {
            probaPleuvoir = 90;
        }
        else
            probaPleuvoir = 60; // se lit en pourcentage
    }

    public override void DeterminerVariables()
    {
        // La catastrpophe en automne correspond à une tempête
        Random random = new Random();
        if (catastrophe)
        {
            temperature = random.Next(0, 10);
            niveauVent = random.Next(85, 100);
        }
        else
        {
            temperature = random.Next(10, 20);
            niveauVent = random.Next(10, 60);
        }
    }
}