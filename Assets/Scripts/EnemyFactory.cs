using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private static EnemyFactory instance;

    [SerializeField]
    GameObject product;

    private GameObject productInstance;

    public static EnemyFactory Instance { get => instance; private set => Instance = value; }

    public GameObject DeliverNewProduct(float countCharacterX, float xPos, float count)
    {
        
        //Debug.Log("factory working");
        productInstance = Instantiate(product, new Vector3(countCharacterX + xPos, count, 0), Quaternion.identity);
        return productInstance;
    }
}
