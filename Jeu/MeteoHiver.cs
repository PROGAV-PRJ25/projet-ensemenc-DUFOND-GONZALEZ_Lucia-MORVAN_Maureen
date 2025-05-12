public class MeteoHiver : Meteo
{
    public MeteoHiver(Monde monde) : base(monde)
    {
        this.monde = monde;
        this.probaPleuvoir = 50; // se lit en pourcentage
    }

    public override void DeterminerVariables()
    {
        // Durant le printemps, les températures varient entre 9 et 14 degrés (pourquoi pas)
        Random random = new Random();
        temperature = random.Next(-5, 8);
        niveauVent = random.Next(40, 80);
    }
}