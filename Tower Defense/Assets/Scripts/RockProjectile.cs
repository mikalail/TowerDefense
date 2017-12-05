using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockProjectile : MonoBehaviour {
    public Transform target; // target location
    public Transform firepoint;// fire point 
    public float fireAngle = 90.0f;// angle of fire
    public float gravity = 9.8f;// force of gravity
    public float blastTime =4.0f;// amout of blastTime
    public RockPooling pool; // refrence to the rock Pooling Script
    public int poolingIndex=0;// pooling index value
    public AudioSource audio;
    public List<AudioClip> clips;


    Vector3 myPosition;// current rock positions
    Vector3 targetPosition;// current target position
    float target_Distance;// Distance from target
    float projectile_Velocity;// velocity of rock
    float Vx;// velocity x
    float Vy;// velocity y
    float flightDuration;
    float elapse_time;
    public GameObject blastZone;// Blast Radius 
    bool inFlight = false;
    bool followHand = false;// whether or not the rock should follow the golems hand.


    // Use this for initialization
    void Awake () {
        blastZone.SetActive(false);
        audio = GetComponent<AudioSource>();
    }
	

    IEnumerator HitTarget()
    {
        blastZone.SetActive(true);
        audio.PlayOneShot(clips[Mathf.RoundToInt(Random.Range(0, clips.Count))]);
        yield return new WaitForSeconds(4);
        pool.DisableObject(poolingIndex,gameObject);
        
        yield return null;
    }
    
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        blastZone.SetActive(false);
        StartCoroutine(SimulateProjectile());      
    }
    



    IEnumerator SimulateProjectile()
    {
        inFlight = true;
        targetPosition = target.position;
        targetPosition.y = 0;
        // Move projectile to the position of throwing object + add some offset if needed.
        transform.position = firepoint.position;

        // Calculate distance to target
        float target_Distance = Vector3.Distance(transform.position, targetPosition);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * fireAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(fireAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(fireAngle * Mathf.Deg2Rad);

        

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

         //Rotate projectile to face the target.
        transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
           
            elapse_time += Time.deltaTime;
            yield return null;
        }
        inFlight = false;
       
    }

    public void SetDamage(int damage)
    {
        blastZone.GetComponent<DamageArea>().SetDamage(damage);
    }


    void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Terrain"))
        {
            StartCoroutine(HitTarget());
        }
        
    }

    

}
