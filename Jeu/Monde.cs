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
                if(grille[i,j].etapeCroissance == 1){
                    Console.Write(".");
                }
                else if(grille[i,j].etapeCroissance == 2){
                    Console.Write("l");
                }
                else if(grille[i,j].etapeCroissance == 3){
                    Console.Write("T");
                }
                else{
                    Console.WriteLine("O");
                }
            }
            Console.WriteLine();
        }
    }

    // public void Semer()
    // {

    // }
}