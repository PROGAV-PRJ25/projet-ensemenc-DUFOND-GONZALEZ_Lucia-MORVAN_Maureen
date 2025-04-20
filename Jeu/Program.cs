Monde monde = new Monde(5, 5);

Plante plante1 = new Plante(monde, 2, 2);
monde.AjouterPlante(plante1, plante1.xPlante, plante1.yPlante);
/* 
monde.AfficherGrille();
Console.WriteLine("Faire pousser la plante au centre");
plante1.Croitre();
plante1.Croitre();
//plante1.Croitre();
//plante1.Croitre(); 

monde.AfficherGrille(); */

Rhododendron plante2 = new Rhododendron(monde, 1, 1);
monde.AjouterPlante(plante2, plante2.xPlante, plante2.yPlante);
/* plante2.SePropager(); // Pas censée se propager à ce moment
monde.AfficherGrille();
plante2.Croitre();
plante2.SePropager(); // Peut se propager à ce moment là

monde.AfficherGrille(); */

Simulation simulation2 = new Simulation(monde);
simulation2.Simuler(monde);