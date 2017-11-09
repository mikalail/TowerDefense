﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    // The Bullet
    public Animator anim;
    public GameObject bulletPrefab;
    public Transform barrel;
    public List<GameObject> enemylist;
    public float shotsPerSecond=.5f;
    public float fireRate= 0f;
    public float range = 15f;
    ObjectPooling pool;

    // For Test Only

    public GameObject testpoint;


    //

    public GameObject target;

    public bool towerActive = false;
    private bool targetsInRange=false;

    void Start()
    {
        fireRate = 1 / shotsPerSecond;
        gameObject.GetComponent<SphereCollider>().radius = range;
        pool = GameObject.Find("GameManager").GetComponent<ObjectPooling>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetsInRange)
        {
            
        }

        if(Input.anyKey)
        {
            Fire(testpoint);
        }

    }

    IEnumerator TowerActive()
    {
        towerActive = true;

        while(enemylist.Count>0)
        {
            UpdateTarget();
            if(target!=null)
            {

            }
            Fire(target);
            yield return new WaitForSeconds(fireRate);
            UpdateTarget();
        }
        Debug.Log("NO ENEMIES PRESENT");
        towerActive = false;
        yield return null;
    }


    void OnTriggerEnter(Collider co)
    {
        // Was it a Monster? Then Shoot it
        //creep1Health health = co.GetComponentInChildren<creep1Health>();

        if (co.CompareTag("Enemy"))
        {
            
            try
            {
                Debug.Log("Enemy Entered Radius");
                enemylist.Add(co.gameObject);
                targetsInRange = true;
                if(!towerActive)
                {
                    StartCoroutine(TowerActive());
                }
            }
            catch { Debug.Log("failed to Add To List"); }
            
            
        }
    }

    void UpdateTarget()
    {
        try
        {   if (target == null && enemylist[0]==null)
            {
                enemylist.RemoveAt(0);
                Debug.Log("NULL");
            }
            else
            {
                target = enemylist[0];
            }

        }

        catch { Debug.Log("failed to Update"); }
        
    }

    void Fire(GameObject target)
    {
        PooledObject placeHolder;

        gameObject.transform.LookAt(new Vector3(target.transform.position.x, gameObject.transform.position.y, target.transform.position.z));
        try
        {
            Debug.Log("Attempt");
            placeHolder = pool.InstantiateObject(Type.Bullet, barrel.position);
            Debug.Log("Bullet Active");
            placeHolder.objectRefrence.GetComponent<Bullet>().target = target.transform;
        }
        catch { Debug.Log("failed to Instantiate"); }

        

    }

    void OnTriggerExit(Collider co)
    {

        if (co.CompareTag("Enemy"))
        {
            enemylist.RemoveAt(0);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,gameObject.GetComponent<SphereCollider>().radius);
    }
}