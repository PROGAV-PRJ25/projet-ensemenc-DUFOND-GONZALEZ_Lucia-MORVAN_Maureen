public class Visuel
{
    public Visuel()
    {

    }

    public static void PrintCentered(string text)
    {
        int windowWidth = Console.WindowWidth;
        int padding = (windowWidth - text.Length) / 2;
        Console.WriteLine(new string(' ', Math.Max(padding, 0)) + text);
    }

    public static void PrintCenteredColored(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        PrintCentered(text);
        Console.ResetColor();
    }

    public static void TypewriterCentered(string text, int delay = 40)
    {
        int windowWidth = Console.WindowWidth;
        int padding = (windowWidth - text.Length) / 2;
        Console.Write(new string(' ', Math.Max(padding, 0)));

        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
        Console.WriteLine();
    }

    public static void FinirJeu()
    {
        try { Console.Clear(); } catch { }
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string gardenBorder = "🫐🪻🍇🌷🌸🌺🪷🌹🍓🍒🥕🍊🏵️🌻🍋🌼🍏🥬🌵🌳🌲🌱🌿🍃🍂🍁";

        string[] gardenArt = new string[]
        {
        " __  __               _       _ _                  _      ",
        "|  \\/  | ___ _ __ ___(_)   __| ( ) __ ___   _____ (_)_ __ ",
        "| |\\/| |/ _ \\ '__/ __| |  / _` |/ / _` \\ \\ / / _ \\| | '__|",
        "| |  | |  __/ | | (__| | | (_| | | (_| |\\ V / (_) | | |   ",
        "|_|  |_|\\___|_|  \\___|_|  \\__,_|  \\__,_| \\_/ \\___/|_|_|   ",
        "                                      (_) ___  _   _  /_/                                     ",
        "                                      | |/ _ \\| | | |/ _ \\                                    ",
        "                                      | | (_) | |_| |  __/                                    ",
        "                                     _/ |\\___/ \\__,_|\\___|                                    ",
        "                                    |__/                                                      ",
        };

        // Affichage du haut
        Console.ForegroundColor = ConsoleColor.Green;
        PrintCentered(gardenBorder);
        Console.WriteLine();

        // Art du jardin
        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (string line in gardenArt)
        {
            PrintCentered(line);
            Thread.Sleep(150);
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        TypewriterCentered("Merci d'avoir joué à l'ENSemenC !");
        TypewriterCentered("Nous espérons que vous avez apprécié cultiver avec nous !");
        Console.WriteLine();

        // Affichage du bas
        Console.ForegroundColor = ConsoleColor.Green;
        PrintCentered(gardenBorder);
        Console.ResetColor();
        Thread.Sleep(3000);
    }

    public static void PresenterJeu()
    {
        string border = "🫐🪻🍇🌷🌸🌺🪷🌹🍓🍒🥕🍊🏵️🌻🍋🌼🍏🥬🌵🌳🌲🌱🌿🍃🍂🍁";
        Console.Clear();
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string[] gardenArt = new string[]
        {
            " ____  _                                      ",
            "| __ )(_) ___ _ ____   _____ _ __  _   _  ___ ",
            "|  _ \\| |/ _ \\ '_ \\ \\ / / _ \\ '_ \\| | | |/ _ \\",
            "| |_) | |  __/ | | \\ V /  __/ | | | |_| |  __/",
            "|____/|_|\\___|_| |_|\\_/ \\___|_| |_|\\__,_|\\___|",
        };

        PrintCentered(border);

        // Affichage de Bienvenue
        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (string line in gardenArt)
        {
            PrintCentered(line);
            Thread.Sleep(150);
        }
        Console.WriteLine();
        PrintCentered("Bienvenue à l'ENSemenC, votre potager personnel !");
        PrintCentered("Ce jeu a été programmé par Lucia Dufond-Gonzalez & Maureen MORVAN");
        Console.WriteLine();
        PrintCentered(border); Console.ForegroundColor = ConsoleColor.White;
    }


    // *****************************  VISUELS POUR L'ANNONCE DES SAISONS *****************************


    // ************* VISUEL PRINTEMPS ***************
    public static void AnnoncerPrintemps()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // En-tête décoratif
        PrintCenteredColored("🌸 Le Printemps Arrive 🌸", ConsoleColor.Green);
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
            PrintCentered(line);
            Thread.Sleep(150);
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;

        // Texte machine à écrire
        TypewriterCentered("Les bourgeons s’éveillent...");
        Thread.Sleep(500);
        TypewriterCentered("Il y a régulièrement de la pluie.");
        Thread.Sleep(700);

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        TypewriterCentered("🌞 Préparez-vous pour une nouvelle saison d'aventure ! 🌞");

        Console.ResetColor();
        Console.WriteLine("\n\n");
        Console.ForegroundColor = ConsoleColor.Gray;
        PrintCentered("Appuyez sur une touche pour continuer...");
        Console.ReadKey();
    }






    // ************************************************************* VISUEL METEO *************************************************************
    public static void AfficherAnimationPluie()
    {

        string[] pluie = new string[]
        {
        "   ☁️     ☁️     ☁️     ☁️     ☁️     ☁️",
        "      💧     💧     💧      💧      ",
        "  💧     💧      💧    💧      💧   ",
        "     💧     💧      💧      💧    💧  "
        };


        for (int i = 0; i < pluie.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(pluie[i]);
        }
        Thread.Sleep(500);

        Console.ResetColor();
    }

    public static void AfficherAnimationSoleil()
    {
        string[] soleil = new string[]
        {
        "        ☁️                 ☁️             ",
        "           ☁️        ☀️       ☁️        ",
        "     ☁️             ☁️           ",
        "          ☁️     ☁️         ☁️     ",
        };

        for (int i = 0; i < soleil.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(soleil[i]);
        }

        Thread.Sleep(500);
        Console.ResetColor();
    }
}