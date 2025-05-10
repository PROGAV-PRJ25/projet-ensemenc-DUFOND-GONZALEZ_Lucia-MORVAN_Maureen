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
        //this.meteo = new Meteo(monde);  // TO DO Vérifier si j'en ai vraiment besoin
    }

    public void DeterminerSaison()
    {
        // TO DO: modifier aussi avec la température et le vent + luminosité en fonction des nuages
        if (temps < 4) // fonctionne mais le temps doit être ajusté
        {
            libelle = "Printemps";
            //meteo.probaPleuvoir = 70; // Il pleut souvent au printemps
        }

        if (temps > 4 && temps < 8)
        {
            libelle = "Ete";
            //meteo.probaPleuvoir = 30;
        }

        if (temps > 8 && temps < 12)
        {
            libelle = "Automne";
            //meteo.probaPleuvoir = 60;
        }

        if (temps > 12 && temps < 14)
        {
            libelle = "Hiver";
            // meteo.probaPleuvoir = 50;
        }

    }

    public void AnnoncerSaison()
    {
        if (libelle != saisonPrecedente)
        {
            Console.Clear();
            switch (libelle)
            {
                case "Printemps":
                    AnnoncerPrintemps();
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



    public void AnnoncerPrintemps()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // En-tête décoratif
        Visuel.PrintCenteredColored("🌸 Le Printemps Arrive 🌸", ConsoleColor.Green);
        Console.WriteLine();

        // Fleur ASCII
        string[] flowerArt = new string[]
        {
        "           wWWWw               wWWWw         ",
        "   vVVVv (___) wWWWw         (___)  vVVVv",
        "   (___)  ~Y~  (___)  vVVVv   ~Y~   (___)",
        "    ~Y~   \\|    ~Y~   (___)    |/    ~Y~",
        "    \\|   \\ |/   \\| /  \\~Y~/   \\|    \\ |/",
        "    \\\\|// \\\\|// \\\\|/// \\\\|//  \\\\|// \\\\\\|///",
        "   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^",
        };

        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (string line in flowerArt)
        {
            Visuel.PrintCentered(line);
            Thread.Sleep(150);
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;

        // Texte machine à écrire
        Visuel.TypewriterCentered("Les bourgeons s’éveillent...");
        Thread.Sleep(500);
        Visuel.TypewriterCentered("Il y a régulièrement de la pluie.");
        Thread.Sleep(700);

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Visuel.TypewriterCentered("🌞 Préparez-vous pour une nouvelle saison d'aventure ! 🌞");

        Console.ResetColor();
        Console.WriteLine("\n\n");
        Console.ForegroundColor = ConsoleColor.Gray;
        Visuel.PrintCentered("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }
}




