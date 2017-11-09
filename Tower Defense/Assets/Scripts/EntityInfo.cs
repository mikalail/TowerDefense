using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type {Soldier,Enemy1,Enemy2,Enemy3,Bullet,NULL};

public class EntityInfo : MonoBehaviour {


    public Type entityType;
    public int Health=100;
    public float AttackSpeed=1.00f;
    public int Damage=10;
    public int Speed=1;
    public string opponentTag=null;
    public string objectiveTag=null;
    public GameObject objective=null;
    public int poolingIndex;
    ObjectPooling pool;

    bool moving = false;
	// Use this for initialization
	void Start () {

        pool = FindObjectOfType<ObjectPooling>();

        if(entityType==Type.Soldier)
        {
            opponentTag = "Enemy";
        }
        else
        {
            opponentTag = "Soldier";
        }

        objectiveTag = objective.tag;

        Debug.Log("Objective:" + objectiveTag);

    }

    // Update is called once per frame    

	void Update () {
        
    }

    public void TakeDamage(int damage)
    {
        if(Health-damage>0)
        {
            Health -= damage;
            StartCoroutine(Flasher()); //VERY IMPORTANT!  You 'must' start coroutines with this code.

        }
        else
        {
            Die();
        }
    }
    IEnumerator Flasher()
    {
        Color normal = gameObject.GetComponent<Renderer>().material.color;

            gameObject.GetComponent<Renderer>().material.color=Color.red;
            yield return new WaitForSeconds(.5f);
            gameObject.GetComponent<Renderer>().material.color=normal;


    }

    void Die()
    {
        pool.DisableObject(poolingIndex,entityType,gameObject);
    }

    public void ReachObjective()
    {
        objective.GetComponent<ObjectiveInfo>().TakeDamage(Damage);
        Die();
    }


    public IEnumerator Attack(EntityInfo enemey)
    {
        enemey.TakeDamage(Damage);
        yield return new WaitForSeconds(.5f);
    }

    
}
