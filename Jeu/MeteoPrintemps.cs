public class MeteoPrintemps : Meteo
{
    public MeteoPrintemps(Monde monde) : base(monde)
    {
        this.monde = monde;
        probaPleuvoir = 50; // se lit en pourcentage
    }

    public override void DeterminerVariables()
    {
        Random random = new Random();
        temperature = random.Next(9, 15); // le maximum est non-inclus dans Next
        niveauVent = random.Next(5,40);// On condidère qu'il y a beaucoup de vent au printemps

    }
}