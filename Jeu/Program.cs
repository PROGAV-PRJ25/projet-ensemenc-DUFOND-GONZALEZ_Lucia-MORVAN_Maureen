void LancerJeu()
{
    Visuel.PresenterJeu();
    AfficherRegles();
    Console.ReadLine();

    // Initialiser paramètres de la partie
    int tour = 0; int nbLignes = 0; int nbColonnes = 0;
    List<Terrain> terrainsMonde = new List<Terrain>();
    List<string> plantesMonde = new List<string>();
    List<string> animauxMonde = new List<string>();

    bool entreeValide = false;

    ChoisirMode();

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("\nCombien de jours voulez-vous que votre partie dure : ");
    Console.ForegroundColor = ConsoleColor.White;
    do
    {
        string texte = Console.ReadLine()!;
        try
        {
            tour = Convert.ToInt32(texte);
            entreeValide = true;
        }
        catch
        {
            Console.WriteLine("Veuillez entrer un nombre entier valide.");
        }
    }
    while (!entreeValide);

    Console.WriteLine("\n1 - La terre brûlée 🏜️");
    Console.WriteLine("2 - La forêt enchantée 🌲");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("Entrez le numéro du monde dans lequel vous souhaitez jouer : ");
    Console.ForegroundColor = ConsoleColor.White;
    entreeValide = false;
    do
    {
        string texte = Console.ReadLine()!;
        try
        {
            if (Convert.ToInt32(texte) == 1 || Convert.ToInt32(texte) == 2)
            {
                entreeValide = true;
                if (Convert.ToInt32(texte) == 1)
                {
                    terrainsMonde = new List<Terrain> { new TerrainSableux(), new TerrainTerreux(), new TerrainTranchee(), new TerrainEpouvantail() };
                    plantesMonde = new List<string> { "Tulipe", "Rose", "Fraise", "Cerise" };
                    animauxMonde = new List<string> { "Renard" };
                }
                else
                {
                    terrainsMonde = new List<Terrain> { new TerrainBoise(), new TerrainHumide(), new TerrainTranchee(), new TerrainEpouvantail() };
                    plantesMonde = new List<string> { "Noisetier", "Sapin", "Rhododendron", "Trefle" };
                    animauxMonde = new List<string> { "Ecureuil" };
                }
            }
            else Console.WriteLine("Veuillez entrer un nombre entier valide.");
        }
        catch
        {
            Console.WriteLine("Veuillez entrer un nombre entier valide.");
        }
    }
    while (!entreeValide);

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("Entrez la hauteur souhaitée pour votre potagé (entre 4 et 20) : ");
    Console.ForegroundColor = ConsoleColor.White;
    entreeValide = false;
    do
    {
        string texte = Console.ReadLine()!;
        try
        {
            nbLignes = Convert.ToInt32(texte);
            entreeValide = true;
        }
        catch
        {
            Console.WriteLine("Veuillez entrer un nombre entier valide.");
        }
    }
    while (!entreeValide);

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write("Entrez la longueur souhaitée pour votre potagé (entre 4 et 20) : ");
    Console.ForegroundColor = ConsoleColor.White;
    entreeValide = false;
    do
    {
        string texte = Console.ReadLine()!;
        try
        {
            nbColonnes = Convert.ToInt32(texte);
            entreeValide = true;
        }
        catch
        {
            Console.WriteLine("Veuillez entrer un nombre entier valide.");
        }
    }
    while (!entreeValide);

    // Ajustements des valeurs min et max
    tour = Math.Clamp(tour, 4, 50);
    nbLignes = Math.Clamp(nbLignes, 4, 20);
    nbColonnes = Math.Clamp(nbColonnes, 4, 20);

    Monde monde = new Monde(nbLignes, nbColonnes, plantesMonde, terrainsMonde, animauxMonde);

    Console.WriteLine();
    foreach (Terrain elem in terrainsMonde)
    {
        Console.WriteLine(elem);
    }


    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\nPrêt à jouer ? Appuie sur une touche pour commencer la partie.");
    Console.ForegroundColor = ConsoleColor.Black;
    Console.ReadLine(); // attend que l'utilisateur appuie sur une touche pour lancer la simulation

    Simulation simulation = new Simulation(monde);
    simulation.Simuler(monde, tour);

    Visuel.FinirJeu();
}


void AfficherRegles()
{
    string regles = @"📖 Voici les règles du jeu :

    Votre mission ? Récolter un maximum de plantes avant la fin de la partie !
    Pour cela, vous pouvez réaliser une action par jour (semer, arroser, desherber, mettre de l'engrais, etc ...).
    Quel que soit le monde, 4 plantes sont à votre disposition, chacune avec des caractéristiques spécifiques.
    Pour pousser, les plantes ont besoin que 50% de leurs besoins soient satisfaits, à savoir :
    Le taux de luminosité, le taux d'humidité, la santé, le terrain préféré et la fertilité de celui-ci (au moins 50%).
    
    Mais attention ! Comme dans la vraie vie, la météo change, les saisons défilent, et les terrains en subissent les conséquences.
    De temps en temps, vous devrez faire face à des péripéties : intempéries, animaux intrus, maladies...
    
    🎯 Votre objectif : Gérer intelligemment votre jardin pour en tirer le meilleur rendement possible, tout en faisant face aux imprévus.
    🌼 Bonne chance, jardinier ! ";
    Console.WriteLine($"\n\n{regles}\n");
}

bool ChoisirMode()
{
    int selection = 0; // 0 = Facile, 1 = Difficile
    ConsoleKeyInfo key;

    int largeurConsole = Console.WindowWidth;
    int positionCentrale = largeurConsole / 2;

    do
    {
        Console.Clear();
        Console.WriteLine("\n\nUtilise les flèches ← → pour choisir un mode, puis Entrée pour valider.\n");

        string optionGauche = "Facile";
        string optionDroite = "Difficile";

        // Calcul du positionnement
        int totalLargeur = optionGauche.Length + optionDroite.Length + 10; // padding
        int debutAffichage = Math.Max(0, positionCentrale - totalLargeur / 2);

        Console.SetCursorPosition(debutAffichage, Console.CursorTop);

        if (selection == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"[ {optionGauche} ]");
            Console.ResetColor();
            Console.Write("     ");
            Console.Write($"  {optionDroite}  ");
        }
        else
        {
            Console.Write($"  {optionGauche}  ");
            Console.Write("     ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"[ {optionDroite} ]");
            Console.ResetColor();
        }

        key = Console.ReadKey(true);

        if (key.Key == ConsoleKey.RightArrow)
            selection = 1;
        else if (key.Key == ConsoleKey.LeftArrow)
            selection = 0;

    } while (key.Key != ConsoleKey.Enter);

    Console.Clear();
    string choix = selection == 0 ? "Facile" : "Difficile";
    Console.WriteLine($"\n\nTu as choisi le mode : {choix} 🎮");
    if (choix == "Difficile") { return true; }
    else
        return false;

}
//LancerJeu();

List<Terrain> terrainsMonde = new List<Terrain> { new TerrainSableux(), new TerrainTerreux() , new TerrainTranchee(), new TerrainEpouvantail()};
List<string> plantesMonde = new List<string> { "Tulipe", "Rose", "Fraise", "Cerise" };
List<string> animauxMonde = new List<string> { "Renard" };
Monde monde = new Monde(10, 10, plantesMonde, terrainsMonde, animauxMonde);
Simulation simulation2 = new Simulation(monde);
simulation2.Simuler(monde, 10);