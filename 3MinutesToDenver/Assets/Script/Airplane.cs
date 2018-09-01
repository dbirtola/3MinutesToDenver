using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour {


    public float speed = 15;
    public float reverseSpeed = 12;

    public float turnSpeed = 5;
    public float rotationSpeed = .5f;
    public float acceleration;
    private Vector3 EulerAngleVelocityLeftDirection = new Vector3(0, -100, 0);//needed for rotating left
    private Vector3 EulerAngleVelocityRightDirection = new Vector3(0, 100, 0);//needed for rotating right
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void steerLeft()
    {
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityLeftDirection * Time.deltaTime * rotationSpeed);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
    }
    public void steerRight() {

        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityRightDirection * Time.deltaTime * rotationSpeed);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
    }
    public void moveBackward() {
        // GetComponent<Rigidbody>().velocity = new Vector3(0, 0, transform.forward.z * -GetComponent<Airplane>().speed);
        GetComponent<Rigidbody>().AddForce( -1 * transform.forward * GetComponent<Airplane>().reverseSpeed);

    }
    public void moveForward() {
        GetComponent<Rigidbody>().AddForce(transform.forward * GetComponent<Airplane>().speed);
        // GetComponent<Rigidbody>().velocity = new Vector3(0, 0, transform.forward.z * GetComponent<Airplane>().speed);
    }
    public void moveForwardAndSteerRight()
    {
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityRightDirection * Time.deltaTime * rotationSpeed);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
        GetComponent<Rigidbody>().AddForce(transform.forward * GetComponent<Airplane>().turnSpeed);
    }
    public void moveForwardAndSteerLeft()
    {
       // GetComponent<Rigidbody>().AddForce(transform.forward * GetComponent<Airplane>().turnSpeed/4);
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityLeftDirection * Time.deltaTime * rotationSpeed);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
    }
    public void moveBackwardAndSteerRight()
    {
       // GetComponent<Rigidbody>().AddForce(-1 * transform.forward * GetComponent<Airplane>().turnSpeed/4);
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityRightDirection * Time.deltaTime * rotationSpeed);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
    }
    public void moveBackwardAndSteerLeft()
    {
       // GetComponent<Rigidbody>().AddForce(-1 * transform.forward * GetComponent<Airplane>().turnSpeed/4);
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityLeftDirection * Time.deltaTime * rotationSpeed);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
    }
}
