public class MeteoPrintemps : Meteo
{
    public MeteoPrintemps(Monde monde) : base(monde)
    {
        this.monde = monde;
        this.probaPleuvoir = 50; // se lit en pourcentage
    }

    public override void DeterminerTemperature()
    {
        // Durant le printemps, les températures varient entre 9 et 14 degrés (pourquoi pas)
        Random random = new Random();
        temperature = random.Next(9, 15); // le maximum est non-inclus dans Next
        Console.WriteLine($"Il fait actuellement {temperature} degrés");
        Thread.Sleep(3000);
    }
}