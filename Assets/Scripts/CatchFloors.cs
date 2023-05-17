using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchFloors : MonoBehaviour
{
    Floor floor;
    // Start is called before the first frame update
    void Start()
    {
        floor=GetComponentInParent<Floor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            floor.charactersList.Add(collision.GetComponent<Character>());
        }
    }
}
