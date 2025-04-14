using System.Runtime.CompilerServices;

public class Monde
{
    public int X { get; protected set; }
    public int Y { get; protected set; }
    private static int NbrPapier = 0;
    public static int NbrPapierSuivant;
    //private int nombreMaxPG = 9;
    public int LigneDepot { get; protected set; }
    public int ColonneDepot { get; protected set; }
    public int LigneRetrait { get; protected set; }
    public int ColonneRetrait { get; protected set; }
    public int[,] Grille;


    public Monde(int x = 10, int y = 10)
    {
        X = x;
        Y = y;

        Grille = new int[X, Y];

        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                Grille[i, j] = 0;
            }
        }
    }


    public void AjouterPapier(int LigneDepot, int ColonneDepot)
    {
        this.LigneDepot = LigneDepot;
        this.ColonneDepot = ColonneDepot;
        NbrPapier = Grille[LigneDepot, ColonneDepot];
        NbrPapierSuivant = NbrPapier++;

        if (NbrPapierSuivant + 1 <= 9)
        {
            Grille[LigneDepot, ColonneDepot] = NbrPapier++;

        }
    }

    public void EnleverPapier(int LigneDepot, int ColonneDepot)
    {
        this.LigneDepot = LigneDepot;
        this.ColonneDepot = ColonneDepot;
        NbrPapier = Grille[LigneDepot, ColonneDepot];
        NbrPapierSuivant = NbrPapier--;

        if (NbrPapierSuivant > 0)
        {
            Grille[LigneDepot, ColonneDepot] = NbrPapier--;

        }
    }

    public void AfficherGrille()
    {
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                if (Grille[i, j] == 0)
                {
                    Console.Write(".  ");
                }
                else
                    Console.Write($"{Grille[i, j]}  ");

            }
            Console.WriteLine();
        }
    }



}