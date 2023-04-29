using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    //public GameObject floorFight;
    //public GameObject[] floorFights;
    public List<GameObject> floorFights = new List<GameObject>();
    private bool moving;

    private float startPosX;
    private float startPosY;
    private bool finish;

    private Vector3 resetPosition;


    // Start is called before the first frame update
    void Start()
    {
        resetPosition = this.transform.localPosition;
        foreach (GameObject floor in GameObject.FindGameObjectsWithTag("FloorFight"))
        {
            floorFights.Add(floor);
        }
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
        Debug.Log("mouse down");
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            moving = true;
        }
       
    }
    public void OnMouseUp()
    {
        Debug.Log("mouse up");
        moving = false;
         bool foundFloorFight = false;

         foreach (GameObject floorFight in floorFights)
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
         }
      
    }

    private void AddFloors() //función para agregar los pisos automáticamente
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower"); //se buscan las torres en la escena
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
        Debug.Log("AddFloors");
    }

}
