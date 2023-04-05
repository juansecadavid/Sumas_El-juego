# Sumas_El-juego

Cambios y consideraciones con respecto a la entrega anterior

-Se añadió un enumerator a la clase character, con el fin de poder tipificar cuando un personaje es el personaje principal, un enemigo, o un cofre.

-Se añadieron 2 métodos a la clase Floor, los cuales se encargan de añadir elementos a una lista de personajes, y de removerlos respectivamente.

-Se añadieron 2 métodos a la clase Tower, los cuales se encargan de añadir elementos a una lista pisos, y de removerlos respectivamente.

-El nombre de la clase SceneManager se modificó a “GameManager” para evitar conflictos con unity.

-Se añadieron 4 nuevos métodos al GameManager, donde 3 de estos se refierene a implementaciones que antes eran para los enemigos, y ahora se gragaron versiones para el jugador. Pero cumplen el mismo rol dentro del funcionamiento del programa. El cuarto método se refiere a la interacción del personaje al moverse entre torres y pisos, el cual viene siendo el método más largo e importante de toda la implementación.

-El oreden de los métodos del GameManager quedó de la siguiente manera:

--TowerCharacterGenerator

--TowerGenerator

--FloorGenerator

--FloorGeneratorForMain

--CharacterGenerator

--EnemyGenerator

--MoveAndFight
