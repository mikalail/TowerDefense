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
    public BulletPooling pool;

    // Target (set by Tower)
    public Transform target=null;
    
    void Start()
    {
        GameObject floor= GameObject.Find("Floor");
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
            Debug.Log("Enemy Hit");
            co.GetComponent<EntityInfo>().TakeDamage(damage);
            BulletDestroy();
        }
    }

    public void BulletDestroy()
    {
        pool.DisableObject(poolingIndex,  gameObject);
    }
    


}
