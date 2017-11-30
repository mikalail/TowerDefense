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
    public Animator anim;
    EnemyPooling pool;

    bool moving = false;

    // Use this for initialization
    void Start () {

        Health = BaseHealth;

        pool = GameObject.Find("GameManager").GetComponent<EnemyPooling>();

        if(gameObject.transform.CompareTag("Soldier"))
        {
            opponentTag = "Enemy";
        }
        else
        {
            opponentTag = "Soldier";
        }

        objectiveTag = objective.tag;

        anim = gameObject.GetComponent<Animator>();

       

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
        else if(Health - damage<=0)
        {
            Die();
        }
    }
    IEnumerator Flasher()
    {
            anim.SetTrigger("Damage");
            yield return new WaitForSeconds(.5f);
           anim.ResetTrigger("Damage");
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
        anim.SetTrigger("Attack");
        enemey.TakeDamage(Damage);
        yield return new WaitForSeconds(.5f);
        anim.ResetTrigger("Attack");
    }


    public void Respawn()
    {
        Health = BaseHealth;
    }

    public void Move()
    {
        anim.SetBool("Moving", true);
    }
    public void Stop()
    {
        anim.SetBool("Moving", false);
    }

}
