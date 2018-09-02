using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour {


    public float speedBoostAcceleration = 2000;
    private float initialSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Airplane>())
            initialSpeed = other.GetComponent<Airplane>().speed;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Airplane>())
        {
            //Debug.Log("Detected plane");
            other.GetComponent<Airplane>().speed += speedBoostAcceleration;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Airplane>())
        {
            other.GetComponent<Airplane>().speed = initialSpeed;

        }
    }

}
