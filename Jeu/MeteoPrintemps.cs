public class MeteoPrintemps : Meteo
{
    public MeteoPrintemps(Monde monde) : base(monde)
    {
        this.monde = monde;
        this.probaPleuvoir = 50; // se lit en pourcentage
    }

    public override void DeterminerVariables()
    {
        // Durant le printemps, les températures varient entre 9 et 14 degrés (pourquoi pas)
        Random random = new Random();
        temperature = random.Next(9, 15); // le maximum est non-inclus dans Next

        niveauVent = random.Next(5,40);// On condidère qu'il y a beaucoup de vent au printemps




        // POur faire le test
        /* int test = random.Next(5);
        if (test == 0)
        {
            temperature = 0;
        }
        else if (test == 1)
        {
            temperature = 26;
        }
        else
            temperature = 17;
 */
    }
}