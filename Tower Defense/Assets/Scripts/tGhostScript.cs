//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using System.Collections;


public class tGhostScript : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
        GameObject enemyObjective = GameObject.Find("EnemyObjective");
        if (enemyObjective)
        {
            GetComponent<NavMeshAgent>().destination = enemyObjective.transform.position;
        }
    }
    void OnTriggerEnter(Collider co)
    {
        // If castle then deal Damage
        if (co.gameObject.tag == "EnemyObjective")
        {
            //co.GetComponentInChildren<townHealth>().decrease();
            Destroy(this.gameObject);
            //gameObject.Destroy;
        }

    }


}
