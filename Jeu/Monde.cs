public class Monde
{
    protected Plante[,]? grille;
    protected int Lignes { get; }
    protected int Colonnes { get;}

    public Monde(int lignes, int colonnes)
    {
        Lignes = lignes;
        Colonnes = colonnes;
        grille = new Plante[Lignes,Colonnes];
    }

    public Monde() : this(10,10){}

    public void AfficherGrille()
    {
        for(int i = 0; i<Lignes; i++)
        {
            for(int j = 0; j<Colonnes; j++)
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