using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum type
    {
        bueno,malo
    }
    private type tipo;
    private int level;

    public Character(int level, type characterType)
    {
        this.level = level;
        this.tipo= characterType;
    }

    public int Level { get => level; set => level = value; }
    private type Tipo { get => tipo; set => tipo = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Die(int Character1,int character2)
    {
      
    }
}
