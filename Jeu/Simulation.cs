using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

public class Simulation
{
    public Monde monde { get; private set; }
    // Ajout des saisons
    public Saison saison { get; set; }
    private bool exit = false; // Variable qui permet de quitter le jeu pendant la partie
    public static bool modeUrgence = false;
    public static int jour;
    private int[] coordonnees = [0, 0];

    public static bool modeDifficile = false;

    int actionsRestantes;

    public Simulation(Monde unMonde)
    {
        monde = unMonde;
        saison = new Saison(monde);
    }

    public void Simuler(Monde monde, int tour)
    {
        for (int i = 1; i <= tour; i++)
        {
            if (!exit)
            {
                jour = i;
                saison.DeterminerSaison();
                saison.AnnoncerSaison();

                Console.Clear();

                // Maureen: Est-ce qu'on garde??
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nSemaine {i}\n");
                Console.ForegroundColor = ConsoleColor.White;

                // Météo du jour
                saison.meteo.DeterminerCatastropheEtVariables();
                saison.meteo.Pleuvoir(); // La météo change selon la saison
                Console.WriteLine();


                if (modeUrgence) // TO DO : mettre condition mode urgence
                {
                    actionsRestantes = 3;
                    while (actionsRestantes > 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"   Jour {jour}");
                        Console.ForegroundColor = ConsoleColor.White;
                        monde.AfficherGrille(saison.meteo);
                        ProposerActionJoueur();
                        actionsRestantes--;
                    }
                    modeUrgence = false;
                }
                else
                {
                    monde.AfficherGrille(saison.meteo);
                    ProposerActionJoueur();
                }

                foreach (var plante in monde.listePlante)
                {
                    plante.Croitre(monde, saison.meteo);
                    InitierMaladie(saison, plante);
                }

                for (int x = monde.listePlante.Count - 1; x >= 0; x--)
                {
                    if (monde.listePlante[x].estMorte)
                    {
                        monde.grillePlante![monde.listePlante[x].xPlante, monde.listePlante[x].yPlante] = null!;
                        monde.listePlante.RemoveAt(x);
                    }
                }

                foreach (var plante in monde.listePlante.ToList())
                {
                    if (!plante.estMorte && plante is PlanteEnvahissante envahissante)
                    {
                        envahissante.SePropager(); // La fonction ajoute directement la nouvelle plante à ListePlante
                    }
                }

                foreach (var animal in monde.listeAnimal.ToList())
                {
                    animal.SeDeplacerAlea();
                }
                monde.AjouterAnimal(saison, monde);

                saison.temps++; // Un jour s'est écoulé

                if (!exit)
                {
                    Console.WriteLine("\nAppuyer sur une Entree pour continuer");
                    Console.ReadLine();
                }
            }
        }
        FinirPartie();
    }

    public int ProposerActionJoueurAvecFleche()
    {
        int choixAction = 0;
        List<string> listeActions = new List<string>{
                "1 - Semer",
                "2 - Arroser",
                "3 - Mettre de l'engrais",
                "4 - Deherber",
                "5 - Traiter",
                "6 - Recolter",
                "7 - Faire fuir animal",
                "8 - Passer la journée",
                "9 - Quitter la partie"
            };

        foreach (string action in listeActions)
        {
            Console.WriteLine(listeActions);
        }

        if (modeUrgence)
        {
            // Actions particulières en cas d'urgence
            listeActions.Add("10 - Creuser une tranchée");
            listeActions.Add("11 - Installer un épouventail");
        }

        ConsoleKey key;
        do
        {
            Console.Clear();
            if (modeUrgence)
            {
                Console.ForegroundColor = ConsoleColor.Red; 
                Console.WriteLine("\n⚠️ MODE URGENCE ACTIVÉ ! Vous avez 3 actions pour protéger votre potager !\n");
                saison.meteo.AfficherEvenement();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\n👉 Action(s) restante(s) : {actionsRestantes}\n");
            }

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine($"   Jour {jour}");
            Console.ForegroundColor = ConsoleColor.White;
            monde.AfficherGrille(saison.meteo);
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("Quelle action souhaitez-vous effectuer ? (Utilisez ↑ ↓ puis Entrée)\n");
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < listeActions.Count; i++)
            {

                if (i == choixAction)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(listeActions[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(listeActions[i]);
                }
            }

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (choixAction == 0)
                    {
                        choixAction = listeActions.Count - 1;
                    }
                    else
                        choixAction -= 1;
                    break;
                case ConsoleKey.DownArrow:
                    if ((choixAction + 1) == listeActions.Count)
                    {
                        choixAction = 0;
                    }
                    else
                        choixAction += 1;

                    break;
            }

        } while (key != ConsoleKey.Enter);

        return choixAction + 1;
    }

    public void ProposerActionJoueur()
    {
        int action = ProposerActionJoueurAvecFleche(); // Récupére l'action choisie par le joueur
        if (modeUrgence && (action == 10 || action == 11))
        {
            ChoisirCoordonneesAvecFleches();
            switch (action)
            {
                case 10:
                    monde.CreuserTranchee(coordonnees[0], coordonnees[1]);
                    break;
                case 11:
                    monde.InstallerEpouventail(coordonnees[0], coordonnees[1]);
                    break;
            }
        }
        switch (action)
        {
            case 1:
                ChoisirPlante();
                break;
            case 2:
                ChoisirCoordonneesAvecFleches();
                monde.ArroserTerrain(coordonnees[0], coordonnees[1]);
                break;
            case 3:
                ChoisirCoordonneesAvecFleches();
                monde.DeposerEngrais(coordonnees[0], coordonnees[1]);
                break;
            case 4:
                ChoisirCoordonneesAvecFleches();
                monde.Desherber(coordonnees[0], coordonnees[1]);
                break;
            case 5:
                Console.WriteLine("Liste des plantes malades :");
                int cptMalade = 0;
                foreach (var plante in monde.listePlante)
                {
                    if (plante.maladie)
                    {
                        Console.WriteLine($"- ({plante.xPlante + 1},{plante.yPlante + 1})");
                        cptMalade++;
                    }
                }
                if (cptMalade > 0)
                {
                    do
                    {
                        ChoisirCoordonneesAvecFleches();
                    }
                    while (monde.grillePlante![coordonnees[0], coordonnees[1]] == null);
                    monde.TraiterPlante(coordonnees[0], coordonnees[1]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Il n'y a aucune plante à traiter pour l'instant...");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                break;
            case 6:
                Console.WriteLine("Liste des plantes à récolter :");
                int cptRecolte = 0;
                foreach (var plante in monde.listePlante)
                {
                    if (plante.EtapeCroissance == 3)
                    {
                        Console.WriteLine($"- ({plante.xPlante + 1},{plante.yPlante + 1})");
                        cptRecolte++;
                    }
                }
                if (cptRecolte > 0) // Seulement quand il y a quelque chose à récolter 
                {
                    do
                    {
                        ChoisirCoordonneesAvecFleches();
                    }
                    while (monde.grillePlante![coordonnees[0], coordonnees[1]] == null);
                    monde.Recolter(coordonnees[0], coordonnees[1]);
                    AfficherRecolte();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Il n'y a aucune plante à récolter pour l'instant...");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                break;
            case 7:
                ChoisirCoordonneesAvecFleches();
                monde.FaireFuirAnimal(coordonnees[0], coordonnees[1]);
                break;
            case 8:
                break; 
            case 9:
                exit = true;
                break;
        }
    }

    public int ChoisirPlanteAvecFleches()
    {
        int choixTypePlante = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"   Jour {jour}");
            Console.ForegroundColor = ConsoleColor.White;
            monde.AfficherGrille(saison.meteo);
            Console.WriteLine("Quelle plante souhaitez-vous semer ? (Utilisez ↑ ↓ puis Entrée)\n");

            for (int i = 0; i < monde.plantesPossible.Count; i++)
            {
                Type type = Type.GetType(monde.plantesPossible[i])!;
                Plante planteTemp = (Plante)Activator.CreateInstance(type, monde, 0, 0)!;

                if (i == choixTypePlante)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"> {i + 1}. {monde.plantesPossible[i]} {planteTemp.ToString()}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {i + 1}. {monde.plantesPossible[i]} {planteTemp.ToString()}");
                }
            }

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    choixTypePlante = (choixTypePlante - 1 + monde.plantesPossible.Count) % monde.plantesPossible.Count;
                    break;
                case ConsoleKey.DownArrow:
                    choixTypePlante = (choixTypePlante + 1) % monde.plantesPossible.Count;
                    break;
            }

        } while (key != ConsoleKey.Enter);

        return choixTypePlante + 1; // Correspond de l'indice
    }

    public void ChoisirPlante()
    {
        Console.WriteLine();
        int numPlante = ChoisirPlanteAvecFleches();
        Type typePlante = Type.GetType(monde.plantesPossible[numPlante - 1])!;
        PlacerPlanteAvecFleches(typePlante);
    }

    public void ChoisirCoordonneesAvecFleches()
    {
        int x = monde.ligne / 2;
        int y = monde.colonne / 2;

        ConsoleKey key;
        bool coordonneesChoisies = false;

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"   Jour {jour}");
            Console.ForegroundColor = ConsoleColor.White;

            // Affichage météo
            if (saison.meteo.estEnTrainDePleuvoir)
                Visuel.AfficherAnimationPluie();
            else
                Visuel.AfficherAnimationSoleil();

            Console.WriteLine();

            // Affichage des numéros de colonnes
            Console.Write("\n   ");
            for (int i = 1; i <= monde.colonne; i++)
            {
                Console.Write(i < 10 ? $" {i}" : $"{i}");
            }
            Console.WriteLine();

            // Affichage de la grille avec le curseur
            for (int i = 0; i < monde.ligne; i++)
            {
                // Numéro de ligne
                Console.Write(i < 9 ? $" {i + 1} " : $"{i + 1} ");

                for (int j = 0; j < monde.colonne; j++)
                {
                    if (i == x && j == y)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("X ");
                        Console.ResetColor();
                    }
                    else if (monde.grillePlante?[i, j] != null)
                        Console.Write(monde.grillePlante[i, j].AfficherVisuel());
                    else if (monde.grilleAnimal?[i, j] != null)
                        Console.Write(monde.grilleAnimal[i, j].visuelAnimal);
                    else
                        Console.Write(monde.grilleTerrain[i, j].visuelTerrain);
                }

                // Affichage météo sur la droite
                monde.AfficherMeteo(i, saison.meteo);
            }

            // Affichage des types de terrain (une seule fois, en bas)
            Console.WriteLine();
            List<Terrain> terrainsModifies = new List<Terrain>();
            for (int i = 0; i < monde.ligne; i++)
            {
                for (int j = 0; j < monde.colonne; j++)
                {
                    Terrain terrain = monde.grilleTerrain[i, j];
                    if (!terrainsModifies.Contains(terrain))
                    {
                        Console.WriteLine(terrain.ToString());
                        terrainsModifies.Add(terrain);
                    }
                }
            }

            // Instructions utilisateur
            Console.WriteLine();
            Console.WriteLine("Utilisez les flèches pour déplacer le curseur, Entrée pour choisir, Échap pour annuler.");

            // Lecture touche
            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (x > 0) x--;
                    break;
                case ConsoleKey.DownArrow:
                    if (x < monde.ligne - 1) x++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (y > 0) y--;
                    break;
                case ConsoleKey.RightArrow:
                    if (y < monde.colonne - 1) y++;
                    break;
                case ConsoleKey.Enter:
                    coordonnees = new int[] { x, y };
                    coordonneesChoisies = true;
                    break;
                case ConsoleKey.Escape:
                    coordonneesChoisies = true;
                    coordonnees = new int[] { -1, -1 }; // ou null selon ton usage
                    break;
            }
        } while (!coordonneesChoisies);
    }

    public void PlacerPlanteAvecFleches(Type typePlante)
    {
        int x = monde.ligne / 2;
        int y = monde.colonne / 2;

        ConsoleKey key;
        bool plantePlacee = false;

        do
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"   Jour {jour}");
            Console.ForegroundColor = ConsoleColor.White;

            // Animation pluie
            if (saison.meteo.estEnTrainDePleuvoir) Visuel.AfficherAnimationPluie(); // Déborde un peu sur la droite, dessous l'encadré
            else Visuel.AfficherAnimationSoleil();
            Console.WriteLine();

            // Affichage numéro de colonnes
            Console.Write($"\n   ");
            for (int i = 1; i < monde.colonne + 1; i++)
            {
                if (i < 10) Console.Write($" {i}");
                else Console.Write($"{i}");
            }
            Console.WriteLine();

            for (int i = 0; i < monde.ligne; i++)
            {
                // Affichage des numéros de lignes 
                if (i < 9) Console.Write($" {i + 1} ");
                else Console.Write($"{i + 1} ");

                for (int j = 0; j < monde.colonne; j++)
                {
                    if (i == x && j == y)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("X ");
                        Console.ResetColor();
                    }
                    else
                    {
                        if (monde.grillePlante?[i, j] != null)
                            Console.Write(monde.grillePlante[i, j].AfficherVisuel());
                        else if (monde.grilleAnimal?[i, j] != null)
                            Console.Write(monde.grilleAnimal[i, j].visuelAnimal);
                        else
                            Console.Write(monde.grilleTerrain[i, j].visuelTerrain);
                    }
                }
                monde.AfficherMeteo(i, saison.meteo);
            }

            Console.WriteLine();
            List<Terrain> terrainsModifiés = new List<Terrain>();
            for (int i = 0; i < monde.ligne; i++) // grilleTerrain comprend des classes Terrains
            {
                for (int j = 0; j < monde.colonne; j++)
                {
                    Terrain terrain = monde.grilleTerrain[i, j];

                    if (!terrainsModifiés.Contains(terrain))
                    {
                        Console.WriteLine(terrain.ToString());
                        terrainsModifiés.Add(terrain);
                    }
                }
            }

            Console.WriteLine("\nUtilisez les flèches pour déplacer le curseur, Enter pour planter, Échap pour annuler.\n");

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (x > 0) x--;
                    break;
                case ConsoleKey.DownArrow:
                    if (x < monde.ligne - 1) x++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (y > 0) y--;
                    break;
                case ConsoleKey.RightArrow:
                    if (y < monde.colonne - 1) y++;
                    break;
                case ConsoleKey.Enter:
                    if (monde.grillePlante?[x, y] == null)
                    {
                        Plante plante = (Plante)Activator.CreateInstance(typePlante, monde, x, y)!;
                        monde.AjouterPlante(plante, x, y, true);
                        if (plante.estMorte)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("La plante n’a pas survécu aux conditions actuelles.");
                            Console.ResetColor();
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            plantePlacee = true;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("🚫 Cette case est déjà occupée !");
                        Console.ResetColor();
                        Thread.Sleep(1500);
                    }
                    break;
                case ConsoleKey.Escape:
                    Console.WriteLine("Retour au menu...");
                    Thread.Sleep(1000);
                    return;
            }
        } while (!plantePlacee);
    }

    public void InitierMaladie(Saison saison, Plante plante)
    {
        Random rng = new Random(); int probaMaladie = -1;
        if (saison.libelle == "Printemps") probaMaladie = rng.Next(12);
        else if (saison.libelle == "Ete") probaMaladie = rng.Next(6);
        else if (saison.libelle == "Automne") probaMaladie = rng.Next(12);
        else probaMaladie = rng.Next(6);

        if (probaMaladie == 0)
        {
            plante.maladie = true;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"La plante ({plante.xPlante + 1},{plante.yPlante + 1}) est malade...");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public void AfficherRecolte()
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("\n🌾 RÉCAPITULATIF DES RÉCOLTES 🌾\n");

        Console.WriteLine("+---------------------------+-------------+");
        Console.WriteLine("| Plante                                  |");
        Console.WriteLine("+---------------------------+-------------+");

        for (int i = 0; i < monde.plantesPossible.Count; i++)
        {
            string nom = monde.plantesPossible[i];
            int quantite = monde.recolte[i];
            Console.WriteLine($"| {nom.PadRight(25)} | {quantite.ToString().PadLeft(11)} |");
        }
        Console.WriteLine("+---------------------------+-------------+");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void FinirPartie()
    {
        Console.Clear();
        Console.WriteLine("Vous êtes arrivé à la fin de la partie.");
        Console.WriteLine("Grille finale : ");
        //monde.AfficherGrille(); // Afficher seulement la grille
        AfficherRecolte();
        Console.WriteLine("\nAppuyer sur une Entree pour continuer");
        Console.ReadLine();
    }
}