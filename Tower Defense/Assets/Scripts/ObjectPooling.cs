using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PooledObject
{
    public GameObject objectRefrence;
    public bool inUse;
    public EntityType type;
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

   public PooledObject InstantiateObject(EntityType type, Vector3 instatiatePoint)
    {
      
        bool freeObject = false;
        PooledObject placeHolder;

        placeHolder.objectRefrence = null;
        placeHolder.index = -1;
        placeHolder.inUse = false;
        placeHolder.type = EntityType.NULL;

        switch (type)
        {

            case EntityType.Bullet:

              
                if (projectilePoolPopulated)
                {
                    for (int i = 0; i < projectilePool.Count && projectilePool.Count != 0; i++)
                    {

                        placeHolder = projectilePool[i];
                        Debug.Log(""+i+projectilePool[i].inUse);
                        if (!placeHolder.inUse && placeHolder.type == type)
                        {
                           
                           
                            placeHolder.objectRefrence = enemyPool[i].objectRefrence;
                            placeHolder.type = type;
                            placeHolder.inUse = true;
                            placeHolder.index = i;
                            projectilePool[i] = placeHolder;

                            placeHolder.objectRefrence.SetActive(true);
                            placeHolder.objectRefrence.transform.position = instatiatePoint;
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
            case EntityType.Ghost:
            case EntityType.Slime:
            case EntityType.Rabbit:
            case EntityType.Bat:
            case EntityType.Oger:
            case EntityType.Spider:
                if (enemyPoolPopulated)
                {
                    for (int i = 0; i < enemyPool.Count && enemyPool.Count != 0; i++)
                    {
                        placeHolder = enemyPool[i];
                        if (!placeHolder.inUse && placeHolder.type == type)
                        {
                            placeHolder.objectRefrence.transform.position = instatiatePoint;
                            placeHolder.objectRefrence.SetActive(true);
                            placeHolder.objectRefrence = enemyPool[i].objectRefrence; 
                            placeHolder.type = type;
                            placeHolder.objectRefrence.SetActive(true);
                            placeHolder.inUse = true;
                            placeHolder.index = i;
                            placeHolder.objectRefrence.GetComponent<EntityInfo>().Respawn();
                            enemyPool[i] = placeHolder;
                            freeObject = true;
                            
                        }
                    }
                }
                if (!freeObject)
                {
                    placeHolder = AddObjectToPool(type, instatiatePoint);
                }
                break;  

          


            case EntityType.Soldier:
                if (soldierPoolPopulated)
                {
                    for (int i = 0; i < soldierPool.Count && enemyPool.Count != 0; i++)
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
                            placeHolder.objectRefrence.GetComponent<EntityInfo>().Respawn();
                        }
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


    PooledObject AddObjectToPool(EntityType type, Vector3 instantinatePoint)
    {
        PooledObject newObject;


        switch (type) {

            case EntityType.Bullet:

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
                    projectilePoolPopulated = true;
                }
                newObject.objectRefrence.GetComponent<Bullet>().poolingIndex = newObject.index;
                newObject.objectRefrence.GetComponent<Transform>().name="Bullet"+newObject.index;
                projectilePool.Add(newObject);

                break;

            case EntityType.Ghost:

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
                    enemyPoolPopulated = true;
                }
                enemyPool.Add(newObject);

                newObject.objectRefrence.GetComponent<EntityInfo>().poolingIndex = newObject.index;

                break;

            case EntityType.Slime:

                newObject.type = type;
                newObject.inUse = true;
                newObject.objectRefrence = GameObject.Instantiate(enemyRefrences[1], instantinatePoint, Quaternion.identity);
                if (enemyPoolPopulated)
                {
                    newObject.index = enemyPool.Count;
                }
                else
                {
                    newObject.index = 0;
                    enemyPoolPopulated = true;
                }

                newObject.objectRefrence.GetComponent<EntityInfo>().poolingIndex = newObject.index;
                enemyPool.Add(newObject);
                
                break;

            case EntityType.Rabbit:

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
                    enemyPoolPopulated = true;
                }
                newObject.objectRefrence.GetComponent<EntityInfo>().poolingIndex = newObject.index;
                enemyPool.Add(newObject);

                break;
            case EntityType.Bat:

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
                    enemyPoolPopulated = true;
                }
                newObject.objectRefrence.GetComponent<EntityInfo>().poolingIndex = newObject.index;
                enemyPool.Add(newObject);

                break;

            case EntityType.Oger:

                newObject.type = type;
                newObject.inUse = true;
                newObject.objectRefrence = GameObject.Instantiate(enemyRefrences[4], instantinatePoint, Quaternion.identity);
                if (enemyPoolPopulated)
                {
                    newObject.index = enemyPool.Count;
                }
                else
                {
                    newObject.index = 0;
                    enemyPoolPopulated = true;
                }
                newObject.objectRefrence.GetComponent<EntityInfo>().poolingIndex = newObject.index;
                enemyPool.Add(newObject);

                break;
            case EntityType.Spider:

                newObject.type = type;
                newObject.inUse = true;
                newObject.objectRefrence = GameObject.Instantiate(enemyRefrences[5], instantinatePoint, Quaternion.identity);
                if (enemyPoolPopulated)
                {
                    newObject.index = enemyPool.Count;
                }
                else
                {
                    newObject.index = 0;
                    enemyPoolPopulated = true;
                }
                newObject.objectRefrence.GetComponent<EntityInfo>().poolingIndex = newObject.index;
                enemyPool.Add(newObject);

                break;

            case EntityType.Soldier:
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
                    soldierPoolPopulated = true;
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

    public void DisableObject(int index,EntityType type,GameObject self)
    {
        PooledObject placeHolder;

        switch (type)
        {

            case EntityType.Bullet:
                placeHolder = new PooledObject();
                placeHolder.objectRefrence = self;
                placeHolder.inUse = false;
                placeHolder.type = type;
                placeHolder.objectRefrence.SetActive(false);
                placeHolder.index = index;
                projectilePool[index] = placeHolder;
                
               

                break;

            case EntityType.Ghost:
            case EntityType.Slime:
            case EntityType.Rabbit:
            case EntityType.Bat:
            case EntityType.Oger:
            case EntityType.Spider:

                placeHolder = new PooledObject();
                placeHolder.objectRefrence = self;
                placeHolder.inUse = false;
                placeHolder.type = type;
                placeHolder.objectRefrence.SetActive(false);
                placeHolder.index = index;
                enemyPool[index]=placeHolder;

                break;
       
            case EntityType.Soldier:

                placeHolder = new PooledObject();
                placeHolder.objectRefrence = self;
                placeHolder.inUse = false;
                placeHolder.type = type;
                placeHolder.objectRefrence.SetActive(false);
                placeHolder.index = index;
                soldierPool[index]=placeHolder;
                break;
            default:
                Destroy(self);
                break;
        }


    }

}
