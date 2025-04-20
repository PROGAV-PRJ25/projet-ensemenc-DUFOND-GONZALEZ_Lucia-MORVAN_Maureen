public class Monde
{
    public Plante[,]? grille; // J'ai changé par public parce que je ne parviens pas à le mettre dans la classe rhododendron sinon...
    // (On va vraiment finir par tout mettre en public c'est énervant)
    public int ligne;
    public int colonne;
    public List<Plante> listePlante = new List<Plante>();

    public Monde(int ligne, int colonne)
    {
        this.ligne = ligne;
        this.colonne = colonne;
        grille = new Plante[ligne, colonne];
        // Que des null en valeurs
    }

    // Initialisation par défaut à 10 et 10 
    public Monde() : this(10, 10) { }

    public void AfficherGrille()
    {
        for (int i = 0; i < ligne; i++)
        {
            for (int j = 0; j < colonne; j++)
            {
                if (grille?[i, j] != null)
                    Console.Write(grille[i, j].AfficherVisuel());
                else
                    Console.Write("⬜");
            }
            Console.WriteLine();
        }
    }

    public void AjouterPlante(Plante plante, int x, int y)
    {
        if (x >= 0 && x < ligne && y >= 0 && y < colonne)
        {
            if (grille?[x, y] == null) // J'ai modifié par égal car on suppose que si ça vaut null alors une plante peut y pousser
            {
                grille[x, y] = plante;
                listePlante.Add(plante);
            }

        }
    }

}