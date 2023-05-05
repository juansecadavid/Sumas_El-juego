using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
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
    float count = -13;
    float countCharacterX = -2;
    Tower EnemyTower;
    Floor EnemyFloor;

    public void Start()
    {
        
        EnemyTower = towerTry.GetComponent<Tower>();
        //EnemyFloor=floorTry.GetComponent<Floor>();
        TowerCharacterGenerator();
        TowerGenerator(3, 3);
        //Instantiate(playerTry, new Vector3(-17, -13f, 0), Quaternion.identity);

    }

    public void TowerCharacterGenerator()
    {
        List<Floor> floorList = FloorGeneratorForMain(2, Character.type.main);
        //Tower tower = new Tower(floorList, Character.type.main);

        //Tower tower = towerTry.AddComponent<Tower>();

        Instantiate(towerTry, new Vector3(-20, -1.5f, 0), Quaternion.identity);

        //return tower;
    }
    public void TowerGenerator(int numberFloor, int numberCharacter)
    {
        int numberFloors = numberFloor;
        int numberCharcters = numberCharacter;

        List<Floor> floorList;
        floorList = FloorGenerator(numberFloors, numberCharcters, Character.type.evil);

        //Tower tower = new Tower(floorList, Character.type.evil);
        EnemyTower.FloorList = floorList;

        Instantiate(towerTry, new Vector3(2, -1.5f, 0), Quaternion.identity);
        //return tower;
    }
    public List<Floor> FloorGenerator(int numberFloors, int numberCharacters, Character.type type)
    {
        List<Floor> floorList = new List<Floor>();



        for (int i = 0; i < numberFloors; i++)
        {
            List<Character> list = new List<Character>();
            countCharacterX = 0f;

            for (int j = 0; j < numberCharacters; j++)
            {
                Character character = EnemyGenerator(type, countCharacterX);
                list.Add(character);


            }
            enemyCounter++;
            //Floor floor = new Floor(list);
            //floorList.Add(floor);

            Instantiate(floorTry, new Vector3(2, count, 0), Quaternion.identity);
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

            //aquí se debería crear un piso

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
    public Character EnemyGenerator(Character.type type, float countCharacterX)
    {

        Character character = new Character((4 + enemyCounter * 2), type);
        Instantiate(enemyTry, new Vector3(countCharacterX, count, 0), Quaternion.identity);
        this.countCharacterX += 2f;
        //Character character = new Character((4), type);

        return character;
    }

    public static void MoveAndFight(Character player, Floor actualFloor, Floor floorToMove, Tower originTower)
    {
        if (floorToMove.CharactersList.Count > 0)
        {
            Console.WriteLine("El combate inicio");
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
}
