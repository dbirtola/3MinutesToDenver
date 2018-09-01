using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour {


    public float speed = 10;
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
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityLeftDirection * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
    }
    public void steerRight() {

        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityRightDirection * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
    }
    public void moveBackward() {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, transform.forward.z * -GetComponent<Airplane>().speed);

    }
    public void moveForward() {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, transform.forward.z * GetComponent<Airplane>().speed);
    }
    public void moveForwardAndSteerRight()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, transform.forward.z * GetComponent<Airplane>().speed);
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityRightDirection * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
    }
    public void moveForwardAndSteerLeft()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, transform.forward.z * GetComponent<Airplane>().speed);
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityLeftDirection * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
    }
    public void moveBackwardAndSteerRight()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, transform.forward.z * -GetComponent<Airplane>().speed);
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityRightDirection * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
    }
    public void moveBackwardAndSteerLeft()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, transform.forward.z * -GetComponent<Airplane>().speed);
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityLeftDirection * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
    }
}
