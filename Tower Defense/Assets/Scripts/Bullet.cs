using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

// Speed
    public float speed = 10;
    public int damage = 10;
    public GameObject floor;
    public Vector3 floorHeight;
    public int poolingIndex;
    ObjectPooling pool;

    // Target (set by Tower)
    public Transform target=null;
    
    void Start()
    {
        GameObject floor= GameObject.Find("Floor");
        pool = FindObjectOfType<ObjectPooling>();

    }

    void Update()
    {
        if (target != null)
        {
            // Fly towards the target           
            Vector3 dir = target.position - transform.position;
            GetComponent<Rigidbody>().velocity = dir.normalized * speed;
        }


    }
    void OnTriggerEnter(Collider co)
    {

        if (co.CompareTag("Enemy"))
        {
            co.GetComponent<EntityInfo>().TakeDamage(damage);
            BulletDestroy();
        }

        if(co.CompareTag("Test"))
        {
            Debug.Log("HIT!!!");
            BulletDestroy();
        }
    }

    void BulletDestroy()
    {
        pool.DisableObject(poolingIndex, EntityType.Bullet, gameObject);
    }
    


}
