# Sumas_El-juego

### Cambios y consideraciones con respecto a la entrega 1:

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


### Cambios con respecto a la entrega 2:

Se implementa ahora sí en Unity, se hacen fondos, menús, UI, sprites de jugador y enemigos.

El código se ve obligado a cambiar, pues todas las formas de instanciar objetos ahora son diferentes para que sí se creen los objetos en el editor como tal. Se implementa el sistema de combate, los niveles, la generación de las torres y los pisos ahora con interacciones entre objetos en Unity. 

Se implementa gravedad en los objetos para un funcionamiento correcto del sistema de pisos y personajes.
Se implementa el drag & drop para el personaje principal.
El combate es totalmente funcional, gana el que tenga mayor poder, si es un enemigo, este se destruye.
Aún falta implementar el funcionamiento y la generación de objetos de ayuda, además de que aparezcan los labels del poder sobre cada personaje, se está trabajando en esto.



Link del ejecutable: https://drive.google.com/drive/u/0/folders/10SAryWePZcjureg2LmfnnpgKFdRPqyhj
