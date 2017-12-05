using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour {

    public int damageValue=100;
	// Use this for initialization
	void Start () {
		
	}

    public void SetDamage(int damage)
    {
        damageValue=damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.transform.GetComponent<EntityInfo>().TakeDamage(damageValue);
        }
    }

}
