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
        TypewriterCentered("Bienvenue à l'ENSemenC, votre potager personnel !");
        // Thread.Sleep(250);
        TypewriterCentered("Ce jeu a été programmé par Lucia Dufond-Gonzalez & Maureen MORVAN");
        // Thread.Sleep(250);
        Console.WriteLine();
        PrintCentered(border); Console.ForegroundColor = ConsoleColor.White;
    }


    // *****************************  VISUELS POUR L'ANNONCE DES SAISONS *****************************

    // ************* VISUEL PRINTEMPS ***************
    public static void AnnoncerPrintemps()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // En-tête décoratif
        PrintCenteredColored("🌸 Le Printemps arrive 🌸", ConsoleColor.Green);
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
    }

    public static void AnnoncerEte()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        PrintCenteredColored("☀️  L'Été brule ☀️", ConsoleColor.DarkYellow);
        Console.WriteLine();

        string[] sunArt = new string[]
        {
            "      \\   |   /      ",
            "        .-*-._       ",
            "     / /     \\ \\     ",
            "    | |  ☀️  | |    ",
            "     \\ \\_____/ /     ",
            "        `-.-'        ",
            "      /   |   \\      ",
        };

        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (string line in sunArt)
        {
            PrintCentered(line);
            Thread.Sleep(150);
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;

        TypewriterCentered("Le soleil est au zénith...");
        Thread.Sleep(500);
        TypewriterCentered("La chaleur assèche les sols rapidement !");
        Thread.Sleep(700);

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        TypewriterCentered("🔥 Pensez à bien arroser vos plantes ! 🔥");
    }

    public static void AnnoncerAutomne()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        PrintCenteredColored("🍁 L’Automne s’installe 🍁", ConsoleColor.DarkRed);
        Console.WriteLine();

        string[] leafArt = new string[]
        {
            "    🍂     🍁      🍂  ",
            "       🍁      🍂       ",
            "   🍁    🌰     🍂     ",
            "     🍂      🍁     🍁  ",
            "         🍁     🌰      ",
            "    🍁      🍂      🍂  ",
        };

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        foreach (string line in leafArt)
        {
            PrintCentered(line);
            Thread.Sleep(150);
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;

        TypewriterCentered("Les feuilles tombent doucement...");
        Thread.Sleep(500);
        TypewriterCentered("Le vent se lève, les températures baissent.");
        Thread.Sleep(700);

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        TypewriterCentered("🍃 Il est temps de renforcer votre potager ! 🍃");
    }

    public static void AnnoncerHiver()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        PrintCenteredColored("❄️ L’Hiver Est Là ❄️", ConsoleColor.Cyan);
        Console.WriteLine();

        string[] snowArt = new string[]
        {
            "      *     *     *      ",
            "   *     ❄️     *      ",
            " *    *     *     *    * ",
            "   ❄️     *     ❄️     ",
        };

        Console.ForegroundColor = ConsoleColor.White;
        foreach (string line in snowArt)
        {
            PrintCentered(line);
            Thread.Sleep(150);
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;

        TypewriterCentered("Le froid s’installe dans le jardin...");
        Thread.Sleep(500);
        TypewriterCentered("La neige menace vos cultures fragiles.");
        Thread.Sleep(700);

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Blue;
        TypewriterCentered("❄️ Protégez vos plantations du gel ! ❄️");
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
        Console.ResetColor();
    }
}