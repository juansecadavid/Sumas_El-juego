using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using System;
using static MoveSystem;


public class NewGameManager : MonoBehaviour
{
    public static int enemyCounter = 0;

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
    Floor EnemyFloor;
    public Sprite sprite1;
    public Sprite sprite2;
    Sprite enemySprite;
    Floor[] floorlist;

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
        TowerCharacterGenerator();
        
        TowerGenerator(4, 3, 2);
        TowerGenerator(4, 3, 26);
        //Instantiate(playerTry, new Vector3(-17, -13f, 0), Quaternion.identity);
        floorlist = FindObjectsOfType<Floor>();

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
    /*public bool IsEmptyAll()
    {
        foreach (var item in floorlist)
        {        
            if(item.charactersList.Count == 0)
            {
                return false;
            }
        }
        return true;
    }*/
}
