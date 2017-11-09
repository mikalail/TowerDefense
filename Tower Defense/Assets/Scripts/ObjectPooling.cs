using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PooledObject
{
    public GameObject objectRefrence;
    public bool inUse;
    public Type type;
    public int index;
}


public class ObjectPooling : MonoBehaviour {
    public GameObject debugObject;
    public List<GameObject> enemyRefrences;
    public List<GameObject> towerRefrences;
    public List<GameObject> projectileRefrences;
    public List<GameObject> soldierRefrences;
    public List<PooledObject> soldierPool;
    public List<PooledObject> enemyPool;
    public List<PooledObject> projectilePool;
    public List<PooledObject> towrerPool;
    private bool soldierPoolPopulated = false;
    private bool enemyPoolPopulated = false;
    private bool projectilePoolPopulated = false;
    private bool towerPoolPopulated = false;

    // Use this for initialization
    void Start() {
        soldierPool =new List<PooledObject>();
        enemyPool = new List<PooledObject>();
        projectilePool = new List<PooledObject>();
        towrerPool =new List<PooledObject>();
    }


    // Update is called once per frame
    void Update() {

    }

   public PooledObject InstantiateObject(Type type, Vector3 instatiatePoint)
    {
      
        bool freeObject = false;
        PooledObject placeHolder;

        placeHolder.objectRefrence = null;
        placeHolder.index = -1;
        placeHolder.inUse = false;
        placeHolder.type = Type.NULL;

        switch (type)
        {

            case Type.Bullet:

                Debug.Log("Instantiation Attempted");
                if(projectilePoolPopulated)
                {
                    for (int i = 0; i < projectilePool.Count && projectilePool.Count!=0; i++)
                    {
                    
                        placeHolder = projectilePool[i];
                        if (!placeHolder.inUse && placeHolder.type == type)
                        {
                            placeHolder.objectRefrence.transform.position = instatiatePoint;
                            placeHolder.objectRefrence.transform.position = instatiatePoint;
                            placeHolder.objectRefrence.SetActive(true);
                            placeHolder.inUse = true;
                            placeHolder.index = projectilePool[i].index;
                            freeObject = true;
                            
                        }
                    }
                }

                if (!freeObject)
                {
                    placeHolder = AddObjectToPool(type, instatiatePoint);
                    projectilePoolPopulated = true;
                    Debug.Log("New Object Created");
                }



                break;
            case Type.Enemy1:
            case Type.Enemy2:
            case Type.Enemy3:

                for (int i = 0; i < enemyPool.Count; i++)
                {
                    placeHolder = enemyPool[i];
                    if (!placeHolder.inUse && placeHolder.type == type)
                    {
                        placeHolder.objectRefrence.transform.position = instatiatePoint;
                        placeHolder.objectRefrence.transform.position = instatiatePoint;
                        placeHolder.objectRefrence.SetActive(true);
                        placeHolder.inUse = true;
                        placeHolder.index = i;
                        freeObject = true;
                    }
                }
                if (!freeObject)
                {
                    placeHolder = AddObjectToPool(type, instatiatePoint);
                }
                break;

            case Type.Soldier:

                for (int i = 0; i < soldierPool.Count; i++)
                {
                    placeHolder = soldierPool[i];
                    if (!placeHolder.inUse && placeHolder.type == type)
                    {
                        placeHolder.objectRefrence.transform.position = instatiatePoint;
                        placeHolder.objectRefrence.transform.position = instatiatePoint;
                        placeHolder.objectRefrence.SetActive(true);
                        placeHolder.inUse = true;
                        placeHolder.index = i;
                        freeObject = true;
                    }
                }
                if (!freeObject)
                {
                    placeHolder = AddObjectToPool(type, instatiatePoint);
                }
                break;
            default:
                placeHolder = AddObjectToPool(type, instatiatePoint);
                break;
        }

        return placeHolder;
        
    }


    PooledObject AddObjectToPool(Type type, Vector3 instantinatePoint)
    {
        PooledObject newObject;


        switch (type) {

            case Type.Bullet:

                newObject.type = type;
                newObject.inUse = true;
                newObject.objectRefrence = GameObject.Instantiate(projectileRefrences[0], instantinatePoint, Quaternion.identity);
              

                if (projectilePoolPopulated)
                {
                    newObject.index = projectilePool.Count;
                }
                else
                {
                    newObject.index = 0;
                }
                newObject.objectRefrence.GetComponent<Bullet>().poolingIndex = newObject.index;
                newObject.objectRefrence.GetComponent<Transform>().name="Bullet"+newObject.index;
                projectilePool.Add(newObject);
               

                break;

            case Type.Enemy1:

                newObject.type = type;
                newObject.inUse = true;
                newObject.objectRefrence = GameObject.Instantiate(enemyRefrences[0], instantinatePoint, Quaternion.identity);
                if (enemyPoolPopulated)
                {
                    newObject.index = enemyPool.Count;
                }
                else
                {
                    newObject.index = 0;
                }
                enemyPool.Add(newObject);

                newObject.objectRefrence.GetComponent<EntityInfo>().poolingIndex = newObject.index;

                break;

            case Type.Enemy2:

                newObject.type = type;
                newObject.inUse = true;
                newObject.objectRefrence = GameObject.Instantiate(enemyRefrences[2], instantinatePoint, Quaternion.identity);
                if (enemyPoolPopulated)
                {
                    newObject.index = enemyPool.Count;
                }
                else
                {
                    newObject.index = 0;
                }

                newObject.objectRefrence.GetComponent<EntityInfo>().poolingIndex = newObject.index;
                enemyPool.Add(newObject);
                
                break;

            case Type.Enemy3:

                newObject.type = type;
                newObject.inUse = true;
                newObject.objectRefrence = GameObject.Instantiate(enemyRefrences[3], instantinatePoint, Quaternion.identity);
                if (enemyPoolPopulated)
                {
                    newObject.index = enemyPool.Count;
                }
                else
                {
                    newObject.index = 0;
                }
                newObject.objectRefrence.GetComponent<EntityInfo>().poolingIndex = newObject.index;
                enemyPool.Add(newObject);

                break;

            case Type.Soldier:
                newObject.type = type;
                newObject.inUse = true;
                newObject.objectRefrence = GameObject.Instantiate(soldierRefrences[0], instantinatePoint, Quaternion.identity);
                if (soldierPoolPopulated)
                {
                    newObject.index = soldierPool.Count;
                }
                else
                {
                    newObject.index = 0;
                }
                newObject.objectRefrence.GetComponent<EntityInfo>().poolingIndex = newObject.index;
                soldierPool.Add(newObject);
               
                break;

            default:
                Debug.Log("ObjectPooling: NonExistant Type. Debug Object Returned");

                newObject.type = type;
                newObject.inUse = true;
                newObject.objectRefrence = GameObject.Instantiate(debugObject, instantinatePoint, Quaternion.identity);
                newObject.index = -1;
                break;


        }
        return newObject;

    }

    public void DisableObject(int index,Type type,GameObject self)
    {
        PooledObject placeHolder;

        switch (type)
        {

            case Type.Bullet:

                placeHolder = projectilePool[index];
                placeHolder.objectRefrence.SetActive(false);
                placeHolder.inUse = false;

                break;

            case Type.Enemy1:
            case Type.Enemy2:
            case Type.Enemy3:

                placeHolder = enemyPool[index];
                placeHolder.objectRefrence.SetActive(false);
                placeHolder.inUse = false;

                break;
       
            case Type.Soldier:
                placeHolder = soldierPool[index];
                placeHolder.objectRefrence.SetActive(false);
                placeHolder.inUse = false;

                break;
            default:
                Destroy(self);
                break;
        }


    }

}
