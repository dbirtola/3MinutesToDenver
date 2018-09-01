using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlaneState
{
    Grounded,
    Falling
}

public class AirplaneState : MonoBehaviour {


    public PlaneState planeState;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.GetComponent<Terrain>())
        {
            EnterState(PlaneState.Grounded);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if(coll.GetComponent<CliffZone>())
        {
            EnterState(PlaneState.Falling);
        }
        
    }

    void EnterState(PlaneState planeState)
    {
        this.planeState = planeState;
    }
}
