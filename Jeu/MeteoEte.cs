public class MeteoEte : Meteo
{
    public MeteoEte(Monde monde) : base(monde)
    {
        this.monde = monde;
        this.probaPleuvoir = 20; // se lit en pourcentage
    }

    public override void DeterminerVariables()
    {
        Random random = new Random();
        temperature = random.Next(25, 35); // le maximum est non-inclus dans Next
        niveauVent = random.Next(0,30);
    }
}