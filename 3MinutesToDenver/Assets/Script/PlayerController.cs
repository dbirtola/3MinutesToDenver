﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //player controller has input testing
    public GameObject player;
    // Use this for initialization
    private Vector3 EulerAngleVelocityLeftDirection = new Vector3(-100,0,0);
    private Vector3 EulerAngleVelocityRightDirection = new Vector3(100, 0, 0);

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if ((Input.GetKey("right") || Input.GetKey(KeyCode.D)) && (Input.GetKey("up") || Input.GetKey(KeyCode.W)))
        {
            player.GetComponent<Airplane>().moveForwardAndSteerRight();
        }
        else if ((Input.GetKey("left") || Input.GetKey(KeyCode.A)) && (Input.GetKey("up") || Input.GetKey(KeyCode.W)))
        {
            player.GetComponent<Airplane>().moveForwardAndSteerLeft();
        }
        else if ((Input.GetKey("right") || Input.GetKey(KeyCode.D)) && (Input.GetKey("down") || Input.GetKey(KeyCode.S)))
        {
            player.GetComponent<Airplane>().moveBackwardAndSteerRight();
        }
        else if ((Input.GetKey("left") || Input.GetKey(KeyCode.A)) && (Input.GetKey("down") || Input.GetKey(KeyCode.S)))
        {
            player.GetComponent<Airplane>().moveBackwardAndSteerLeft();
        }


        if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
        {
            player.GetComponent<Airplane>().moveForward();
        }
        else if (Input.GetKey("down") || Input.GetKey(KeyCode.S))
        {
            player.GetComponent<Airplane>().moveBackward();
        }
        else if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
        {
            player.GetComponent<Airplane>().steerLeft();
        }
        else if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
        {
            player.GetComponent<Airplane>().steerRight();
        }

    }
}