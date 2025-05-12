void LancerJeu()
{
    Visuel.PresenterJeu();
    AfficherRegles();

    // Initialiser paramètres de la partie
    int tour = 0; int nbLignes = 0; int nbColonnes = 0;
    List<Terrain> terrainsMonde = new List<Terrain>();
    List<string> plantesMonde = new List<string>();
    List<string> animauxMonde = new List<string>();
    bool entreeValide = false;

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
                    terrainsMonde = new List<Terrain> { new TerrainSableux(), new TerrainTerreux() };
                    plantesMonde = new List<string> { "Tulipe", "Rose", "Fraise", "Cerise" };
                    animauxMonde = new List<string> { "Renard" };
                }
                else
                {
                    terrainsMonde = new List<Terrain> { new TerrainBoise(), new TerrainHumide() };
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

    Monde monde = new Monde(nbLignes, nbColonnes, terrainsMonde, animauxMonde);

    Console.WriteLine();
    foreach (Terrain elem in terrainsMonde)
    {
        Console.WriteLine(elem);
    }


    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\nPrêt à jouer ? Appuie sur une touche pour commencer la partie.");
    Console.ForegroundColor = ConsoleColor.Black;
    Console.ReadLine(); // attend que l'utilisateur appuie sur une touche pour lancer la simulation

    Simulation simulation = new Simulation(monde, plantesMonde);
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


//LancerJeu();

List<Terrain> terrainsMonde = new List<Terrain> { new TerrainSableux(), new TerrainTerreux() };
List<string> plantesMonde = new List<string> { "Tulipe", "Rose", "Fraise", "Cerise" };
List<string> animauxMonde = new List<string> { "Renard" };
Monde monde = new Monde(10, 10, terrainsMonde, animauxMonde);
Simulation simulation2 = new Simulation(monde, plantesMonde);
simulation2.Simuler(monde, 10);