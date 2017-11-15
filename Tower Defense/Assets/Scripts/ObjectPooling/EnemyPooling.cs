using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour {
    public List<GameObject> enemyRefrence;
    public List<GameObject> enemyPool;
    public List<bool> enemyinUse;
    public List<EntityType> enemyType;
    bool enemyPoolPopulated;

    // Use this for initialization
    void Awake()
    {
        enemyinUse = new List<bool>();
        enemyPool = new List<GameObject>();
        enemyType = new List<EntityType>();
        enemyPoolPopulated = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject InstantiateObject(EntityType type, Vector3 instatiatePoint)
    {
        int index;
        GameObject placeHolder;
        if (enemyPoolPopulated)
        {
            Debug.Log("EnemyPool:" + enemyPool.Count);

            for (int i = 0; i < enemyPool.Count && enemyPool.Count != 0; i++)
            {

                if (!enemyinUse[i]&&enemyType[i]==type)
                {

                    enemyPool[i].SetActive(true);
                    enemyPool[i].transform.position = instatiatePoint;
                    placeHolder = enemyPool[i];
                    enemyPoolPopulated = true;
                    return placeHolder;
                }
            }
        }
        placeHolder = AddObjectToPool(type,instatiatePoint);
        enemyPoolPopulated = true;
        Debug.Log("New Object Created");

        return placeHolder;
    }

    GameObject AddObjectToPool(EntityType type,Vector3 instatiatePoint)
    {
        int index = 0;
        GameObject newEnemy;

        if (enemyPoolPopulated)
        {
            index = enemyPool.Count;
            Debug.Log(index);
        }
        else
        {
            Debug.Log("IndexStart");
            index = 0;
        }

        switch (type)
        {


            case EntityType.Ghost:
                newEnemy = GameObject.Instantiate(enemyRefrence[0], instatiatePoint, Quaternion.identity);
                Debug.Log("This:" + newEnemy);
                newEnemy.GetComponent<EntityInfo>().poolingIndex = index;
                newEnemy.GetComponent<Transform>().name = "Ghost-" + index;
                enemyPool.Add(newEnemy);
                Debug.Log("This:" + enemyPool[0]);
                enemyinUse.Add(true);
                enemyType.Add(type);
                enemyPoolPopulated = true;
                break;

            case EntityType.Slime:
                newEnemy = GameObject.Instantiate(enemyRefrence[1], instatiatePoint, Quaternion.identity);
                newEnemy.GetComponent<EntityInfo>().poolingIndex = index;
                newEnemy.GetComponent<Transform>().name = "Slime-" + index;
                enemyPool.Add(newEnemy);
                enemyinUse.Add(true);
                enemyType.Add(type);
                enemyPoolPopulated = true;
                break;


            case EntityType.Rabbit:
                newEnemy = GameObject.Instantiate(enemyRefrence[2], instatiatePoint, Quaternion.identity);
                newEnemy.GetComponent<EntityInfo>().poolingIndex = index;
                newEnemy.GetComponent<Transform>().name = "Rabbuit-" + index;
                enemyPool.Add(newEnemy);
                enemyinUse.Add(true);
                enemyType.Add(type);
                enemyPoolPopulated = true;
                break;

            case EntityType.Bat:
                newEnemy = GameObject.Instantiate(enemyRefrence[3], instatiatePoint, Quaternion.identity);
                newEnemy.GetComponent<EntityInfo>().poolingIndex = index;
                newEnemy.GetComponent<Transform>().name = "Bat-" + index;
                enemyPool.Add(newEnemy);
                enemyType.Add(type);
                enemyinUse.Add(true);
                enemyType.Add(type);
                enemyPoolPopulated = true;
                break;

            case EntityType.Oger:
                newEnemy = GameObject.Instantiate(enemyRefrence[4], instatiatePoint, Quaternion.identity);
                newEnemy.GetComponent<EntityInfo>().poolingIndex = index;
                newEnemy.GetComponent<Transform>().name = "Oger-" + index;
                enemyPool.Add(newEnemy);
                enemyinUse.Add(true);
                enemyType.Add(type);
                enemyPoolPopulated = true;
                break;

            case EntityType.Spider:
                newEnemy = GameObject.Instantiate(enemyRefrence[5], instatiatePoint, Quaternion.identity);
                newEnemy.GetComponent<EntityInfo>().poolingIndex = index;
                newEnemy.GetComponent<Transform>().name = "Spider-" + index;
                enemyPool.Add(newEnemy);
                enemyinUse.Add(true);
                enemyType.Add(type);
                enemyPoolPopulated = true;
                break;

            default:
                Debug.Log("Non Compatable type");
                newEnemy = null;
                break;
        }

        return newEnemy;

    }

    public void DisableObject(int index, GameObject self)
    {
        Debug.Log("Index:"+index);
        enemyPool[index].SetActive(false);
        Debug.Log("Hello");
        enemyinUse[index] = false;



    }
}
