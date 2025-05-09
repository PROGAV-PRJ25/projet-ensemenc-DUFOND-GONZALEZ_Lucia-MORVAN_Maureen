public class Saison
{
    public Meteo meteo { get; set; }
    public string libelle { get; set; }
    public static int temps { get; set; }

    public Saison()
    {
        temps = 0; //Initialisation à 0
    }

    public void AvancerSaison()
    {
        // TO DO: modifier aussi avec la température et le vent + luminosité en fonction des nuages
        if (temps < 4)
        {
            libelle = "Printemps";
            meteo.probaPleuvoir = 70; // Il pleut souvent au printemps
        }

        if (temps > 4 && temps < 8)
        {
            libelle = "Ete";
            meteo.probaPleuvoir = 30;
        }

        if (temps > 8 && temps < 12)
        {
            libelle = "Automne";
            meteo.probaPleuvoir = 60;
        }

        if (temps > 12 && temps < 14)
        {
            libelle = "Hiver";
            meteo.probaPleuvoir = 50;
        }

    }

    public string AnnoncerSaison()
    {
        return $"C'est le {libelle.ToUpper()}!!"; // Améliorer pour que ce soit plus joli esthétiquement 

    }




}