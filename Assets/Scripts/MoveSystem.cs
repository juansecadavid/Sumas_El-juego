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
    public AudioSource clip1, clip2, clip3, clip4;

    public List<GameObject> floorFights = new List<GameObject>();
    public bool isOnFloor;
    public bool moving;

    [SerializeField]
    EnemyPool enemyPool;

    private float startPosX;
    private float startPosY;
    public bool finish;
    public GameObject maldito;

    public Transform resetPosition;
    BoxCollider2D coll;
    public Floor actualFloor;
    public TextMeshProUGUI levelText;
    public int level;
    public int score;
    public TextMeshProUGUI label;
    Rigidbody2D rigidbody2D;
    public GameObject lossScreen;
    //public Button restartButton;
    public GameObject nextLevelScreen;
    public Button nextLevelButton;



    NewGameManager manager;

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
        rigidbody2D=GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        resetPosition = this.transform;
        AddFloors(); // Llama a la funci�n AddFloors()
        isOnFloor = false;
        score = level;
        finish = false;
        manager = FindObjectOfType<NewGameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
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
            //rigidbody2D.gravityScale = 0;
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            moving = true;
            coll.isTrigger = true;
            Debug.Log("Presionando");
        }
    }
    public void OnMouseUp()
    {
        moving = false;
        coll.isTrigger = false;

        Debug.Log("Se Solt�");
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
            //floor.AddCharacter(gameObject);
        }
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
            int result = score - actualFloor.CharactersList[actualFloor.charactersList.Count - 1].Level;
            nextLevelScreen.SetActive(false);
            nextLevelButton.gameObject.SetActive(false);

            if (result > 0)
            {
                int levelEnemy;
                Character character = actualFloor.charactersList[actualFloor.charactersList.Count - 1];
                //actualFloor.CharactersList[actualFloor.charactersList.Count - 1].gameObject.SetActive(false);
                levelEnemy=character.Level;

                int randAudio = UnityEngine.Random.Range(1, 3);

                Debug.Log(randAudio);

                if(randAudio == 1)
                {
                    Debug.Log("sonido1");
                    clip3.Play();
                }
                else
                {
                    Debug.Log("Sonido2");
                    clip1.Play();
                }
                
                actualFloor.RemoveCharacter(actualFloor.CharactersList[actualFloor.charactersList.Count - 1]);
                //Destroy(character.gameObject);

                enemyPool.Recycle(character.gameObject);

                Debug.Log("En combate");
                //if (actualFloor.charactersList.Count == 0)
                //{
                    Debug.Log("Cantidad de characters: " + actualFloor.charactersList.Count);
                    score += levelEnemy;
                    //level = score;
                    Debug.Log(score);
                    Debug.Log("Gan� combate");

                    bool youWon = manager.IsEmptyAll();
                    Debug.Log("youWon: " + youWon);
                    //Debug.Log("Manager: " + manager.IsEmptyAll());

                    if (youWon == false)
                    {
                        

                        //Pon aqu� lo que pasa al ganar;

                    }
                    else 
                    {
                        nextLevelScreen.SetActive(false);
                        nextLevelButton.gameObject.SetActive(false);
                        clip4.Play();
                    }
                    
                //}
            }           
            else
            {
                score -= level;
                level = score;
                clip2.Play();
                Debug.Log("Herido");

                if (score>=0)
                {
                    //pon aqu� lo que pasa al perder;
                    lossScreen.SetActive(true);
                    //restartButton.gameObject.SetActive(true);
                    //restartButton.onClick.AddListener(StartAgain);
                    Debug.Log("Perdio");
                }
            }
        }

        else
           Debug.Log("Vac�o");
   
    }

    public void StartAgain()
    {
        manager.Delete();
        lossScreen.SetActive(false);
        //restartButton.gameObject.SetActive(false);
        Debug.Log("Reinicio");
    }
    private IEnumerator ActivateNextLevelScreen()
    {
        yield return new WaitForSeconds(1f); // Tiempo de espera de 1 segundos

        nextLevelScreen.SetActive(true);
        nextLevelButton.gameObject.SetActive(true);

        
    }
}
