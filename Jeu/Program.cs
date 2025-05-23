void LancerJeu()
{
    //Visuel.PresenterJeu();
    //AfficherRegles();
    ChoisirModeDifficile();
    Thread.Sleep(1000);

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
    Console.Write("Entrez la hauteur souhaitée pour votre potagé (entre 8 et 20) : ");
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
    Console.Write("Entrez la longueur souhaitée pour votre potagé (entre 8 et 20) : ");
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
    nbLignes = Math.Clamp(nbLignes, 8, 20);
    nbColonnes = Math.Clamp(nbColonnes, 8, 20);

    Monde monde = new Monde(nbLignes, nbColonnes, plantesMonde, terrainsMonde, animauxMonde);

    Console.WriteLine("\nListes des terrains du jeu :");
    foreach (Terrain elem in terrainsMonde)
    {
        Console.WriteLine(elem);
    }

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\nPrêt à jouer ? Appuie sur une touche pour commencer la partie.");
    Console.ForegroundColor = ConsoleColor.Black;
    Console.ReadLine(); // attend que l'utilisateur appuie sur une touche pour lancer la simulation

    Simulation simulation = new Simulation(monde);


    // ****************** TEST AFFICHAGE PLANTES MALADES ******************
    Tulipe t1 = new Tulipe(monde, 5, 7);
    monde.AjouterPlante(t1, t1.xPlante, t1.yPlante, false);
    t1.maladie = true;

    Tulipe t2 = new Tulipe(monde, 8, 7);
    monde.AjouterPlante(t2, t2.xPlante, t2.yPlante, false);
    t2.maladie = true;

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
    Ps: Dans le mode difficile, les intempéries sont plus courantes.
    
    🎯 Votre objectif : Gérer intelligemment votre jardin pour en tirer le meilleur rendement possible, tout en faisant face aux imprévus.
    🌼 Bonne chance, jardinier ! ";
    Console.WriteLine($"\n\n{regles}\n");
}

void ChoisirModeDifficile()
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
    Thread.Sleep(2000);
    if (choix == "Difficile")
    {
        Simulation.modeDifficile = true;
    }
}

LancerJeu();

// TEST CREATION DES CLASSES & SIMULATION 

// List<Terrain> terrainsMonde = new List<Terrain> { new TerrainSableux(), new TerrainTerreux(), new TerrainTranchee(), new TerrainEpouvantail() };
// List<string> plantesMonde = new List<string> { "Tulipe", "Rose", "Fraise", "Cerise" };
// List<string> animauxMonde = new List<string> { "Renard" };
// Monde monde = new Monde(10, 10, plantesMonde, terrainsMonde, animauxMonde);
// Simulation simulation2 = new Simulation(monde);
// simulation2.Simuler(monde, 10);


// TEST RAPPORT AFFICHAGE
// Terre brulee

// List<Terrain> terrainsMonde = new List<Terrain> { new TerrainSableux(), new TerrainTerreux() , new TerrainTranchee(), new TerrainEpouvantail()};
// List<string> plantesMonde = new List<string> { "Tulipe", "Rose", "Fraise", "Cerise" };
// List<string> animauxMonde = new List<string> { "Renard" };
// Monde monde = new Monde(10, 10, plantesMonde, terrainsMonde, animauxMonde);

// Tulipe t1 = new Tulipe(monde, 5, 7);
// monde.AjouterPlante(t1, t1.xPlante, t1.yPlante, false);
// t1.EtapeCroissance = 3;

// Rose r1 = new Rose(monde, 2, 8);
// monde.AjouterPlante(r1, r1.xPlante, r1.yPlante, false);
// r1.EtapeCroissance = 3;

// Fraise f1 = new Fraise(monde, 1, 1);
// monde.AjouterPlante(f1, f1.xPlante, f1.yPlante, false);
// f1.EtapeCroissance = 3;
// Fraise f2 = new Fraise(monde, 1, 2);
// monde.AjouterPlante(f2, f2.xPlante, f2.yPlante, false);
// f2.EtapeCroissance = 2;
// Fraise f3 = new Fraise(monde, 2, 1);
// monde.AjouterPlante(f3, f3.xPlante, f3.yPlante, false);
// f3.EtapeCroissance = 1;

// Cerise c1 = new Cerise(monde, 7, 2);
// monde.AjouterPlante(c1, c1.xPlante, c1.yPlante, false);
// c1.EtapeCroissance = 3;

// Saison saison = new Saison(monde);
// MeteoEte meteo = new MeteoEte(monde);

// Renard renard = new Renard(monde, 9, 9);
// monde.grilleAnimal[renard.coorX, renard.coorY] = renard;

// monde.AfficherGrille(meteo);


// Forêt enchantée 

// List<Terrain> terrainsMonde = new List<Terrain> { new TerrainBoise(), new TerrainHumide(), new TerrainTranchee(), new TerrainEpouvantail() };
// List<string> plantesMonde = new List<string> { "Noisetier", "Sapin", "Rhododendron", "Trefle" };
// List<string> animauxMonde = new List<string> { "Ecureuil" };
// Monde monde = new Monde(10, 10, plantesMonde, terrainsMonde, animauxMonde);

// Sapin s1 = new Sapin(monde, 3, 8);
// monde.AjouterPlante(s1, s1.xPlante, s1.yPlante, false);
// s1.EtapeCroissance = 3;

// Noisetier n1 = new Noisetier(monde, 1, 5);
// monde.AjouterPlante(n1, n1.xPlante, n1.yPlante, false);
// n1.EtapeCroissance = 3;

// Trefle t1 = new Trefle(monde, 4, 2);
// monde.AjouterPlante(t1, t1.xPlante, t1.yPlante, false);
// t1.EtapeCroissance = 3;

// Rhododendron r1 = new Rhododendron(monde, 8, 5);
// monde.AjouterPlante(r1, r1.xPlante, r1.yPlante, false);
// r1.EtapeCroissance = 3;
// Rhododendron r2 = new Rhododendron(monde, 7, 5);
// monde.AjouterPlante(r2, r2.xPlante, r2.yPlante, false);
// r2.EtapeCroissance = 2;
// Rhododendron r3 = new Rhododendron(monde, 8, 4);
// monde.AjouterPlante(r3, r3.xPlante, r3.yPlante, false);
// r3.EtapeCroissance = 1;

// Saison saison = new Saison(monde);
// Meteo meteo = new MeteoHiver(monde);

// Ecureuil ecureuil = new Ecureuil(monde, 0, 9);
// monde.grilleAnimal[ecureuil.coorX, ecureuil.coorY] = ecureuil;

// Ecureuil ecureuil2 = new Ecureuil(monde, 0, 0);
// monde.grilleAnimal[ecureuil2.coorX, ecureuil2.coorY] = ecureuil2;

// monde.grilleTerrain[0, 8] = terrainsMonde[2];   // Creuser une tranchée
// monde.grilleTerrain[1, 9] = terrainsMonde[2];
// ecureuil.SeDeplacerAlea(); // L'animal se déplace sur la (1,8) car c'est la seule à laquelle il a accès

// monde.grilleTerrain[7, 7] = terrainsMonde[3];   // Ajout d'épouventail
// monde.grilleTerrain[2, 2] = terrainsMonde[3];

// monde.AfficherGrille(meteo);


