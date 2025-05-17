public abstract class Animal
{
    public Monde monde;
    public string type;
    public int coorX;
    public int coorY;
    public int plantePrefere;
    public string visuelAnimal;

    public Animal(Monde leMonde, string leType, int X, int Y, int laPlantePref, string leVisuel){
        monde = leMonde;
        type = leType;
        coorX = X;
        coorY = Y;
        plantePrefere = laPlantePref;
        visuelAnimal = leVisuel;
    }

    public void SeDeplacerAlea()
    {
        Random rng = new Random();
        List<(int dx, int dy)> directions = new List<(int, int)> { 
            (1,0),(-1,0),(0,1),(0,-1),(1,1),(-1,-1),(1,-1),(-1,1) // Ensemble des déplacements possibles
        };
        directions = directions.OrderBy(_ => rng.Next()).ToList(); // Mélanger les directions 

        foreach (var (dx, dy) in directions) // Parcourir toutes les directions => obliger le déplacement s'il est possible
        {
            int nouvelleLigne = coorX + dx;
            int nouvelleColonne = coorY + dy;
            // Verifier que la case est dans la grille et qu'il n'y a pas déjà un animal 
            if (nouvelleLigne >= 0 && nouvelleLigne < monde.ligne && nouvelleColonne >= 0 && nouvelleColonne < monde.colonne
            && monde.grilleAnimal[nouvelleLigne, nouvelleColonne] == null) 
            {
                monde.grilleAnimal[coorX, coorY] = null!;
                coorX = nouvelleLigne;
                coorY = nouvelleColonne;
                monde.grilleAnimal[coorX, coorY] = this; 
                if(monde.grillePlante?[coorX, coorY] != null)
                {
                    Plante plante = monde.grillePlante[coorX, coorY];
                    MangerPlante(plante, coorX, coorY);
                }
                break;
            }
        }
    }

    public void MangerPlante(Plante plante, int x, int y)
    {
        if(plantePrefere == plante.idType){
            if(plante.nbFruit > 0){
                plante.nbFruit--;
                Console.WriteLine($"Attention l'animal mange les fruits de la plante ({x},{y}) !");
            }
            else{
                plante.EtapeCroissance--;
                Console.WriteLine($"Vous avez de la chance, la plante ({x},{y}) n'a plus de fruits.");
                Console.WriteLine($"Attention, sa croissance a tout de même était ralentit.");
            }
        }
        else{
            monde.grillePlante![x, y] = null!;      // On supprime la plante de la grille
            monde.listePlante?.Remove(plante);    // On supprime la plante de la liste
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"La plante du terrain ({x},{y}) a été détruite par un animal !");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}