using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    static PlayerController playerController;

    //player controller has input testing
    public GameObject player;
    // Use this for initialization
    private float initialMass;

    void Awake()
    {
        if(playerController != null)
        {
            Destroy(gameObject);
            return;
        }else
        {
            playerController = this;
        }
    }
           // if (GetComponent<AirplaneState>().planeState == PlaneState.Grounded)

	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {


            if ((Input.GetKey("right") || Input.GetKey(KeyCode.D)) && (Input.GetKey("up") || Input.GetKey(KeyCode.W)))
            {
                player.GetComponent<Airplane>().moveForwardAndSteerRight();
            }
            else if ((Input.GetKey("left") || Input.GetKey(KeyCode.A)) && (Input.GetKey("up") || Input.GetKey(KeyCode.W)))
            {
                player.GetComponent<Airplane>().moveForwardAndSteerLeft();
            }
            else if (((Input.GetKey("right") || Input.GetKey(KeyCode.D)) && (Input.GetKey("down") || Input.GetKey(KeyCode.S))))
            {
                player.GetComponent<Airplane>().rotateBackward();
            }
            else if ((Input.GetKey("left") || Input.GetKey(KeyCode.A)) && (Input.GetKey("down") || Input.GetKey(KeyCode.S)))
            {
                player.GetComponent<Airplane>().rotateBackward();
            }
            else if ((Input.GetKey("up") || Input.GetKey(KeyCode.W) && (Input.GetKey("down") || Input.GetKey(KeyCode.S))))
            {
                player.GetComponent<Airplane>().rotateBackward();
            }


            if ((Input.GetKey("up") || Input.GetKey(KeyCode.W)))
            {
                player.GetComponent<Airplane>().moveForward();
            }
            else if (Input.GetKey("down") || Input.GetKey(KeyCode.S))
            {
                player.GetComponent<Airplane>().rotateBackward();
            }
            else if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
            {
                player.GetComponent<Airplane>().steerLeft();
            }
            else if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
            {
                player.GetComponent<Airplane>().steerRight();
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                player.GetComponent<Airplane>().rotateLeft();
            }
            else if (Input.GetKey(KeyCode.E))
            {
                player.GetComponent<Airplane>().rotateRight();
            }

        }

    }


}
