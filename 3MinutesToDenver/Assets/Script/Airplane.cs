using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour {


    public float speed = 15;
    // public float reverseSpeed = 12;
   // public float tempDrag = 3;
   // public float turnSpeed = 5;
    public float rotationSpeed = .5f;
    //public float acceleration;
    private Vector3 EulerAngleVelocityLeftDirection = new Vector3(0, -100, 0);//needed for rotating left
    private Vector3 EulerAngleVelocityRightDirection = new Vector3(0, 100, 0);//needed for rotating right
    private Vector3 EulerAngleVelocityRotateBackward = new Vector3(-150, 0, 0);//needed for rotating back
    private Vector3 EulerAngleVelocityRotateForward = new Vector3(100, 0, 0);//needed for rotating forward
    private Vector3 EulerAngleVelocityBarrelLeft = new Vector3(0, 0, -200);//needed for barrel roll to the left
    private Vector3 EulerAngleVelocityBarrelRight = new Vector3(0, 0, 200);//needed for barrel roll to the right

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void steerLeft()
    {
        if (GetComponent<AirplaneState>().planeState == PlaneState.Grounded)
        {
            Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityLeftDirection * Time.deltaTime * rotationSpeed);
            GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
        }


    }

    public void rotateLeft()
    {

            Quaternion deltaRotation1 = Quaternion.Euler(EulerAngleVelocityBarrelRight * Time.deltaTime * rotationSpeed);
            GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation1);
    }

    public void rotateRight()
    {

            Quaternion deltaRotation2 = Quaternion.Euler(EulerAngleVelocityBarrelLeft * Time.deltaTime * rotationSpeed);
            GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation2);
    }
    public void rotateLeftAndForward() {

        Quaternion deltaRotation1 = Quaternion.Euler(EulerAngleVelocityBarrelRight * Time.deltaTime * rotationSpeed);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation1);
        GetComponent<Rigidbody>().AddForce(transform.forward.x * GetComponent<Airplane>().speed, transform.forward.y * GetComponent<Airplane>().speed, transform.forward.z * GetComponent<Airplane>().speed);

    }
    public void rotateRightAndForward()
    {

        Quaternion deltaRotation2 = Quaternion.Euler(EulerAngleVelocityBarrelLeft * Time.deltaTime * rotationSpeed);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation2);
        GetComponent<Rigidbody>().AddForce(transform.forward.x * GetComponent<Airplane>().speed, transform.forward.y * GetComponent<Airplane>().speed, transform.forward.z * GetComponent<Airplane>().speed);

    }

    public void steerRight() {

        if (GetComponent<AirplaneState>().planeState == PlaneState.Grounded)
        {
            Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityRightDirection * Time.deltaTime * rotationSpeed);
            GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
        }

    }
 
    public void rotateBackward()
    {

            Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityRotateBackward * Time.deltaTime * rotationSpeed);
            GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
            GetComponent<Rigidbody>().AddForce(0,100,0);

    }

    public void moveForward()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward.x * GetComponent<Airplane>().speed, transform.forward.y * GetComponent<Airplane>().speed, transform.forward.z * GetComponent<Airplane>().speed);

    }

    public void moveForwardAndSteerRight()
    {
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityRightDirection * Time.deltaTime * rotationSpeed);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
        GetComponent<Rigidbody>().AddForce(deltaRotation*transform.right * GetComponent<Airplane>().speed);

    }
    public void moveForwardAndSteerLeft()
    {
        Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocityLeftDirection * Time.deltaTime * rotationSpeed);
        GetComponent<Rigidbody>().MoveRotation(GetComponent<Rigidbody>().rotation * deltaRotation);
        GetComponent<Rigidbody>().AddForce(deltaRotation * transform.right*-1 * GetComponent<Airplane>().speed);
    }

}
