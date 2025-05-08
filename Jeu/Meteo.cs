public class Meteo
{
    public Monde monde;
    protected int probaPleuvoir;
    public bool estEnTrainDePleuvoir = false;
    public static int nombreJoursSansPluie = 0;

    public Meteo(Monde monde)
    {
        this.monde = monde;
    }

    // Objectif de cette classe météo: Les terrains sont plus ou moins sensibles aux jours avec ou sans pluie
    // On comptabilise les jours sans pluie et les jours avec pluie et le taux d'humidité est modifié à chaque fois, avec des écarts plus ou moins grand selon le terrain et sa particularité

    // On suppose qu'il existe une listeTerrain dans monde

    public void Pleuvoir()
    {
        Random random = new Random();
        int chance = random.Next(0, 100);
        if (chance < probaPleuvoir)
        {
            estEnTrainDePleuvoir = true;
            List<Terrain> terrainsModifiés = new List<Terrain>();
            // Parcourir tout le monde avec la grille et à chaque fois changer le type de terrain
            // Problème : je rajoute 10 à chaque fois avant d'avoir changé de terrain, et il ne faut pas
            for (int i = 0; i < this.monde.ligne; i++)
            {
                for (int j = 0; j < this.monde.colonne; j++)
                {
                    Terrain terrain = this.monde.grilleTerrain[i, j];

                    if (!terrainsModifiés.Contains(terrain) && (terrain.humidite + 10 <= 100))
                    {
                        terrain.humidite += 10;
                        terrainsModifiés.Add(terrain);
                    }
                }
                nombreJoursSansPluie = 0;
                estEnTrainDePleuvoir = false;
            }
            Console.WriteLine("Il pleut !"); // à enlever probablement
        }
        else { nombreJoursSansPluie++; }

        if (nombreJoursSansPluie > 3)
        {
            List<Terrain> terrainsModifiés = new List<Terrain>();
            for (int i = 0; i < this.monde.ligne; i++) // grilleTerrain comprend des classes Terrains
            {
                for (int j = 0; j < this.monde.colonne; j++)
                {
                    Terrain terrain = this.monde.grilleTerrain[i, j];

                    if (!terrainsModifiés.Contains(terrain) && (terrain.humidite + 10 <= 100))
                    {
                        terrain.humidite += 10;
                        terrainsModifiés.Add(terrain);
                    }
                }
            }
        }
    }

    public void AfficherHumiditeTerrain() // à modifier également
    {
        List<Terrain> terrainsModifiés = new List<Terrain>();
        for (int i = 0; i < this.monde.ligne; i++) // grilleTerrain comprend des classes Terrains
        {
            for (int j = 0; j < this.monde.colonne; j++)
            {
                Terrain terrain = this.monde.grilleTerrain[i, j];

                if (!terrainsModifiés.Contains(terrain) && (terrain.humidite + 10 <= 100))
                {
                    Console.WriteLine(monde.grilleTerrain[i, j]);
                    terrainsModifiés.Add(terrain);
                }
            }
        }
    }
}