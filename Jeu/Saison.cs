public class Saison
{
    public Meteo meteo;
    public string? libelle { get; set; }
    public int temps { get; set; }
    public string? saisonPrecedente { get; set; }
    public Monde monde;

    public Saison(Monde monde)
    {
        this.monde = monde;
        temps = 0;
        this.meteo = new MeteoPrintemps(this.monde); // On commence toujours au printemps
    }

    public void DeterminerSaison()
    {
      
        if (temps < 4)
        {
            libelle = "Printemps";
        }

        else if (temps < 8)
        {
            libelle = "Ete";
            //meteo.probaPleuvoir = 30;
        }

        else if (temps < 12)
        {
            libelle = "Automne";
            //meteo.probaPleuvoir = 60;
        }

        else if (temps < 14)
        {
            libelle = "Hiver";
            // meteo.probaPleuvoir = 50;
        }
        else
            temps = 0; // On retourne au printemps
    }

    public void AnnoncerSaison()
    {
        if (libelle != saisonPrecedente) // Réalise ces actions uniquement lorsqu'on change de saison
        {
            Console.Clear();

            // A chaque changement de saison, on annonce la saison suivante et on modifie la météo en conséquence
            switch (libelle)
            {
                case "Printemps":
                    Visuel.AnnoncerPrintemps(); 
                    meteo = new MeteoPrintemps(monde);
                    break;
                case "Ete":
                    Visuel.AnnoncerEte();
                    meteo = new MeteoEte(monde);
                    break;
                case "Automne":
                    Visuel.AnnoncerAutomne();
                    meteo = new MeteoAutomne(monde);
                    break;
                case "Hiver":
                    Visuel.AnnoncerHiver();
                    meteo = new MeteoHiver(monde);
                    break;
            }
        }
        saisonPrecedente = libelle;
    }
}