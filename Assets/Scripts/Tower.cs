using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private List<Floor> floorList;
    private bool isFromEnemy;

    public Tower(List<Floor> floorList, bool isFromEnemy)
    {
        this.floorList = floorList;
        this.isFromEnemy = isFromEnemy;
    }

    public bool IsFromEnemy { get => isFromEnemy; set => isFromEnemy = value; }
    internal List<Floor> FloorList { get => floorList; set => floorList = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void DestroyTower()
    {
        Destroy(gameObject);
    }
}
