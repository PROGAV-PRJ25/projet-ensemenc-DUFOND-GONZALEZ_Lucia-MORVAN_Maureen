using System.Runtime.CompilerServices;

public class Meteo
{
    public Monde monde;
    public int temperature;
    public int probaPleuvoir;
    public bool estEnTrainDePleuvoir = false;
    public int niveauVent;
    public static int nombreJoursSansPluie = 0;
    protected bool catastrophe = false;

    public Meteo(Monde monde)
    {
        this.monde = monde;
    }

    // Objectif de cette classe météo : Les terrains sont plus ou moins sensibles aux jours avec ou sans pluie
    // On comptabilise les jours sans pluie et les jours avec pluie et le taux d'humidité est modifié à chaque fois, avec des écarts plus ou moins grand selon le terrain et sa particularité

    public void Pleuvoir()
    {
        estEnTrainDePleuvoir = false;
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
                        if(terrain.luminosite - 10 >= 0) terrain.luminosite -= 10; // On suppose que c'est nuageux
                        terrainsModifiés.Add(terrain);
                    }
                }
                nombreJoursSansPluie = 0;
            }
        }
        else
        {
            nombreJoursSansPluie++;

            if (nombreJoursSansPluie > 3)
            {
                List<Terrain> terrainsModifiés = new List<Terrain>();
                for (int i = 0; i < this.monde.ligne; i++) // grilleTerrain comprend des classes Terrains
                {
                    for (int j = 0; j < this.monde.colonne; j++)
                    {
                        Terrain terrain = this.monde.grilleTerrain[i, j];

                        if (!terrainsModifiés.Contains(terrain) && (terrain.humidite - 10 >= 0))
                        {
                            terrain.humidite -= 10;
                            if(terrain.luminosite + 20 <= 100) terrain.luminosite += 20;
                            terrainsModifiés.Add(terrain);
                        }
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

    public void DeterminerCatastropheEtVariables()
    {
        Random random = new Random();
        int probaCatastrophe;
        if (Simulation.modeDifficile)
        {
            probaCatastrophe = random.Next(3); // Il y a une chance sur 3 qu'une catastrophe arrive dans le mode difficile
        }
        else
            probaCatastrophe = random.Next(6); // Il y a une chance sur 6 qu'une catastrophe arrive lorsque ce n'est pas le mode difficile

        if (probaCatastrophe == 1) // Il y a une chance sur 3 qu'il y ait une catastrophe
        {
            catastrophe = true;
            Simulation.modeUrgence = true;
        }
        DeterminerVariables();

    }

    public virtual void DeterminerVariables()
    { }

    public virtual void AfficherEvenement()
    { }
}