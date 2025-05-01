void LancerJeu()
{
    Console.WriteLine("\n🫐🪻🍇🌷🌸🌺🪷🌹🍓🍒🥕🍊🏵️🌻🍋🌼🍏🥬🌵🌳🌲🌱🌿🍃🍂🍁");
    Console.WriteLine("\nBienvenue à l'ENSemenC, votre potager personnel !");
    Console.WriteLine("Ce jeu a été programmé par Lucia Dufond-Gonzalez & Maureen MORVAN");
    Console.WriteLine("\n🫐🪻🍇🌷🌸🌺🪷🌹🍓🍒🥕🍊🏵️🌻🍋🌼🍏🥬🌵🌳🌲🌱🌿🍃🍂🍁");

    int tour = 0; int nbLignes = 0; int nbColonnes = 0; 
    List<Terrain> terrainsMonde = new List<Terrain>();
    bool entreeValide = false;

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("\nCombien de jours voulez-vous que votre partie dure ?");
    Console.ForegroundColor = ConsoleColor.White;
    do{
        string texte = Console.ReadLine()!;
        try{
            tour = Convert.ToInt32(texte);
            entreeValide = true;
        }
        catch{
            Console.WriteLine("Veuillez entrer un nombre entier valide.");
        }
    }
    while(!entreeValide);

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("\nEntrez le numéro du monde dans lequel vous souhaitez jouer :");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("1 - La terre brûlée 🏜️");
    Console.WriteLine("2 - La forêt enchantée 🌲");
    entreeValide = false;
    do{
        string texte = Console.ReadLine()!;
        try{
            if(Convert.ToInt32(texte)==1 || Convert.ToInt32(texte)==2)
            {
                entreeValide = true;
                if(Convert.ToInt32(texte)==1) terrainsMonde = new List<Terrain> {new TerrainSableux(), new TerrainTerreux()};
                else if(Convert.ToInt32(texte)==2) terrainsMonde = new List<Terrain> {new TerrainBoise(), new TerrainHumide()};
            }
            else Console.WriteLine("Veuillez entrer un nombre entier valide.");
        }
        catch{
            Console.WriteLine("Veuillez entrer un nombre entier valide.");
        }
    }
    while(!entreeValide);

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("\nEntrez la hauteur souhaitée pour votre potagé");
    Console.ForegroundColor = ConsoleColor.White;
    entreeValide = false;
    do{
        string texte = Console.ReadLine()!;
        try{
            nbLignes = Convert.ToInt32(texte);
            entreeValide = true;          
        }
        catch{
            Console.WriteLine("Veuillez entrer un nombre entier valide.");
        }
    }
    while(!entreeValide);

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("\nEntrez la longueur souhaitée pour votre potagé");
    Console.ForegroundColor = ConsoleColor.White;
    entreeValide = false;
    do{
        string texte = Console.ReadLine()!;
        try{
            nbColonnes = Convert.ToInt32(texte);
            entreeValide = true;          
        }
        catch{
            Console.WriteLine("Veuillez entrer un nombre entier valide.");
        }
    }
    while(!entreeValide);

    // Ajustements des valeurs min et max
    tour = Math.Clamp(tour, 2, 40);
    nbLignes = Math.Clamp(nbLignes, 4, 20);
    nbColonnes = Math.Clamp(nbColonnes, 4, 20);

    Monde monde = new Monde(nbLignes,nbColonnes,terrainsMonde);

    Console.WriteLine();
    foreach (Terrain elem in terrainsMonde)
    {
        Console.WriteLine(elem);
    }
    System.Threading.Thread.Sleep(3000);

    
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\nPrêt à jouer ? Appuie sur une touche pour commencer la partie");
    Console.ForegroundColor = ConsoleColor.Black;
    Console.ReadLine(); // attend que l'utilisateur appuie sur une touche pour lancer la simulation
    
    Simulation simulation2 = new Simulation(monde);
    simulation2.Simuler(monde, tour);
}
LancerJeu();



// Plante plante1 = new Plante(monde, 2, 2);
// monde.AjouterPlante(plante1, plante1.xPlante, plante1.yPlante);
// 
// monde.AfficherGrille();
// Console.WriteLine("Faire pousser la plante au centre");
// plante1.Croitre();
// plante1.Croitre();
// //plante1.Croitre();
// //plante1.Croitre(); 

// monde.AfficherGrille();

// Rhododendron plante2 = new Rhododendron(monde, 1, 1);
// monde.AjouterPlante(plante2, plante2.xPlante, plante2.yPlante);
// /* plante2.SePropager(); // Pas censée se propager à ce moment
// monde.AfficherGrille();
// plante2.Croitre();
// plante2.SePropager(); // Peut se propager à ce moment là

// monde.AfficherGrille(); */