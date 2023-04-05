using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int enemyCounter = 0;

    public static Tower TowerCharacterGenerator()
    {
        List<Floor> floorList = FloorGeneratorForMain(2, Character.type.main);
        Tower tower = new Tower(floorList, Character.type.main);

        return tower;
    }
    public static Tower TowerGenerator(int numberFloor, int numberCharacter)
    {
        int numberFloors = numberFloor;
        int numberCharcters = numberCharacter;

        List<Floor> floorList;
        floorList = FloorGenerator(numberFloors, numberCharcters, Character.type.evil);

        Tower tower = new Tower(floorList, Character.type.evil);
        return tower;
    }
    public static List<Floor> FloorGenerator(int numberFloors, int numberCharacters, Character.type type)
    {
        List<Floor> floorList = new List<Floor>();

        for (int i = 0; i < numberFloors; i++)
        {
            List<Character> list = new List<Character>();

            for (int j = 0; j < numberCharacters; j++)
            {
                Character character = EnemyGenerator(type);
                list.Add(character);
            }
            enemyCounter++;
            Floor floor = new Floor(list);
            floorList.Add(floor);
        }
        return floorList;
    }

    public static List<Floor> FloorGeneratorForMain(int numberFloors, Character.type type)
    {
        List<Floor> floorList = new List<Floor>();
        int rand = Random.Range(0, 2);

        for (int i = 0; i < numberFloors; i++)
        {
            List<Character> list = new List<Character>();

            if (rand == i)
            {
                Character character = CharacterGenerator(type);
                list.Add(character);
            }

            Floor floor = new Floor(list);
            floorList.Add(floor);
        }
        return floorList;
    }
    public static Character CharacterGenerator(Character.type type)
    {
        Character character = new Character(7, type);
        return character;
    }
    public static Character EnemyGenerator(Character.type type)
    {

        Character character = new Character((4 + enemyCounter * 2), type);
        //Character character = new Character((4), type);

        return character;
    }
    public static void MoveAndFight(Character player, Floor actualFloor, Floor floorToMove, Tower originTower)
    {

        if (floorToMove.CharactersList.Count > 0)
        {

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
