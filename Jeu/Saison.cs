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
        // TO DO: modifier aussi avec la température et le vent + luminosité en fonction des nuages
        if (temps < 4) // fonctionne mais le temps doit être ajusté
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
        if (libelle != saisonPrecedente)
        {
            Console.Clear();
            switch (libelle)
            {
                case "Printemps":
                    //Visuel.AnnoncerPrintemps();
                    Console.WriteLine("C'est le printemps"); // Le temps qu'on programme pour éviter de perdre du temps
                    meteo = new MeteoPrintemps(this.monde);
                    break;
                case "Ete":
                    Console.WriteLine("Déjà l'été ! ");
                    meteo = new MeteoEte(this.monde);
                    break;
                case "Automne":
                    Console.WriteLine("Les feuilles commencent à tomber, c'est l'automne.");
                    break;
                case "Hiver":
                    Console.WriteLine("Winter is coming...");
                    break;
            }
            Thread.Sleep(3000);
        }
        saisonPrecedente = libelle;
    }




}




