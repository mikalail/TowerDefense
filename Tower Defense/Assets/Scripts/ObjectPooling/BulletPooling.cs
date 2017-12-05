using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour {
    public GameObject bulletRefrence;
    public List<GameObject> bulletPool;
    public List<bool> bulletinUse;
    bool bulletPoolPopulated;

	// Use this for initialization
	void Start () {
        bulletinUse = new List<bool>();
        bulletPool = new List<GameObject>();
        bulletPoolPopulated = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject InstantiateObject( Vector3 instatiatePoint)
    {
        int index;
        GameObject placeHolder;
        if (bulletPoolPopulated)
        {
            for (int i = 0; i < bulletPool.Count && bulletPool.Count != 0; i++)
            {

                if (!bulletinUse[i])
                {

                    bulletPool[i].SetActive(true);
                    bulletPool[i].transform.position = instatiatePoint;
                    placeHolder = bulletPool[i];
                    bulletinUse[i] = true;
                    return placeHolder;
                }
            }
        }
        placeHolder = AddObjectToPool(instatiatePoint);
        bulletPoolPopulated = true;
        
        return placeHolder;
    }

   GameObject AddObjectToPool(Vector3 instatiatePoint)
    {
        int index = 0;
        GameObject newBullet;

        if (bulletPoolPopulated)
        {
            index = bulletPool.Count;
        }
        else
        {
            index = 0;
        }
        newBullet=GameObject.Instantiate(bulletRefrence, instatiatePoint, Quaternion.identity);
        newBullet.GetComponent<Bullet>().poolingIndex = index;
        newBullet.GetComponent<Transform>().name = "Bullet" + index;
        bulletPool.Add(newBullet);
        bulletinUse.Add(true);
        return newBullet;
    }

    public void DisableObject(int index, GameObject self)
    {
        bulletPool[index].SetActive(false);
        bulletinUse[index]=false;
    }
}
