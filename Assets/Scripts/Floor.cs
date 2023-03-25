using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private List<Character> charactersList;

    public Floor(List<Character> charactersList)
    {
        this.charactersList = charactersList;
    }

    internal List<Character> CharactersList { get => charactersList; set => charactersList = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveFloor()
    {

    }
}
