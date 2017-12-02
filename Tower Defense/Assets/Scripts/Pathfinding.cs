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

    public Path myPath;// Refrence to path
    List<GameObject> destinationPoints;// list of destinoation Points transform

    public bool atLocation = false; // Whether or not the agent is at the desired location
    public Vector3 destinationPoint;// The current destination point

    bool isMoving = true;

    NavMeshAgent navMeshAgent;// refrence to the NaveMeshAgent attached to the parent gameobject.

    int locationindex = 0; // Index of the current next point in the Path.

    //public Animator anim;// Refrence to the Animator connected to the parent game object.


    void Start()
    {

        navMeshAgent = transform.GetComponent<NavMeshAgent>(); // sets the reference to the navMeshAgent

        destinationPoints = myPath.Nodes; // Sets reference to the list of destination points

        destinationPoint = destinationPoints[0].transform.position;// sets the first destination point of the point

        StartCoroutine(CheckPosition());// starts the "CheckPosition Coroutine"
    }

    // Update is called once per frame
    void Update()
    {



    }

    // A Void Function that chooses the next point in the path based on the current point.
    void ChoosePoint()
    {


        if (locationindex < destinationPoints.Count)
        {                                   //If the index is less than the max 
            destinationPoint = (destinationPoints[locationindex].transform.position);// the new destination point is set to the next in the list
            locationindex++;                                                         // the index is the incremented to reflect the path progression.
        }
        else
        {                                                                       // If the index is last in the list then the path is reset 
            locationindex = 0;
            destinationPoint = (destinationPoints[locationindex].transform.position); // the next destination point is then set to the first point in the list.
        }

    }

    // Coroutine used to check the current position of the NavMeshAgent. 

    IEnumerator CheckPosition()
    {

        Debug.Log("Started");
        while (true)
        {

            if (this.transform.position.x == destinationPoint.x && this.transform.position.z == destinationPoint.z)
            {

                ChoosePoint();// If at the destination point the next point is chosen
               // Animation(true);// The animation assosiated with reaching the location is played
              

            }

            else
            {
                navMeshAgent.destination = destinationPoint;// the NavMeshAgent's destination is set
               // Animation(false);// The animation with reaching the location is stopped
                yield return null;
            }

        }
    }

    void Animation(bool idle)
    {
   //     anim.SetBool("Idle", idle);//plays the idle animation if idle is true. Stops the idle animation if idle is false.
    }


}
