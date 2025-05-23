using System.Runtime.CompilerServices;

public abstract class Meteo
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


    // La fonction Pleuvoir est appelée pour déterminer s'il va pleuvoir
    // Selon s'il pleut, des changements ont lieu

    public void Pleuvoir()
    {
        estEnTrainDePleuvoir = false;
        Random random = new Random();
        int chance = random.Next(0, 100);
        if (chance < probaPleuvoir) 
        {
            estEnTrainDePleuvoir = true; // Le booleen sert pour le visuel (utilisé dans la classe Simulation)
            List<Terrain> terrainsModifiés = new List<Terrain>(); // Cette nouvelle liste permet de vérifier que l'humidité du terrain n'a pas déjà été augmenté

            // Parcourir tout le monde avec la grille et à chaque fois changer le type de terrain
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

            List<Terrain> terrainsModifiés = new List<Terrain>();

            for (int i = 0; i < this.monde.ligne; i++) // grilleTerrain comprend des classes Terrains
            {
                for (int j = 0; j < this.monde.colonne; j++)
                {
                    Terrain terrain = this.monde.grilleTerrain[i, j];

                    if (!terrainsModifiés.Contains(terrain))
                    {
                        if ((nombreJoursSansPluie > 3) && (terrain.humidite - 10 >= 0))
                        {
                            terrain.humidite -= 10;
                        }
                        if(terrain.luminosite + 20 <= 100) terrain.luminosite += 20;
                        terrainsModifiés.Add(terrain);
                    }
                }
            }
        
        }
    }

    public void AfficherHumiditeTerrain()
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

        if (probaCatastrophe == 1)
        {
            catastrophe = true;
            Simulation.modeUrgence = true; // Le mode urgence est activé lorsqu'il y a des météos extrêmes
        }
        DeterminerVariables(); 
    }

    public virtual void DeterminerVariables() // Les paramètres changent selon la météo, qui est elle-même liée à la saison
    { }

    public virtual void AfficherEvenement()
    { }
}