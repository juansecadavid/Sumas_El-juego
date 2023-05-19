using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Character : MonoBehaviour
{
  
    public enum type
    {
        main,
        evil,
        help
    }
    private type chType;
    public int level;

    public Character(int level, type characterType)
    {
        this.level = level;
        this.chType = characterType;
       
    }

    public int Level { get => level; set => level = value; }
    public type ChType { get => chType; set => chType = value; }
    
    
}
