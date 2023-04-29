using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    //public GameObject floorFight;
    //public GameObject[] floorFights;
    public List<GameObject> floorFights = new List<GameObject>();
    public bool isOnFloor;
    private bool moving;

    private float startPosX;
    private float startPosY;
    private bool finish;
    public GameObject maldito;

    public Transform resetPosition;
    BoxCollider2D coll;


    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        resetPosition = this.transform;
        AddFloors(); // Llama a la función AddFloors()
        isOnFloor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (finish == false)
        {
            if (moving)
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
            }
        }
    }

    public void OnMouseDown()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            moving = true;
            coll.isTrigger = true;
        }
    }
    public void OnMouseUp()
    {
        
         moving = false;
         bool foundFloorFight = false;
        coll.isTrigger = false;

         /*foreach (GameObject floorFight in floorFights)
         {
             if (Math.Abs(this.transform.localPosition.x - floorFight.transform.localPosition.x) <= 0.5f && Math.Abs(this.transform.localPosition.y - floorFight.transform.localPosition.y) <= 0.5f)
             {
                 this.transform.position = new Vector3(floorFight.transform.position.x, floorFight.transform.position.y, floorFight.transform.localPosition.z);
                 foundFloorFight = true;
                 break;
             }
         }

         if (!foundFloorFight)
         {
             this.transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
         }*/
        if(isOnFloor)
        {

        }
        else
            this.transform.position = maldito.transform.position;

    }

    private void AddFloors() //función para agregar los pisos automáticamente
    {
        /*GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower"); //se buscan las torres en la escena
         foreach (GameObject tower in towers)
         {
             foreach (Transform child in tower.transform) //se busca en los hijos de la torre los objetos con el tag "Floor"
             {
                 if (child.gameObject.CompareTag("Floor"))
                 {
                     floorFights.Add(child.gameObject); //se agregan los objetos a la lista de pisos
                 }
             }
         }
         Debug.Log("AddFloors: " + floorFights.Count);*/
        GameObject[] floorFightObjects = GameObject.FindGameObjectsWithTag("FloorFight");
        foreach (GameObject floorFight in floorFightObjects)
        {
            floorFights.Add(floorFight);
        }
        Debug.Log("Floor added: " + floorFights.Count);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Catch"))
        {
            isOnFloor = true;
        }
        else
            isOnFloor = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Catch"))
        {
            isOnFloor = true;
        }
        else
            isOnFloor = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Catch"))
        {
            isOnFloor = false;
            Floor floor=collision.GetComponent<Floor>();
            //floor.AddCharacter(gameObject);
        }
    }
}
