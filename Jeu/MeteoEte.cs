public class MeteoEte : Meteo
{
    public MeteoEte(Monde monde) : base(monde)
    {
        this.monde = monde;
        this.probaPleuvoir = 20; // se lit en pourcentage
    }

    public override void DeterminerTemperature()
    {
        // Durant le printemps, les températures varient entre 9 et 14 degrés (pourquoi pas)
        Random random = new Random();
        temperature = random.Next(25, 35); // le maximum est non-inclus dans Next
        Console.WriteLine($"Il fait actuellement {temperature} degrés");
        Thread.Sleep(3000);
    }
}