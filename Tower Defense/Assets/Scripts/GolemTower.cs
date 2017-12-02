using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemTower : MonoBehaviour {

   
    public Animator anim;// tower animator
    public GameObject rockPrefab; // The Boulder
    public Transform hand;// refrence to the golems hand.
    public List<GameObject> enemylist;// list of enemies in the area;
    public float shotsPerSecond=.5f;//shots per second
    public float fireDelay= 1f;// fire rate
    public float range = 15f;// radius range
    public RockPooling pool;
    public int damage=10;// Ammount of damage dealt;
    public float minimumDistance=20; 


    public GameObject target;

    public bool towerActive = false;
    private bool targetsInRange=false;

    void Start()
    {
        gameObject.GetComponent<SphereCollider>().radius = range;
        pool = GameObject.Find("GameManager").GetComponent<RockPooling>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(towerActive&&target!=null)
        {
            gameObject.transform.LookAt(new Vector3(target.transform.position.x, gameObject.transform.position.y, target.transform.position.z));
        }

    }

    IEnumerator TowerActive()
    {
        towerActive = true;
        UpdateTarget();// gets first target when tower is active

        while(enemylist.Count>0)
        {
            
            if (target!=null)
            {
                anim.SetBool("Fire", true);
                yield return new WaitForSeconds(1.19f);
                Fire(target);
            }
            
            anim.SetBool("Fire", false);
            yield return new WaitForSeconds(fireDelay);
            UpdateTarget();
        }
        anim.SetBool("Fire", false);
        Debug.Log("NO ENEMIES PRESENT");
        towerActive = false;
        yield return null;
    }


    void OnTriggerEnter(Collider co)
    {
        // Was it a Monster? Then Shoot it

        if (co.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Entered Radius");
            try
            {
                Debug.Log("Enemy Entered Radius");
                enemylist.Add(co.gameObject);
                targetsInRange = true;
                if(!towerActive)
                {
                    StartCoroutine(TowerActive());
                }
            }
            catch { Debug.Log("failed to Add To List"); }      
 
        }
    }

    void UpdateTarget()
    {bool targetobtained = false;
      while(!targetobtained)
        {
            try
            {   
                if(target==null)
                {
                    target = enemylist[0];
                    targetobtained = true;
                }
                else if (!target.activeSelf && enemylist.Count>1)//Enemy dead move to the next in target list
                {
                    enemylist.RemoveAt(0);
                    target = enemylist[0];
                    if(Vector3.Distance(transform.position, target.transform.position)>=minimumDistance)
                    {
                        if (target.activeSelf)
                        {
                           targetobtained = true;
                        }

                    }

                 }
                else if(!target.activeSelf && enemylist.Count==1)
                {
                    enemylist.RemoveAt(0);
                    target = null;
                    targetobtained = true;
                }
                else 
                {
                    target = enemylist[0];
                    targetobtained = true;
                }

            }

            catch { Debug.Log("failed to Update"); }
            }

        
    }

    //Causes The Golem to throw a rock
    void Fire(GameObject target)
    {
        GameObject placeHolder;

       
        try
        {
            placeHolder = pool.InstantiateObject( hand.position,damage);
            placeHolder.GetComponent<RockProjectile>().firepoint=hand;
            placeHolder.GetComponent<RockProjectile>().SetTarget(target.transform);
            
        }
        catch { Debug.Log("failed to Instantiate"); }

        

    }

    // On Trigger Exit The evemy removed from the queue;
    void OnTriggerExit(Collider co)
    {

        if (co.CompareTag("Enemy"))
        {
            enemylist.RemoveAt(0);
        }
    }

    void OnDrawGizmo()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, minimumDistance);
    }

}
