/* How to make this work:
 *  =======================
 *  
 *  what you need:
 *  (1) A NavMeshAgent attached to the current game object.
 *  (2) A NavMesh Must be present and baked in the Scene.
 *  (2) A Path GameObject with the "Path" script added to it. The Path must have nodes added to it. See (NodeCreator).
 * 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{

    
    public Transform MyObjective;// Refrence to path
    public bool atLocation = false; // Whether or not the agent is at the desired location
    public Vector3 destinationPoint;// The current destination point
    bool isMoving = true;

    bool battleActive = false;// Whether or not the agent is in combat
    public GameObject target;// Combat Target;

    NavMeshAgent navMeshAgent;// refrence to the NaveMeshAgent attached to the parent gameobject.
    int locationindex = 0; // Index of the current next point in the Path.
    List<GameObject> enemylist;// List Of enemies currently in combat with entity;
    EntityInfo myInfo;// entity info

    //public Animator anim;// Refrence to the Animator connected to the parent game object.


    void Start()
    {
        Respawn();// Sets all variables required on spawn;
    }

    void Respawn()
    {   myInfo = GetComponent<EntityInfo>();// entity info;

        if (gameObject.transform.CompareTag("Soldier"))
        {
            MyObjective = GameObject.Find("SoldierObjective").GetComponent<Transform>();
        }
        else
        {
            MyObjective = GameObject.Find("EnemyObjective").GetComponent<Transform>();
        }

        navMeshAgent = transform.GetComponent<NavMeshAgent>(); // sets the reference to the navMeshAgent

      // Sets reference to the list of destination points

        destinationPoint = MyObjective.position;// sets the first destination point of the point

        StartCoroutine(Move());// starts the "CheckPosition Coroutine"

        
    }

    
    void UpdateTarget()
    {
        try
        {
            if (!target.activeSelf && enemylist.Count > 1)//Enemy dead move to the next in target list
            {
                enemylist.RemoveAt(0);
                target = enemylist[0];
            }
            else if (!target.activeSelf && enemylist.Count == 1)
            {
                enemylist.RemoveAt(0);
                target = null;
            }
            else
            {
                target = enemylist[0];
            }

        }

        catch { Debug.Log("failed to Update"); }

    }



    // Coroutine used to check the current position of the NavMeshAgent. 

    IEnumerator Move()
    {

        myInfo.Move();

        navMeshAgent.updatePosition =true;
        navMeshAgent.updateRotation =true;
        navMeshAgent.destination = destinationPoint;// the NavMeshAgent's destination is set
                                                            // Animation(false);// The animation with reaching the location is stopped
        yield return null;

    }
    IEnumerator Battle()
    {
        myInfo.Stop();

        while (enemylist.Count > 0)
        {

            if (target != null)
            {
                navMeshAgent.destination = target.transform.position;
                myInfo.Attack(target.GetComponent<EntityInfo>());
            }
            UpdateTarget();
        }
        StartCoroutine(Move());
        yield return null;
    }

    

    void OnTriggerEnter(Collider other)
    {
        /*
        if (other.CompareTag(myInfo.objectiveTag))
        {
            myInfo.ReachObjective();
        }
        else //if(other.CompareTag(myInfo.opponentTag))
        {
            enemylist.Add(other.gameObject);
            if(!battleActive)
            {
                StartCoroutine(Battle());
            }
        }*/
    }

}
