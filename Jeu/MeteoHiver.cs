public class MeteoHiver : Meteo
{
    public MeteoHiver(Monde monde) : base(monde)
    {
        this.monde = monde;
        probaPleuvoir = 50; // se lit en pourcentage
    }

    public override void DeterminerVariables()
    {
        // La catastrophe en hiver est le gel
        Random random = new Random();
        if (catastrophe)
        {
            temperature = random.Next(-15, 5);
        }
        temperature = random.Next(-5, 8);
        niveauVent = random.Next(0, 50);
    }
}