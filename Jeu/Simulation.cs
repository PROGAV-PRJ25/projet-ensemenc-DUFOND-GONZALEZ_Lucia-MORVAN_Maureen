using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

public class Simulation
{
    public Monde monde { get; private set; }
    // Ajout des saisons
    public Saison saison { get; set; }
    private bool exit = false; // Variable qui permet de quitter le jeu pendant la partie
    public bool modeUrgence = false;
    public static int jour;
    private int[] coordonnees;
    public Simulation(Monde unMonde)
    {
        monde = unMonde;
        this.saison = new Saison(this.monde);
    }

    public void Simuler(Monde monde, int tour)
    {
        ChoisirModeDifficile();

        for (int i = 1; i <= tour; i++)
        {
            if (!exit)
            {
                jour = i;
                saison.DeterminerSaison();
                saison.AnnoncerSaison();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\nSemaine {i}\n");
                Console.ForegroundColor = ConsoleColor.White;

                // Météo du jour
                saison.meteo.Pleuvoir(); // La météo change selon la saison
                saison.meteo.DeterminerVariables();
                Console.WriteLine();


                if (i == 2) // TO DO : mettre condition mode urgence
                {
                    modeUrgence = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n ⚠️ MODE URGENCE ACTIVÉ ! Vous avez 3 actions pour protéger votre potager !\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    int actionsRestantes = 3;
                    while (actionsRestantes > 0)
                    {
                        Console.Clear();
                        monde.AfficherGrille(saison.meteo);
                        Console.WriteLine($"👉 Action(s) restante(s) : {actionsRestantes}\n");
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

    public static bool ChoisirModeDifficile()
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

    public static bool ChoisirModeDifficile()
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



        if (modeUrgence) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Actions d'urgence :");
            Console.WriteLine("10 - Creuser une tranchée");
            Console.WriteLine("11 - Installer un épouventail");
            Console.ForegroundColor = ConsoleColor.White;
        }

        ConsoleKey key;
        do
        {
            Console.Clear();
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
        /*Console.WriteLine("1 - Semer");
        Console.WriteLine("2 - Arroser");
        Console.WriteLine("3 - Mettre de l'engrais");
        Console.WriteLine("4 - Deherber");
        Console.WriteLine("5 - Traiter");
        Console.WriteLine("6 - Recolter");
        Console.WriteLine("7 - Faire fuir animal");

        Console.WriteLine("\n8 - Passer la journée");
        Console.WriteLine("9 - Quitter la partie");*/

        // Console.ForegroundColor = ConsoleColor.Blue;
        // Console.Write("Quelle action souhaitez-vous effectuer ? ");
        //Console.ForegroundColor = ConsoleColor.White;

        //bool entreeValide = false;


        int action = ProposerActionJoueurAvecFleche();
        // Demander ce que ça signifie
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
            // Cas pour semer
            case 1:
                ChoisirPlante();
                break;

            // Cas pour arroser
            case 2:
                ChoisirCoordonneesAvecFleches();
                monde.ArroserTerrain(coordonnees[0], coordonnees[1]);
                break;

            // Cas pour mettre de l'engrais
            case 3:
                ChoisirCoordonneesAvecFleches();
                monde.DeposerEngrais(coordonnees[0], coordonnees[1]);
                break;

            // Cas pour desherber
            case 4:
                ChoisirCoordonneesAvecFleches();
                monde.Desherber(coordonnees[0], coordonnees[1]);
                break;

            // Cas pour Traiter les plantes
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
                else {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Il n'y a aucune plante à récolter pour l'instant...");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                break;
            // Cas pour faire fuir animal
            case 7:
                ChoisirCoordonneesAvecFleches();
                monde.FaireFuirAnimal(coordonnees[0], coordonnees[1]);
                break;

            // Passer la journée (ne rien faire)
            case 8:
                break;

            //Cas pour finir la partie   
            case 9:
                FinirPartie();
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

        return choixTypePlante + 1; // Correspond à l’indice humain (1, 2, 3...)
    }


    public void ChoisirPlante()
    {
        Console.WriteLine();
        /*for (int j = 0; j < monde.plantesPossible.Count; j++)
        {
            Type type = Type.GetType(monde.plantesPossible[j])!;                                 // Récupérer le type dans la liste
            Plante planteTemp = (Plante)Activator.CreateInstance(type, monde, 0, 0)!;            // Créer plante temporaire
            Console.WriteLine($"{j + 1}. {monde.plantesPossible[j]} {planteTemp.ToString()}");   // Affichage des caractéristiques avec le ToString
        }

        bool entreeValide = false;
        int numPlante = -1;

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Quelle plante souhaitez-vous semer : ");
        Console.ForegroundColor = ConsoleColor.White;

        do
        {
            string texte = Console.ReadLine()!;
            try
            {
                numPlante = Convert.ToInt32(texte);
                if (numPlante > 0 && numPlante <= monde.plantesPossible.Count)
                    entreeValide = true;
            }
            catch { }
        }
        while (!entreeValide);*/

        int numPlante = ChoisirPlanteAvecFleches();

        Type typePlante = Type.GetType(monde.plantesPossible[numPlante - 1])!;
        PlacerPlanteAvecFleches(typePlante);
        // Probablement à supprimer (vérifier avec Maureen)
        /* do
         {
             peutSemer = true;

             // Pour le test de la fonction
             //int[] coordonnees = ChoisirCoordonnees();
             //Plante nouvellePlante = (Plante)Activator.CreateInstance(typePlante, monde, coordonnees[0], coordonnees[1])!;
             //monde.AjouterPlante(nouvellePlante, coordonnees[0], coordonnees[1]);
             PlacerPlanteAvecFleches(typePlante);
             if (nouvellePlante.estMorte)
             {
                 Console.WriteLine("Votre plante ne peut pas pousser dans ses conditions\nRéalisez une autre action.");
                 peutSemer = false;
             }
         } while (!peutSemer);*/
    }

    // Probablement à supprimer également puisqu'on utilise le curseur maintenant
    /*   public int[] ChoisirCoordonnees()
      {
          Console.ForegroundColor = ConsoleColor.Blue;
          Console.Write("\nNuméro de ligne : ");
          Console.ForegroundColor = ConsoleColor.White;

          bool entreeValide = false; int ligne = -1;
          do
          {
              string texte = Console.ReadLine()!;
              try
              {
                  ligne = Convert.ToInt32(texte);
                  if (ligne > 0 && ligne <= monde.ligne) entreeValide = true;
              }
              catch { }
          }
          while (!entreeValide);

          Console.ForegroundColor = ConsoleColor.Blue;
          Console.Write("Numéro de colonne : ");
          Console.ForegroundColor = ConsoleColor.White;

          entreeValide = false; int colonne = -1;
          do
          {
              string texte = Console.ReadLine()!;
              try
              {
                  colonne = Convert.ToInt32(texte);
                  if (colonne > 0 && colonne <= monde.colonne) entreeValide = true;
              }
              catch { }
          }
          while (!entreeValide);

          return [ligne - 1, colonne - 1];
      } */

    public void ChoisirCoordonneesAvecFleches()
    {

        int x = monde.ligne / 2;
        int y = monde.colonne / 2;

        ConsoleKey key;
        bool coordonneesChoisies = false;

        do
        {
            Console.Clear();
            Console.WriteLine($"Jour {jour}"); // Déterminer si jour ou semaine
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


            Console.WriteLine("\nUtilisez les flèches pour déplacer le curseur, Enter pour choisir les coordonnées, Échap pour annuler.\n");

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
                    coordonnees = [x, y];
                    coordonneesChoisies = true;
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
            Console.WriteLine($"Jour {jour}");

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
                        monde.AjouterPlante(plante, x, y);
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
