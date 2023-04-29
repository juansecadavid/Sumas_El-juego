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
    public void RemoveCharacter(Character character)
    {
        for (int i = 0; i < charactersList.Count; i++)
        {
            if (charactersList[i] == character)
            {
                charactersList.RemoveAt(i);
                break;
            }
        }
    }
    public void AddCharacter(Character character)
    {
        charactersList.Add(character);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) ;
    }
}
