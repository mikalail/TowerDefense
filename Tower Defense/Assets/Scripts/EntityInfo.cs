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
    public AudioSource audio;
    public List<AudioClip> clips;
    public bool dead;
    public GameObject entityGuiText;
    public EnemyGuiManager egm;

    Pathfinding pathfinder;
    

    bool moving = false;

    // Use this for initialization
    void Awake () {



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
        audio = GetComponent<AudioSource>();
        pathfinder = GetComponent<Pathfinding>();
        
       

    }

    void Start()
    {
        egm = entityGuiText.GetComponent<EnemyGuiManager>();
        egm.SetHealth(BaseHealth);

    }



    // Update is called once per frame    

    void Update () {
        
    }

    public void TakeDamage(int damage)
    {

        egm.Damage(damage);

        if(Health-damage>0)
        {
            Health -= damage;
            StartCoroutine(Flasher()); //VERY IMPORTANT!  You 'must' start coroutines with this code.
            audio.PlayOneShot(clips[1]);

        }
        else if(Health - damage<=0)
        {
            StartCoroutine(Die());
        }
    }
    IEnumerator Flasher()
    {
            anim.SetTrigger("Damage");
            StartCoroutine(pathfinder.Stop());
            yield return new WaitForSeconds(.5f);
            StartCoroutine(pathfinder.Move());
            anim.ResetTrigger("Damage");
    }

    IEnumerator Die()
    {
        StartCoroutine(pathfinder.Stop());
        audio.PlayOneShot(clips[0]);
        anim.SetBool("Die", true);
        dead = true;

        yield return new WaitForSeconds(2.5f);
        
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
        dead = false;
        anim.SetBool("Die", false);
        StartCoroutine(pathfinder.Move());
        Health = BaseHealth;
        egm.SetHealth(BaseHealth);
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
