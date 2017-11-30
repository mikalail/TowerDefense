using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockProjectile : MonoBehaviour {
    public Transform target;
    public float fireAngle = 45.0f;
    public float gravity = 9.8f;

    Vector3 myPosition;
    float target_Distance;
    float projectile_Velocity;
    float Vx;
    float Vy;
    float flightDuration;
    float elapse_time;

    GameObject 

    // Use this for initialization
    void Start () {
        FlightSetUp();
	}
    void OnEnable(){
        FlightSetUp();
    }
	
	// Update is called once per frame
	void Update () {
        if (elapse_time < flightDuration)
        {
            gameObject.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;
        }
    }

    void FlightSetUp()
    {
        myPosition = transform.position;
        target_Distance = Vector3.Distance(myPosition, target.position);
        projectile_Velocity = target_Distance / (Mathf.Sin(2 * fireAngle * Mathf.Deg2Rad) / gravity);
        Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(fireAngle * Mathf.Deg2Rad);
        Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(fireAngle * Mathf.Deg2Rad);
        flightDuration = target_Distance / Vx;
        elapse_time = 0;
    }

    void OnTriggerEnter()
    {

    }

    

}
