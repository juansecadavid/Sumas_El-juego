using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sumas__TheGame
{
    internal class GameManager
    {
        public static Tower TowerCharacterGenerator()
        {
            List<Floor> floorList= FloorGeneratorForMain(2,1,Character.type.main);
            Tower tower = new Tower(floorList,Character.type.main);

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
                    Character character = CharacterGenerator(type);
                    list.Add(character);
                }
                Floor floor = new Floor(list);
                floorList.Add(floor);
            }
            return floorList;
        }

        public static List<Floor> FloorGeneratorForMain(int numberFloors, int numberCharacters, Character.type type)
        {
            List<Floor> floorList = new List<Floor>();
            
            Random random = new Random();
            int rand = random.Next(0, 2);

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
        public static void AddFloor(Tower tower, Floor floor)
        {
            tower.FloorList.Add(floor);
        }
        public static void MoveAndFight(PlayerController player, Floor actualFloor, Floor floorToMove)
        {

            if(floorToMove.CharactersList.Count>1)
            {
                actualFloor.RemoveCharacter(player);
                floorToMove.AddCharacter(player);

                int result = floorToMove.CharactersList[floorToMove.CharactersList.Count - 1].Level - floorToMove.CharactersList[floorToMove.CharactersList.Count - 2].Level;
                if (result > 0)
                {
                    floorToMove.RemoveCharacter(floorToMove.CharactersList[floorToMove.CharactersList.Count - 2]);
                }
                else
                    floorToMove.RemoveCharacter(player);
            }
            
        }
    }
}
