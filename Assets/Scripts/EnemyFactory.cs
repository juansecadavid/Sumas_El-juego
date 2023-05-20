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

    int enemysCount = 0;

    private GameObject productInstance;

    public static EnemyFactory Instance { get => instance; private set => Instance = value; }

    public GameObject DeliverNewProduct(float countCharacterX, float xPos, float count, int currentLevel)
    {
        int rand = 0;

        if (currentLevel == 1)
        {
            rand = Random.Range(14 + (int)(enemysCount * 18f), 23 + (int)(enemysCount * 24));
            // enemyTry.GetComponent<Character>().level = rand;
            //levelTextEnemys.SetText(rand.ToString());
        }
        if (currentLevel == 2)
        {
            //rand = Random.Range(30, 80);
            rand = Random.Range(30 + (int)(enemysCount * 18f), 40 + (int)(enemysCount * 24));


        }
        if (currentLevel == 3)
        {
            //rand = Random.Range(60, 95);
            rand = Random.Range(60 + (int)(enemysCount * 18f), 70 + (int)(enemysCount * 24));

        }

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

        enemysCount++;

        //Debug.Log("factory working");
        productInstance = Instantiate(product, new Vector3(countCharacterX + xPos, count, 0), Quaternion.identity);
        return productInstance;
    }
}
