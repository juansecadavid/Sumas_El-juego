using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    private static EnemyFactory instance;

    [SerializeField]
    GameObject product;

    [SerializeField]
    Sprite sprite1, sprite2;

    Sprite enemySprite;

    private GameObject productInstance;

    public static EnemyFactory Instance { get => instance; private set => Instance = value; }

    public GameObject DeliverNewProduct(float countCharacterX, float xPos, float count, int rand)
    {
        product.GetComponent<Character>().level = rand;
        int randSprite = Random.Range(0, 2);
        if (randSprite == 0)
        {
            enemySprite = sprite1;
        }
        else
        {
            enemySprite = sprite2;
        }
        product.GetComponent<SpriteRenderer>().sprite = enemySprite;

        //Debug.Log("factory working");
        productInstance = Instantiate(product, new Vector3(countCharacterX + xPos, count, 0), Quaternion.identity);
        return productInstance;
    }
}
