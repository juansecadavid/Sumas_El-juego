using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Tower TowerGenerator(bool isFromEnemy)
    {
        int numberFloors = Random.Range(1, 3);
        int numberCharcters=Random.Range(1, 3);
        List<Floor> floorList;

        if (isFromEnemy)
        {
            floorList = FloorGenerator(numberFloors, numberCharcters, Character.type.malo);
        }
        else
        {
            floorList= FloorGenerator(numberFloors, numberCharcters, Character.type.bueno);
        }
            
        Tower tower=new Tower(floorList,isFromEnemy);
        return tower;
    }
    public List<Floor> FloorGenerator(int numberFloors, int numberCharacters,Character.type type)
    {
        List<Floor> floorList=new List<Floor>();
        List<Character> list = new List<Character>();
        for (int i = 0; i < numberFloors; i++)
        {
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
    public Character CharacterGenerator(Character.type type)
    {
        Character character = new Character(0,type);
        return character;
    }
    public void AddFloor(Tower tower,Floor floor)
    {
        tower.FloorList.Add(floor);
    }
}
