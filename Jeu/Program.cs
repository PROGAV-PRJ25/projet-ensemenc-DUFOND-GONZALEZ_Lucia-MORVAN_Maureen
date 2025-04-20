Monde monde = new Monde(5,5);

Plante plante1 = new Plante(monde,2,2);
monde.AjouterPlante(plante1,plante1.xPlante, plante1.yPlante);

monde.AfficherGrille();
Console.WriteLine("Faire pousser la plante au centre");
plante1.Croitre();
plante1.Croitre();
//plante1.Croitre();
//plante1.Croitre();

monde.AfficherGrille();

Console.WriteLine(plante1.AfficherVisuel());