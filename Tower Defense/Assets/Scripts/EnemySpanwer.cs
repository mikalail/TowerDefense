using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnerType { Soldier, Enemy, NULL};


public class EnemySpanwer : MonoBehaviour {

    public float spawnTimer;
    private int[] spawnList;// Array containning the abount of enemies to Spawn 
    private SpawnerType myType = SpawnerType.Enemy;
    private ObjectPooling pool;
    private Transform spawnLocation;

    // Use this for initialization
    private void Start()
    {
        if(myType==SpawnerType.Enemy)
        {
            spawnList = new int[6];
        }
        pool = GameObject.Find("GameManager").GetComponent<ObjectPooling>();
        spawnLocation = gameObject.transform;
        NextList(2, 0, 0, 0, 0, 0);
        StartCoroutine(Spawn());
    }
    // Update is called once per frame
    void Update () {
        
        
	}

    void NextList(int ghosts,int slime, int rabbits, int bats, int oger,int spider)
    {

        spawnList[0] = ghosts;
        spawnList[1] = slime;
        spawnList[2] = rabbits;
        spawnList[3] = bats;
        spawnList[4] = oger;
        spawnList[5] = spider;
    }

    public IEnumerator Spawn()
    {
        for(int i=0; i<6; i++)
        {
            for(int j = 0; j < spawnList[i] ; j++)
            {
                switch(i)
                {
                    case 0:
                        pool.InstantiateObject(EntityType.Ghost, spawnLocation.position);
                        break;

                    case 1:
                        pool.InstantiateObject(EntityType.Slime, spawnLocation.position);
                        break;

                    case 2:
                        pool.InstantiateObject(EntityType.Rabbit, spawnLocation.position);
                        break;

                    case 3:
                        pool.InstantiateObject(EntityType.Bat, spawnLocation.position);
                        break;

                    case 4:
                        pool.InstantiateObject(EntityType.Oger, spawnLocation.position);
                        break;

                    case 5:
                        pool.InstantiateObject(EntityType.Spider, spawnLocation.position);
                        break;
                }
                yield return new WaitForSeconds(spawnTimer);
            }
        }
    }


}
