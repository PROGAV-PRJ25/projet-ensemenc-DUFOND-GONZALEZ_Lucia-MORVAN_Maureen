/* public class Meteo
{
    public Monde monde;
    protected int probaPleuvoir;

    public bool estEnTrainDePleuvoir = false;
    public static int nombreJoursSansPluie = 0;

    public Meteo(Monde monde)
    {
        this.monde = monde;
    }

    // On suppose qu'il existe une listeTerrain dans monde
    public void Pleuvoir(Monde monde)
    {
        estEnTrainDePleuvoir = true;
        //foreach Terrain terrain in listeTerrain // Changer par GrilleTerrain
        for (int i = 0; i < grilleTerrain.Length; i++)
        {
            for (int j = 0; j < grilleTerrain.Length; j++)
            {
                if (grilleTerrain[i, j].tauxHumidite + 10 < 100)
                {
                    grilleTerrain[i, j].tauxHumidite += 10;
                    nombreJoursSansPluie = 0;
                }
            }
        }
    }

    public void VerifierHumididite() // Cette fonction (qui pourra être modifié pour ne plus être une fonction) sert à 


} */