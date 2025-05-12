public class MeteoHiver : Meteo
{
    public MeteoHiver(Monde monde) : base(monde)
    {
        this.monde = monde;
        this.probaPleuvoir = 50; // se lit en pourcentage
    }

    public override void DeterminerVariables()
    {
        Random random = new Random();
        temperature = random.Next(-5, 8);
        niveauVent = random.Next(0, 50);
    }
}