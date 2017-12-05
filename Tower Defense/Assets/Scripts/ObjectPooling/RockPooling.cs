using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPooling : MonoBehaviour {
    public GameObject rockRefrence;
    public List<GameObject> rockPool;
    public List<bool> rockinUse;
    bool rockPoolPopulated;

	// Use this for initialization
	void Start () {
        rockinUse = new List<bool>();
        rockPool = new List<GameObject>();
        rockPoolPopulated = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject InstantiateObject(Vector3 instatiatePoint, int damage)
    {
        int index;
        GameObject placeHolder;
        if (rockPoolPopulated)
        {
            for (int i = 0; i < rockPool.Count && rockPool.Count != 0; i++)
            {

                if (!rockinUse[i])
                {

                    rockPool[i].SetActive(true);
                    rockPool[i].transform.position = instatiatePoint;
                    placeHolder = rockPool[i];
                    rockinUse[i] = true;
                    return placeHolder;
                }
            }
        }
        placeHolder = AddObjectToPool(instatiatePoint);
        placeHolder.GetComponent<RockProjectile>().SetDamage(damage);
        rockPoolPopulated = true;
        return placeHolder;
    }

   GameObject AddObjectToPool(Vector3 instatiatePoint)
    {
        int index = 0;
        GameObject newRock;

        if (rockPoolPopulated)
        {
            index = rockPool.Count;
        }
        else
        {
            index = 0;
        }
        newRock=GameObject.Instantiate(rockRefrence, instatiatePoint, Quaternion.identity);
        newRock.GetComponent<RockProjectile>().poolingIndex = index;
        newRock.GetComponent<Transform>().name = "Rock" + index;
        rockPool.Add(newRock);
        newRock.GetComponent<RockProjectile>().pool = transform.GetComponent<RockPooling>();
        rockinUse.Add(true);
        return newRock;
    }

    public void DisableObject(int index, GameObject self)
    {
        rockPool[index].SetActive(false);
        rockinUse[index]=false;
    }
}
