public class MeteoHiver : Meteo
{
    public MeteoHiver(Monde monde) : base(monde)
    {
        this.monde = monde;
        probaPleuvoir = 50;
    }

    public override void DeterminerVariables()
    {
        // La catastrophe en hiver est le gel
        Random random = new Random();
        if (catastrophe)
        {
            temperature = random.Next(-15, 5);
            Console.WriteLine("Attention, il gèle cet hiver.");
            Thread.Sleep(3000);
        }
        temperature = random.Next(-5, 8);
        niveauVent = random.Next(0, 50);
    }

    public override void AfficherEvenement()
    {
        if (catastrophe)
        {
            Console.WriteLine("Il a gelé");
        }

    }
}