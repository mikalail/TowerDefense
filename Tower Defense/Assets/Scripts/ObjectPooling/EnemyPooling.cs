using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooling : MonoBehaviour {
    public List<GameObject> enemyRefrence;
    public List<GameObject> enemyPool;
    public List<bool> enemyinUse;
    public List<EntityType> enemyType;
    bool bulletPoolPopulated;

    // Use this for initialization
    void Start()
    {
        enemyinUse = new List<bool>();
        enemyPool = new List<GameObject>();
        enemyType = new List<EntityType>();
        bulletPoolPopulated = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject InstantiateObject(EntityType type, Vector3 instatiatePoint)
    {
        int index;
        GameObject placeHolder;
        if (bulletPoolPopulated)
        {
            for (int i = 0; i < enemyPool.Count && enemyPool.Count != 0; i++)
            {

                if (!enemyinUse[i]&&enemyType[i]==type)
                {

                    enemyPool[i].SetActive(true);
                    enemyPool[i].transform.position = instatiatePoint;
                    placeHolder = enemyPool[i];
                    return placeHolder;
                }
            }
        }
        placeHolder = AddObjectToPool(type,instatiatePoint);
        bulletPoolPopulated = true;
        Debug.Log("New Object Created");

        return placeHolder;
    }

    GameObject AddObjectToPool(EntityType type,Vector3 instatiatePoint)
    {
        int index = 0;
        GameObject newEnemy;

        if (bulletPoolPopulated)
        {
            index = enemyPool.Count;
        }
        else
        {
            index = 0;
        }

        switch (type)
        {


            case EntityType.Ghost:
                newEnemy = GameObject.Instantiate(enemyRefrence[0], instatiatePoint, Quaternion.identity);
                newEnemy.GetComponent<Bullet>().poolingIndex = index;
                newEnemy.GetComponent<Transform>().name = "Bullet" + index;
                enemyPool.Add(newEnemy);
                enemyinUse.Add(true);
                enemyType.Add(type);
                break;

            case EntityType.Slime:
                newEnemy = GameObject.Instantiate(enemyRefrence[1], instatiatePoint, Quaternion.identity);
                newEnemy.GetComponent<Bullet>().poolingIndex = index;
                newEnemy.GetComponent<Transform>().name = "Bullet" + index;
                enemyPool.Add(newEnemy);
                enemyinUse.Add(true);
                enemyType.Add(type);
                break;


            case EntityType.Rabbit:
                newEnemy = GameObject.Instantiate(enemyRefrence[2], instatiatePoint, Quaternion.identity);
                newEnemy.GetComponent<Bullet>().poolingIndex = index;
                newEnemy.GetComponent<Transform>().name = "Bullet" + index;
                enemyPool.Add(newEnemy);
                enemyinUse.Add(true);
                enemyType.Add(type);
                break;

            case EntityType.Bat:
                newEnemy = GameObject.Instantiate(enemyRefrence[3], instatiatePoint, Quaternion.identity);
                newEnemy.GetComponent<Bullet>().poolingIndex = index;
                newEnemy.GetComponent<Transform>().name = "Bullet" + index;
                enemyPool.Add(newEnemy);
                enemyType.Add(type);
                enemyinUse.Add(true);
                enemyType.Add(type);
                break;

            case EntityType.Oger:
                newEnemy = GameObject.Instantiate(enemyRefrence[4], instatiatePoint, Quaternion.identity);
                newEnemy.GetComponent<Bullet>().poolingIndex = index;
                newEnemy.GetComponent<Transform>().name = "Bullet" + index;
                enemyPool.Add(newEnemy);
                enemyinUse.Add(true);
                enemyType.Add(type);
                break;

            case EntityType.Spider:
                newEnemy = GameObject.Instantiate(enemyRefrence[5], instatiatePoint, Quaternion.identity);
                newEnemy.GetComponent<Bullet>().poolingIndex = index;
                newEnemy.GetComponent<Transform>().name = "Bullet" + index;
                enemyPool.Add(newEnemy);
                enemyinUse.Add(true);
                enemyType.Add(type);
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
        enemyinUse[index] = false;



    }
}
