/*public class Meteo
{
    public Monde monde;
    protected int probaPleuvoir;

    public bool estEnTrainDePleuvoir = false;
    public static int nombreJoursSansPluie = 0;

    public Meteo(Monde monde)
    {
        this.monde = monde;
    }

    // Objectif de cette classe météo: Les terrains sont plus ou moins sensible aux jours avec ou sans pluie
    // On comptabilise les jours sans pluies et les jours avec pluie et le taux d'humidité est modifié à chaque fois, avec des écarts plus ou moins grand selon le terrain et sa particularité


    // On suppose qu'il existe une listeTerrain dans monde
    public void Pleuvoir(Monde monde)
    {
        estEnTrainDePleuvoir = true;

        for (int i = 0; i < monde.grilleTerrain.ligne; i++) // grilleTerrain comprend des classes Terrains
        {
            for (int j = 0; j < monde.grilleTerrain.colonne; j++)
            {
                if (monde.grilleTerrain[i, j].tauxHumidite + 10 < 100)
                {
                    monde.grilleTerrain[i, j].tauxHumidite += 10;
                    nombreJoursSansPluie = 0;
                }
            }
        }
    }

    public void VerifierHumidite(Monde monde)// Cette fonction (qui pourra être modifié pour ne plus être une fonction) sert à vérifier le nombre de jours durant lesquels il n'a pas plus
    {
        if (!estEnTrainDePleuvoir)
        {
            nombreJoursSansPluie++;
        }

        if (nombreJoursSansPluie > 3)
        {
            for (int i = 0; i < monde.grilleTerrain.ligne; i++) // grilleTerrain comprend des classes Terrains
            {
                for (int j = 0; j < monde.grilleTerrain.colonne; j++)
                {
                    if (monde.grilleTerrain[i, j].tauxHumidite - 10 > 0)
                    {
                        monde.grilleTerrain[i, j].tauxHumidite -= 10;
                    }
                }
            }

        }


    }

}

*/