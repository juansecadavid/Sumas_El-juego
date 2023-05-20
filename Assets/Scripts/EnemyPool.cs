using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPool : MonoBehaviour
{
    private static EnemyPool instance;

    [SerializeField]
    private int size = 40;

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    EnemyFactory enemyFactory;

    private List<GameObject> instances = new List<GameObject>();

    public static EnemyPool Instance { get => instance; private set => Instance = value; }
    
    public GameObject Retrieve()
    {
        GameObject target = instances[0];
        target.transform.parent = null;
        instances.Remove(target);
        target.SetActive(true);
        return target;
    }

    public void Recycle(GameObject target)
    {
        target.SetActive(false);
        target.transform.position = transform.position;
        target.transform.rotation = Quaternion.identity;
        target.transform.parent = transform;
        instances.Add(target);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (enemyPrefab != null)
        {
            for (int i = 0; i < size; i++)
            {
                AddNewInstanceToPool();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    private void AddNewInstanceToPool()
    {
        GameObject newInstance = enemyFactory.DeliverNewProduct(); ;
        Recycle(newInstance);
    }

}
