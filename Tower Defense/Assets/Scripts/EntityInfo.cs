using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType {Soldier,Ghost,Slime,Rabbit,Bat,Oger,Spider,Bullet,NULL};

public class EntityInfo : MonoBehaviour {


    public EntityType entityType;
    public int Health=100;
    public int BaseHealth=100;
    public float AttackSpeed=1.00f;
    public int Damage=10;
    public int Speed=1;
    public string opponentTag=null;
    public string objectiveTag="None";
    public GameObject objective=null;
    public int poolingIndex;
    EnemyPooling pool;

    bool moving = false;

    // Use this for initialization
    void Start () {

        Health = BaseHealth;

        pool = GameObject.Find("GameManager").GetComponent<EnemyPooling>();

        if(entityType==EntityType.Soldier)
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
        pool.DisableObject(poolingIndex,gameObject);
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

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            if (gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<Bullet>().BulletDestroy();
            }
        }
    }

    public void Respawn()
    {
        Health = BaseHealth;
    }
}
