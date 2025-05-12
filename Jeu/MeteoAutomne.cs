public class MeteoAutomne : Meteo
{
    public MeteoAutomne(Monde monde) : base(monde)
    {
        this.monde = monde;
        this.probaPleuvoir = 60; // se lit en pourcentage
    }

    public override void DeterminerVariables()
    {
        // Durant le printemps, les températures varient entre 9 et 14 degrés (pourquoi pas)
        Random random = new Random();
        temperature = random.Next(10, 20); 
        niveauVent = random.Next(10, 60);
    }
}