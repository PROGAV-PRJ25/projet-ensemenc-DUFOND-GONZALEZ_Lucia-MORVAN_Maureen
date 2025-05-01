void LancerJeu()
{
    Console.Clear();
    Console.WriteLine("\n🫐🪻🍇🌷🌸🌺🪷🌹🍓🍒🥕🍊🏵️🌻🍋🌼🍏🥬🌵🌳🌲🌱🌿🍃🍂🍁");
    Console.WriteLine("\nBienvenue à l'ENSemenC, votre potager personnel !");
    Console.WriteLine("Ce jeu a été programmé par Lucia Dufond-Gonzalez & Maureen MORVAN");
    Console.WriteLine("\n🫐🪻🍇🌷🌸🌺🪷🌹🍓🍒🥕🍊🏵️🌻🍋🌼🍏🥬🌵🌳🌲🌱🌿🍃🍂🍁");

    int tour = 0; int nbLignes = 0; int nbColonnes = 0; 
    List<Terrain> terrainsMonde = new List<Terrain>();
    List<Plante> plantesMonde = new List<Plante>();
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
                if(Convert.ToInt32(texte)==1)
                {
                    terrainsMonde = new List<Terrain> {new TerrainSableux(), new TerrainTerreux()};
                    //plantesMonde = new List<Plante> {new Chene(), new Sapin(), new Rhododendron(), new Trefle()};
                }                            
                else{
                    terrainsMonde = new List<Terrain> {new TerrainBoise(), new TerrainHumide()};
                    //plantesMonde = new List<Plante> {new Tulipe(), new Rose(), new Fraise(), new Cerise()};
                }
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
    tour = Math.Clamp(tour, 4, 50);
    nbLignes = Math.Clamp(nbLignes, 4, 20);
    nbColonnes = Math.Clamp(nbColonnes, 4, 20);

    Monde monde = new Monde(nbLignes,nbColonnes,terrainsMonde);

    Console.WriteLine();
    foreach (Terrain elem in terrainsMonde)
    {
        Console.WriteLine(elem);
    }

    
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\nPrêt à jouer ? Appuie sur une touche pour commencer la partie");
    Console.ForegroundColor = ConsoleColor.Black;
    Console.ReadLine(); // attend que l'utilisateur appuie sur une touche pour lancer la simulation
    
    Simulation simulation2 = new Simulation(monde, plantesMonde);
    simulation2.Simuler(monde, tour);
}
//LancerJeu();

List<Terrain> terrainsMonde = new List<Terrain> {new TerrainSableux(), new TerrainTerreux()};
List<Plante> plantesMonde = new List<Plante>();
Monde monde = new Monde(10,10,terrainsMonde);
Simulation simulation2 = new Simulation(monde, plantesMonde);
simulation2.Simuler(monde, 10);