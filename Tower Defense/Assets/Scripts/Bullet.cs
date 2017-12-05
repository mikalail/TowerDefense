using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

// Speed
    public float speed = 10;
    public int damage = 10;
    public Vector3 floorHeight;
    public int poolingIndex;
    public BulletPooling pool;

    // Target (set by Tower)
    public Transform target=null;
    
    void Start()
    {
        pool = FindObjectOfType<BulletPooling>();

    }

    void Update()
    {
        if (target != null)
        {
            // Fly towards the target           
            Vector3 dir = target.position - transform.position;
            GetComponent<Rigidbody>().velocity = dir.normalized * speed;
        }
        if(target==null)
        {
            BulletDestroy();
        }


    }
    void OnTriggerEnter(Collider co)
    {
     
        if (co.CompareTag("Enemy"))
        {
            
            co.GetComponent<EntityInfo>().TakeDamage(damage);
            BulletDestroy();
        }
        if (co.CompareTag("Terrain"))
        {
            BulletDestroy();
        }
    }

    public void BulletDestroy()
    {
        pool.DisableObject(poolingIndex,  gameObject);
    }
    


}
