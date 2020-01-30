Lanceurs de projectiles :

	Num Projectiles : Nombre de projectiles envoyés à chaque "pulse" du système.

	Projectile : L'objet envoyé en tant que projectile. 

	Projectile Speed : Vitesse du projectile.

	Step Angle (Used in Default, Angle Range and Moving Angle Range behaviors) : Angle (sur le cercle) entre les projectile

	Timed Spawn : Y as-t-il un interval entre les "pulse" du système ?

	Spawn Interval : Temps entre deux "pulse" si timed spawn est true.

	Circle Radius : Distance du projectile par rapport au centre du cercle à l'instantiation.

	Launcher Parameters : 
		Default behavior : Distribution en fonction de step angle & Nb de projectiles. Ne feras pas necessairement 360°.

		Equal Distribution behavior : Distribution à angle égal entre chaque projectile en fonction uniquement du nombre de projectiles.

		Angle Range : Distribue des projectiles de manière égales sur un angle donné (de 0 à l'angle sur le cercle trigo)

		Moving Angle Range : Distribue des projectile de manière égale sur un angle donné, tout en parcourant le cercle.

		Progressive Circle : Instancie des projectiles un par un suivant une distribution égale le long du cercle (en fonction de Num Projectiles)

Projectiles : 

	Default projectiles : Just die once it's been alive for it's lifetime.

	Helix projectiles : Like projectiles but when several are instanciated (and not synchronized) they do a beautiful helix. They can also just do an oscillating pattern if synchronized.

Modifiers :

	Gravity well : Puit de gravité, utilisant la formule de l'attraction gravitationnelle de Newton pour déterminer la norme du vecteur à obtenir, normalisation du vecteur entre les deux points, et mutiplication par le résultat. (qui est G * (Ma * Mb / Dab²))