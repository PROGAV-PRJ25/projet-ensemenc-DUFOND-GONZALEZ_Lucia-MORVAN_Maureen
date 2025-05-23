public class Tests
{
    public Tests()
    {

    }
    // ***************************** TEST CREATION DES CLASSES & SIMULATION *****************************



    // ***************************** Test n°1 CREATION DES OBJETS ET AFFICHAGE SUR LE TERRAIN *****************************

    public static void RealiserPremierTest()
    {
        List<Terrain> terrainsMonde = new List<Terrain> { new TerrainSableux(), new TerrainTerreux(), new TerrainTranchee(), new TerrainEpouvantail() };
        List<string> plantesMonde = new List<string> { "Tulipe", "Rose", "Fraise", "Cerise" };
        List<string> animauxMonde = new List<string> { "Renard" };
        Monde monde = new Monde(13, 13, plantesMonde, terrainsMonde, animauxMonde);

        Tulipe t1 = new Tulipe(monde, 1, 2);
        monde.AjouterPlante(t1, t1.xPlante, t1.yPlante, false);
        t1.EtapeCroissance = 1;

        Tulipe t2 = new Tulipe(monde, 1, 3);
        monde.AjouterPlante(t2, t2.xPlante, t2.yPlante, false);
        t2.EtapeCroissance = 2;

        Tulipe t3 = new Tulipe(monde, 1, 4);
        monde.AjouterPlante(t3, t3.xPlante, t3.yPlante, false);
        t3.EtapeCroissance = 3;

        Tulipe t4 = new Tulipe(monde, 1, 5);
        monde.AjouterPlante(t4, t4.xPlante, t4.yPlante, false);
        t4.EtapeCroissance = 4;

        Rose r1 = new Rose(monde, 2, 2);
        monde.AjouterPlante(r1, r1.xPlante, r1.yPlante, false);
        r1.EtapeCroissance = 3;

        Fraise f1 = new Fraise(monde, 3, 2);
        monde.AjouterPlante(f1, f1.xPlante, f1.yPlante, false);
        f1.EtapeCroissance = 3;

        Fraise f2 = new Fraise(monde, 3, 3);
        monde.AjouterPlante(f2, f2.xPlante, f2.yPlante, false);
        f2.EtapeCroissance = 2;

        Fraise f3 = new Fraise(monde, 3, 4);
        monde.AjouterPlante(f3, f3.xPlante, f3.yPlante, false);
        f3.EtapeCroissance = 1;

        Cerise c1 = new Cerise(monde, 4, 2);
        monde.AjouterPlante(c1, c1.xPlante, c1.yPlante, false);
        c1.EtapeCroissance = 3;


        Saison saison = new Saison(monde);
        saison.meteo = new MeteoEte(monde);

        // Test de l'affichage des animaux

        Renard renard = new Renard(monde, 9, 9);
        monde.grilleAnimal[renard.coorX, renard.coorY] = renard;

        // test affichage des plantes malades
        f3.maladie = true; // La plante aux coordonnées (3,4) est malade
        t2.maladie = true; // La plante aux coordonnées (1,3) est malade

        monde.AfficherGrille(saison.meteo);
        // On observe si les éléments sont bien apparus comme on le souhaitait


        // ***************************** TEST POUR TRAITER LES PLANTES ET FAIRE FUIR L'ANIMAL

        // Une fois qu'on a bien vérifié que tout s'affichait correctement comme on le souhaitait, 
        // On passe à la suite du test pour vérifier si la fonction pour Traiter les fleurs et celle pour faire fuir l'animal fonctionne 
        Console.WriteLine("\nAppuyez sur une touche");
        Console.ReadKey(true);

        monde.TraiterPlante(f3.xPlante, f3.yPlante); // La plante est censée être traitée

        monde.FaireFuirAnimal(renard.coorX, renard.coorY); // Au moment de l'affichage, le renard est censé disparaître.

        monde.Desherber(c1.xPlante, c1.yPlante); // La cerise aux coordonnées (4,2) est censée disparaître

        monde.AfficherGrille(saison.meteo);

        Console.WriteLine($"f3 est malade: {f3.maladie}"); // Censé être false
        Console.WriteLine($"t2 est malade: {t2.maladie}"); // Censé être encore vrai

        // On vérifie qu'il n'y a vraiment plus d'animaux
        int nbrAnimaux = 0;
        foreach (Animal animal in monde.listeAnimal)
        {
            nbrAnimaux++;
        }
        Console.WriteLine($"Il y a {nbrAnimaux} animaux"); // Censé être zéro
    }


    // ************************** TEST n°2 : Propagation des plantes et déplacement des animaux **************************

    public static void RealiserDeuxiemeTest()
    {
        List<Terrain> terrainsMonde = new List<Terrain> { new TerrainSableux(), new TerrainTerreux(), new TerrainTranchee(), new TerrainEpouvantail() };
        List<string> plantesMonde = new List<string> { "Tulipe", "Rose", "Fraise", "Cerise" };
        List<string> animauxMonde = new List<string> { "Renard" };
        Monde monde = new Monde(13, 13, plantesMonde, terrainsMonde, animauxMonde);

        Saison saison = new Saison(monde);
        // Ici on vérifie que les plantes se propagent et croissent bien
        // on vérifie également que les animaux se déplacent

        // Faise se propage lorsqu'elle a assez grandi (au bout de 3 jours d'existence)
        Fraise f1 = new Fraise(monde, 3, 2);
        monde.AjouterPlante(f1, f1.xPlante, f1.yPlante, false);

        monde.AjouterAnimal(saison, monde); // Un animal est censé apparaître puis se déplacer

        Renard renard = new Renard(monde, 7, 7);
        monde.grilleAnimal[renard.coorX, renard.coorY] = renard; // On reprend le code qui se trouve dans AjouterAnimal dans la classe Monde
        monde.listeAnimal.Add(renard); ;
        // On force l'animal à apparaître pour vérifier qu'il se déplace bien 

        Simulation simulation2 = new Simulation(monde);

        simulation2.Simuler(monde, 3);
        // IMPORTANT: Il faut passer la journée dans les actions afin de voir ce qui se fait bien tout seul

        // On remarquera que l'animal est bien apparu et qu'il se déplace bien!
    }




    // ***************************************** TEST POUR AUTRE MONDE POSSIBLE *****************************************

    // Maintenant que nous avons réalisé les premiers tests pour les déplacement et la propagation, on peut tester le monde de la forêt enchantée (sans avoir besoin de faire autant de tests)

    public static void TestForetEnchantee()
    {
        List<Terrain> terrainsMonde = new List<Terrain> { new TerrainBoise(), new TerrainHumide(), new TerrainTranchee(), new TerrainEpouvantail() };
        List<string> plantesMonde = new List<string> { "Noisetier", "Sapin", "Rhododendron", "Trefle" };
        List<string> animauxMonde = new List<string> { "Ecureuil" };
        Monde monde = new Monde(10, 10, plantesMonde, terrainsMonde, animauxMonde);

        Sapin s1 = new Sapin(monde, 3, 8);
        monde.AjouterPlante(s1, s1.xPlante, s1.yPlante, false);
        s1.EtapeCroissance = 3;

        Noisetier n1 = new Noisetier(monde, 1, 5);
        monde.AjouterPlante(n1, n1.xPlante, n1.yPlante, false);
        n1.EtapeCroissance = 3;

        Trefle t1 = new Trefle(monde, 4, 2);
        monde.AjouterPlante(t1, t1.xPlante, t1.yPlante, false);
        t1.EtapeCroissance = 3;

        Rhododendron r1 = new Rhododendron(monde, 8, 5);
        monde.AjouterPlante(r1, r1.xPlante, r1.yPlante, false);
        r1.EtapeCroissance = 3;
        Rhododendron r2 = new Rhododendron(monde, 7, 5);
        monde.AjouterPlante(r2, r2.xPlante, r2.yPlante, false);
        r2.EtapeCroissance = 2;
        Rhododendron r3 = new Rhododendron(monde, 8, 4);
        monde.AjouterPlante(r3, r3.xPlante, r3.yPlante, false);
        r3.EtapeCroissance = 1;

        Saison saison = new Saison(monde);
        Meteo meteo = new MeteoHiver(monde);

        Ecureuil ecureuil = new Ecureuil(monde, 0, 9);
        monde.grilleAnimal[ecureuil.coorX, ecureuil.coorY] = ecureuil;

        Ecureuil ecureuil2 = new Ecureuil(monde, 0, 0);
        monde.grilleAnimal[ecureuil2.coorX, ecureuil2.coorY] = ecureuil2;

        monde.grilleTerrain[0, 8] = terrainsMonde[2];   // Creuser une tranchée
        monde.grilleTerrain[1, 9] = terrainsMonde[2];
        ecureuil.SeDeplacerAlea(); // L'animal se déplace sur la (1,8) car c'est la seule à laquelle il a accès

        monde.grilleTerrain[7, 7] = terrainsMonde[3];   // Ajout d'épouventail
        monde.grilleTerrain[2, 2] = terrainsMonde[3];

        monde.AfficherGrille(meteo);

    }


    // *************************************** TEST POUR LA METEO ***************************************

    public static void RealiserTestsMeteo()
    {
        // On cherche ici à voir si les saisons s'enchainent bien et si la meteo suit bien
        List<Terrain> terrainsMonde = new List<Terrain> { new TerrainSableux(), new TerrainTerreux(), new TerrainTranchee(), new TerrainEpouvantail() };
        List<string> plantesMonde = new List<string> { "Tulipe", "Rose", "Fraise", "Cerise" };
        List<string> animauxMonde = new List<string> { "Renard" };
        Monde monde = new Monde(13, 13, plantesMonde, terrainsMonde, animauxMonde);

        Saison saison = new Saison(monde);

        // On reprend une partie du code de la méthode Simuler qui se trouve dans la classe Simulation pour tester les saisons
        for (int i = 1; i <= 20; i++)

        {
            Console.WriteLine($"Jour {i}");
            // La saison est déterminé selon le cours de la partie comme Printemps ou Automne etc
            saison.DeterminerSaison(); // L'objet météo est créé dans la classe Saison par la biais de cette fonction
            saison.AnnoncerSaison(); // Afficher un visuel s'il y a un changement de saison pour informer le joueur
            Thread.Sleep(1500);

            // Météo du jour
            saison.meteo.DeterminerCatastropheEtVariables();
            saison.meteo.Pleuvoir();
            for (int j = 0; j <= 10; j++) // Dans la simulation, ça correspond ici à l'affichage de la grille
            {
                monde.AfficherMeteo(j, saison.meteo);
            }

            saison.meteo.AfficherHumiditeTerrain(); // Comme cela on peut également vérifier l'augmentation des caractéristiques des terrains (en particulier s'il pleur ou pas)

            Console.WriteLine("\nAppuyez sur une touche");
            Console.ReadKey(true);

            saison.temps++;
        }
    }


    // *************************************** TEST POUR VERIFIER L'ARRET DE LA CROISSANCE DES PLANTES***************************************

    public static void VerifierArretCroissance()
    {
        List<Terrain> terrainsMonde = new List<Terrain> { new TerrainSableux(), new TerrainTerreux(), new TerrainTranchee(), new TerrainEpouvantail() };
        List<string> plantesMonde = new List<string> { "Tulipe", "Rose", "Fraise", "Cerise" };
        List<string> animauxMonde = new List<string> { "Renard" };
        Monde monde = new Monde(13, 13, plantesMonde, terrainsMonde, animauxMonde);
        Saison saison = new Saison(monde);

        Tulipe t1 = new Tulipe(monde, 1, 2);
        monde.AjouterPlante(t1, t1.xPlante, t1.yPlante, false);
        t1.EtapeCroissance = 1;

        Tulipe t2 = new Tulipe(monde, 1, 3);
        monde.AjouterPlante(t2, t2.xPlante, t2.yPlante, false);
        t2.EtapeCroissance = 2;

        Tulipe t3 = new Tulipe(monde, 1, 4);
        monde.AjouterPlante(t3, t3.xPlante, t3.yPlante, false);
        t3.EtapeCroissance = 3;

        Tulipe t4 = new Tulipe(monde, 1, 5);
        monde.AjouterPlante(t4, t4.xPlante, t4.yPlante, false);
        t4.EtapeCroissance = 4;

        Rose r1 = new Rose(monde, 2, 2);
        monde.AjouterPlante(r1, r1.xPlante, r1.yPlante, false);
        r1.EtapeCroissance = 3;

        Fraise f1 = new Fraise(monde, 3, 2);
        monde.AjouterPlante(f1, f1.xPlante, f1.yPlante, false);
        f1.EtapeCroissance = 3;

        Fraise f2 = new Fraise(monde, 3, 3);
        monde.AjouterPlante(f2, f2.xPlante, f2.yPlante, false);
        f2.EtapeCroissance = 2;

        Fraise f3 = new Fraise(monde, 3, 4);
        monde.AjouterPlante(f3, f3.xPlante, f3.yPlante, false);
        f3.EtapeCroissance = 1;

        Cerise c1 = new Cerise(monde, 4, 2);
        monde.AjouterPlante(c1, c1.xPlante, c1.yPlante, false);
        c1.EtapeCroissance = 3;

        // On reprend des éléments de Simulation

        for (int i = 1; i <= 7; i++)
        {
            Console.WriteLine("*****************************************************");
            Console.WriteLine($"Jour {i}");
            // Météo du jour
            saison.meteo.DeterminerCatastropheEtVariables();
            saison.meteo.Pleuvoir();
            Console.WriteLine();

            // On vérifie que les plantes ne poussent pas s'il y a trop d'eau

            foreach (Terrain terrain in terrainsMonde)
            {
                terrain.humidite = 100; // Trop d'eau pour toutes les plantes
                terrain.fertilite = 49;  // Pas assez fertile             
            }
            saison.meteo.AfficherHumiditeTerrain();

            saison.meteo.temperature = -15;

            foreach (Plante plante in monde.listePlante)
            {
                plante.maladie = true;
            }

            monde.AfficherGrille(saison.meteo);
            foreach (var plante in monde.listePlante)
            {
                plante.Croitre(monde, saison.meteo); // Si la méthode est bonne, les plantes ne grandiront pas.
            }

            for (int x = monde.listePlante.Count - 1; x >= 0; x--)
            {
                if (monde.listePlante[x].estMorte)
                {
                    monde.grillePlante![monde.listePlante[x].xPlante, monde.listePlante[x].yPlante] = null!;
                    monde.listePlante.RemoveAt(x);
                }
            }

            int cptPlanteEnvahissante = 0;
            foreach (var plante in monde.listePlante.ToList())
            {
                if (!plante.estMorte && plante is PlanteEnvahissante envahissante)
                {
                    envahissante.SePropager(); // Si la méthode est bonne, les plantes ne se propageront pas
                    cptPlanteEnvahissante++;
                }
            }

            saison.temps++; // Un jour s'est écoulé
            Thread.Sleep(2500);

        }

    }

}