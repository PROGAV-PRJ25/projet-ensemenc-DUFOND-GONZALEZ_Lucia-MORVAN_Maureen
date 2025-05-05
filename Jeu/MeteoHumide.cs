public class MeteoHumide : Meteo
{
    public MeteoHumide(Monde monde) : base(monde)
    {
        this.monde = monde;
        this.probaPleuvoir = 75; // se lit en pourcentage
    }
}