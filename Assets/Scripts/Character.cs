using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int level;
    private string characterType;

    public Character(int level, string characterType)
    {
        this.level = level;
        this.characterType = characterType;
    }

    public int Level { get => level; set => level = value; }
    public string CharacterType { get => characterType; set => characterType = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
