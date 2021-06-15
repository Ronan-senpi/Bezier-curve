# Bezier-curve
Bézier curve

# Step1

![drag image](https://raw.githubusercontent.com/Ronan-senpi/Bezier-curve/main/.gitressources/GIF/bezier.gif)

:o: done :pushpin: in progress :x: cancel

1) Cliquer n+1 points de contrôle à l'écran et afficher le polygone de controle :o:

2) Utiliser l'algorithme de DeCasteljau, celui du cours en version itérative, afin d'engendrer la courbe dedegré n, tracer cette courbe à l'écran avec un pas fixé :o:

3) Possibilité de modifier le pas, avec les touches + et - afin de pouvoir (ou non) lisser la courbe :o:

4) Pouvoir engendrer un nombre illimité de courbes, pour cela utiliser les listes chaînées :o:

5) Pouvoir parcourir la liste et supprimer n'importe quelle courbe :o:

6) Pouvoir déplacer et supprimer un ou plusieurs points de controle et afficher en temps réel la courbe :o:

7) Modifier les courbes: utiliser les matrices de translation, scaling, rotation et cisaillement sur les points de controle d'une Bézier et tracer en temps réel la courbe en fonction des points de controle modifiés. Effectuer une gestion à la souris, éventuellement clavier. :o:

8) Effet de la multiplicité d'un point de controle: répéter un point de controle dans la liste des points de controle et vérifier que la courbe se rapproche de ce point :o:

9) Implémenter une méthode pour calculer l'enveloppe convexe d'un nuage de points (ex: méthode de Jarvis). Calculer les enveloppes convexes des lignes polygonales de controle de 2 Bézier. Tester si les deux enveloppes s'interceptent. Si c'est le cas calculer le point d'intersection éventuel entre les 2 Bézier :o:


10) Effectuer des découpages et remplissages de courbes de Bézier fermées via les algo vus au S1 :x:

# Step 2

1) de courbes de Bézier avec l'utilisateur (reprise du projet précédent).

2) de primitives d'extrusion graphiques interactives obtenues à l'aide d'un profil 2D (Bézier et polygones) et d'une trajectoire 3D (Bézier ou polygone)

3) du tableau des normales en chaque point de la surface

4) de la navigation d'une caméra sous OpenGL.

Bonus: 1) texturage des surfaces obtenues à l'aide des shaders

 2) cas général pour les primitives d'extrusion généralisées

 L'interface utilisateur comportera un menu permettant les sélections:

 a) Tracé de polygones et de courbes Bézier

 b) Primitives d'extrusion

 c) Mode pour les surfaces: filaire, plein (avec et sans éclairage), texturé (avec et sans éclairage)

Détail des différentes parties à implémenter:

Pour les extrusions, Il est judicieux d'avoir 2 affichages distincts:

 - 2D pour la construction de courbes (Bézier et polygones quelconques)

 - 3D pour afficher la surface engendrée

1) Effectuer l'extrusion simple d'une courbe 2D (ouverte ou fermée) en utilisant une hauteur et un
coefficient d'agrandissement ou de réduction de la base supérieure.

2) Effectuer l'extrusion par révolution d'une courbe 2D (ouverte ou fermée).

Rq: Facettiser toutes les surfaces (maillage triangulaire ou rectangulaire) à l'aide des indices des
paramètres dans l'espace (u,v) du cours

4) Calculer le tableau des normales en chaque point de la surface et effectuer le remplissage des facettes
ainsi que son éclairage (lumière ambiante, diffuse et spéculaire)

5) Effectuer une navigation de caméra pour pouvoir se déplacer dans l’environnement 3D.

6) Bonus

a) Bonus: Effectuer l'extrusion généralisée d'une courbe 2D (ouverte ou fermée) le long d'une courbe 3D
(ouverte ou fermée) contenue dans le plan z=0.

b) Programmer des shaders afin de texturer les surfaces (extrusion et Bézier). Proposer une liste de
textures que l'utilisateur pourra appliquer.

c) Amélioration de la gestion d'OpenGL: lumière réfractée et transparence