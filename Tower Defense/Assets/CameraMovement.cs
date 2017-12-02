using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public int speed = 10;
    Transform placeHolder;


	// Use this for initialization
	void Start () {


        placeHolder = transform;
        placeHolder.rotation=new Quaternion(0, 0, 0, 0);
		
	}
    
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.D))
        {
            placeHolder.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            transform.position = placeHolder.position;
        }
        if (Input.GetKey(KeyCode.A))
        {
            placeHolder.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            transform.position = placeHolder.position;
        }
        if (Input.GetKey(KeyCode.S))
        {
            placeHolder.Translate(new Vector3(0,0,-speed * Time.deltaTime));
            transform.position = placeHolder.position;
        }
        if (Input.GetKey(KeyCode.W))
        {
            placeHolder.Translate(new Vector3(0,0,speed * Time.deltaTime));
            transform.position = placeHolder.position;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, -speed * Time.deltaTime,0));
        }
        if (Input.GetKey(KeyCode.E))
        { 
            transform.Rotate(new Vector3(0,speed * Time.deltaTime,0));
        }

    }

}
