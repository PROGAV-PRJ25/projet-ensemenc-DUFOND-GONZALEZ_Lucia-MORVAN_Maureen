public class Monde
{
    protected Plante[,]? grille;
    public int ligne;
    public int colonne;

    public Monde(int ligne, int colonne)
    {
        this.ligne = ligne;
        this.colonne = colonne;
        grille = new Plante[ligne,colonne];
    }

    public Monde() : this(10,10){}

    public void AfficherGrille()
    {
        for(int i = 0; i<ligne; i++)
        {
            for(int j = 0; j<colonne; j++)
            {
                if (grille[i, j] != null)
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
            grille[x, y] = plante;
        }
    }
}