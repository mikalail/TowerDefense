using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveInfo : MonoBehaviour {
    public int Health = 100;
    
	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		

	}

    public void TakeDamage(int damage)
    {
        if (Health - damage > 0)
        {
            Health -= damage;
        }
        else
        {
            Die();
        }
    }


    void Die()
    {
        Destroy(gameObject);
    }
}
