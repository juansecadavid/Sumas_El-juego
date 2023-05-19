using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using System;
using static MoveSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NewGameManager : MonoBehaviour
{
    public static int enemyCounter = 0;

    public GameObject nextLevelScreen;
    public Button nextLevelButton;

    public GameObject WonScreen;
    public Button WonButton;

    [SerializeField]
    GameObject towerTry;

    [SerializeField]
    GameObject floorTry;

    [SerializeField]
    GameObject enemyTry;

    [SerializeField]
    GameObject playerTry;
    float countInicial=-13;
    float count;
    float countCharacterX = -2;
    Tower EnemyTower;
    public Sprite sprite1;
    public Sprite sprite2;
    Sprite enemySprite;
    Floor[] floorlist;
    Tower[] towerList;
    int currentLevel;

    private static NewGameManager instance;

    public static NewGameManager Instance { get => instance; private set => instance = value; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void Start()
    {
        EnemyTower = towerTry.GetComponent<Tower>();
        //EnemyFloor=floorTry.GetComponent<Floor>();
        currentLevel = 1;
        CreateLevel(currentLevel);
        //Instantiate(playerTry, new Vector3(-17, -13f, 0), Quaternion.identity);

    }
    public void Update()
    {
        
    }
    public void TowerCharacterGenerator()
    {
        List<Floor> floorList = FloorGeneratorForMain(2, Character.type.main);
        //Tower tower = new Tower(floorList, Character.type.main);

        //Tower tower = towerTry.AddComponent<Tower>();

        Instantiate(towerTry, new Vector3(-20, -1.5f, 0), Quaternion.identity);

        //return tower;
    }
    public void TowerGenerator(int numberFloor, int numberCharacter, float xPos)
    {
        int numberFloors = numberFloor;
        int numberCharcters = numberCharacter;
        count = countInicial;
        List<Floor> floorList;
        floorList = FloorGenerator(numberFloors, numberCharcters, Character.type.evil, xPos);

        //Tower tower = new Tower(floorList, Character.type.evil);
        EnemyTower.FloorList = floorList;

        Instantiate(towerTry, new Vector3(xPos, -1.5f, 0), Quaternion.identity);
        //return tower;
    }
    public List<Floor> FloorGenerator(int numberFloors, int numberCharacters, Character.type type, float xPos)
    {
        List<Floor> floorList = new List<Floor>();

        

        for (int i = 0; i < numberCharacters; i++)
        {
            List<Character> list = new List<Character>();
            countCharacterX = 0f;

            int enemyRand = Random.Range(1, numberCharacters+1);

            for (int j = 0; j < enemyRand; j++)
            {
                Character character = EnemyGenerator(type, countCharacterX, xPos-4);
                list.Add(character);
                //character.GetComponent<BoxCollider2D>().enabled = false;
                //character.GetComponent<Rigidbody2D>().gravityScale = 0;

            }
            enemyCounter++;
            //Floor floor = new Floor(list);
            //floorList.Add(floor);

            Instantiate(floorTry, new Vector3(xPos, count, 0), Quaternion.identity);
            count += 10f;
        }
        return floorList;
    }

    public List<Floor> FloorGeneratorForMain(int numberFloors, Character.type type)
    {
        List<Floor> floorList = new List<Floor>();
        int rand = UnityEngine.Random.Range(0, 2);
        float count = -13;

        for (int i = 0; i < numberFloors; i++)
        {
            List<Character> list = new List<Character>();

            if (rand == i)
            {
                Character character = CharacterGenerator(type);
                list.Add(character);
            }

            //Floor floor = new Floor(list);           

            //Floor floor = floorTry.AddComponent<Floor>();

            Instantiate(floorTry, new Vector3(-20, count, 0), Quaternion.identity);

            //aqu� se deber�a crear un piso

            //floorList.Add(floor);

            count += 10f;
        }
        return floorList;
    }
    public Character CharacterGenerator(Character.type type)
    {
        Character character = new Character(7, type);
        return character;
    }
    public Character EnemyGenerator(Character.type type, float countCharacterX, float xPos)
    {

        Character character = new Character((4 + enemyCounter * 2), type);

        //Retrieve();
        int rand = Random.Range(10, 16);
        enemyTry.GetComponent<Character>().level = rand;
        int randSprite=Random.Range(0, 2);
        if(randSprite==0)
        {
            enemySprite = sprite1;
        }
        else
        {
            enemySprite=sprite2;
        }
        enemyTry.GetComponent<SpriteRenderer>().sprite = enemySprite;
        Instantiate(enemyTry, new Vector3(countCharacterX+xPos, count, 0), Quaternion.identity);
        this.countCharacterX += 4f;
        //Character character = new Character((4), type);

        return character;
    }

    public static void MoveAndFight(Character player, Floor actualFloor, Floor floorToMove, Tower originTower)
    {
        if (floorToMove.CharactersList.Count > 0)
        {
            //Console.WriteLine("El combate inicio");
            actualFloor.RemoveCharacter(player);
            floorToMove.AddCharacter(player);

            if (actualFloor.CharactersList.Count == 0 && originTower.Type != Character.type.main)
            {
                originTower.RemoveFloor(actualFloor);
            }


            int result = floorToMove.CharactersList[floorToMove.CharactersList.Count - 1].Level - floorToMove.CharactersList[floorToMove.CharactersList.Count - 2].Level;
            if (floorToMove.CharactersList[floorToMove.CharactersList.Count - 2].ChType == Character.type.evil)
            {
                if (result > 0)
                {
                    //floorToMove.CharactersList[floorToMove.CharactersList.Count - 1].Level = 11;
                    floorToMove.CharactersList[floorToMove.CharactersList.Count - 1].Level += floorToMove.CharactersList[floorToMove.CharactersList.Count - 2].Level;
                    floorToMove.RemoveCharacter(floorToMove.CharactersList[floorToMove.CharactersList.Count - 2]);
                }
                else
                {
                    floorToMove.CharactersList[floorToMove.CharactersList.Count - 2].Level += player.Level;
                    floorToMove.CharactersList[floorToMove.CharactersList.Count - 1].Level = 0;

                    floorToMove.RemoveCharacter(player);
                }
            }
            else
            {
                floorToMove.CharactersList[floorToMove.CharactersList.Count - 1].Level += floorToMove.CharactersList[floorToMove.CharactersList.Count - 2].Level;
                floorToMove.RemoveCharacter(floorToMove.CharactersList[floorToMove.CharactersList.Count - 2]);
            }

        }
    }
    public bool IsEmptyAll()
    {
        foreach (var item in floorlist)
        {        
            if(item.charactersList.Count > 0) //estaba ==
            {
                return false;
            }
        }
        currentLevel++;
        StartCoroutine(ActivateNextLevelScreen());
        return true;
    }
    private IEnumerator ActivateNextLevelScreen()
    {
        yield return new WaitForSeconds(1f); // Tiempo de espera de 1 segundos

        nextLevelScreen.SetActive(true);
        nextLevelButton.gameObject.SetActive(true);
    }

    public void CreateLevel(int level)
    {
        float posicion = 26;
        MoveSystem character = FindFirstObjectByType<MoveSystem>();
        // L�gica para crear el nivel
        switch (level)
        {
            case 1:

                
                TowerCharacterGenerator();
                character.transform.position = character.maldito.transform.position;
                character.level = 20;
                TowerGenerator(3, 2, 2);
                TowerGenerator(3, 2, posicion);
                TowerGenerator(3, 2, posicion*2);
                floorlist = FindObjectsOfType<Floor>();
                towerList=FindObjectsOfType<Tower>();
                break;
            case 2:
                
                TowerCharacterGenerator();
                character.transform.position = character.maldito.transform.position;
                TowerGenerator(4, 3, 2);
                character.level = 50;
                TowerGenerator(4, 3, posicion);
                TowerGenerator(4, 3, posicion * 2);
                TowerGenerator(4, 3, posicion * 3);
                floorlist = FindObjectsOfType<Floor>();
                towerList = FindObjectsOfType<Tower>();
                break;
            case 3:
                
                TowerCharacterGenerator();
                character.transform.position = character.maldito.transform.position;
                character.level = 70;
                TowerGenerator(5, 3, 2);
                TowerGenerator(5, 3, posicion);
                TowerGenerator(5, 3, posicion * 2);
                TowerGenerator(5, 3, posicion * 3);
                TowerGenerator(5, 3, posicion * 4);
                floorlist = FindObjectsOfType<Floor>();
                towerList = FindObjectsOfType<Tower>();
                break;
            default:
                
                // Se ha completado el �ltimo nivel, mostrar mensaje de finalizaci�n o hacer algo m�s
                Debug.Log("�Has completado todos los niveles!");
                WonScreen.SetActive(true);
                WonButton.gameObject.SetActive(true);
                WonButton.onClick.AddListener(BackToTheStart);

                break;
        }
    }
    public void BackToTheStart()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void Delete()
    {
        foreach (var item in floorlist)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in towerList)
        {
            Destroy(item.gameObject);
        }

        CreateLevel(currentLevel);
    }
}
