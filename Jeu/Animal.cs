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
        int nouvelleLigne = coorX + rng.Next(-1,2);
        int nouvelleColonne = coorY + rng.Next(-1,2);
        if (nouvelleLigne >= 0 && nouvelleLigne < monde.ligne && nouvelleColonne >= 0 && nouvelleColonne < monde.ligne)
        {
            coorX = nouvelleLigne;
            coorY = nouvelleColonne;
            if(monde.grillePlante?[coorX, coorY] != null)
            {
                Plante plante = monde.grillePlante[coorX, coorY];
                MangerPlante(plante, coorX, coorY);
            }
        }
    }

    public void MangerPlante(Plante plante, int x, int y)
    {
        //if(plantePrefere = )
    }
}