using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MoveSystem : MonoBehaviour
{
    //public GameObject floorFight;
    //public GameObject[] floorFights;
    public AudioSource clip1;
    public AudioSource clip2;

    public List<GameObject> floorFights = new List<GameObject>();
    public bool isOnFloor;
    private bool moving;

    private float startPosX;
    private float startPosY;
    private bool finish;
    public GameObject maldito;

    public Transform resetPosition;
    BoxCollider2D coll;
    public Floor actualFloor;
    public TextMeshProUGUI levelText;
    public int level;
    public int score;
    public TextMeshProUGUI label;



    private static MoveSystem instance;

    public static MoveSystem Instance { get => instance; private set => instance = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        resetPosition = this.transform;
        AddFloors(); // Llama a la funci�n AddFloors()
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
        //levelText.text = $"{level}";
        label.text = score.ToString();
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
            Fight();
        }
        else
            this.transform.position = maldito.transform.position;

    }

    private void AddFloors() //funci�n para agregar los pisos autom�ticamente
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
            Floor floor = collision.GetComponentInParent<Floor>();
            actualFloor = floor;
        }

            //isOnFloor = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Catch"))
        {
            isOnFloor = true;
        }
            //isOnFloor = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Catch"))
        {
            isOnFloor = false;
            
            //floor.AddCharacter(gameObject);
        }
    }
    void Fight()
    {
        if (actualFloor.charactersList.Count > 0)
        {
            int result = level - actualFloor.CharactersList[actualFloor.charactersList.Count - 1].Level;
            if (result > 0)
            {
                
                actualFloor.CharactersList[actualFloor.charactersList.Count - 1].gameObject.SetActive(false);
                clip1.Play();
                actualFloor.RemoveCharacter(actualFloor.CharactersList[actualFloor.charactersList.Count - 1]);
                Debug.Log("En combate");

                if (actualFloor.charactersList.Count == 0)
                {
                    score += level;
                    Debug.Log(score);
                    Debug.Log("Gan�");
                }

            }
            else
            {
                score -= level;
                clip2.Play();
                Debug.Log("Herido");

                if (score==0)
                {
                    Debug.Log("Perdio");
                }
            }
        }
        else
            Debug.Log("Vac�o");
    }
}
