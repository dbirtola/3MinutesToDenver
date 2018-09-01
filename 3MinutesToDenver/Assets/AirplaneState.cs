using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlaneState
{
    Grounded,
    Falling
}

public class AirplaneState : MonoBehaviour {

    public UnityEvent leftGroundEvent;

    public PlaneState planeState;


    void Awake()
    {
        leftGroundEvent = new UnityEvent();
    }

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
        if (planeState == PlaneState.Falling)
        {
            leftGroundEvent.Invoke();
        }

    }
}
