using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlaneState
{
    Grounded,
    Falling
}

public enum PlaneSize
{
    small,
    medium,
    large
}
public class StringEvent : UnityEvent<string>
{

}
public class StateEvent : UnityEvent<PlaneState>{

}

public class AirplaneState : MonoBehaviour {

    public StateEvent stateChangedEvent;

    public StringEvent planeDestroyedEvent;

    public PlaneSize planeSize;
    public PlaneState planeState;

    Coroutine checkFallingRoutine;

    float angleChange = 0;
    float currentAngle = 0;
    float lastAngle = 0;
    Quaternion lastRotation;

    void Awake()
    {
        stateChangedEvent = new StateEvent();
        planeDestroyedEvent = new StringEvent();
    }

    void Update()
    {
        //float currentAngle = 
        currentAngle = Mathf.DeltaAngle(transform.rotation.eulerAngles.x, 0);
        angleChange += lastAngle - currentAngle;
       // Debug.Log("Total rotation change:  " + angleChange + "Delta: " + currentAngle);

        lastAngle = currentAngle;
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.GetComponent<Terrain>())
        {
            EnterState(PlaneState.Grounded);

            if(checkFallingRoutine != null)
            {
                StopCoroutine(checkFallingRoutine);
            }
        }

        if (coll.gameObject.GetComponent<Destructable>())
        {
            if(coll.gameObject.GetComponent<Destructable>().requiredSizeToDestroy > planeSize)
            {
                DestroyPlane("Destructable was too big for your current plane!");
            }
        }

        if (coll.gameObject.GetComponent<Obstacle>())
        {
            DestroyPlane("Hit an obstacle!");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.transform.root.gameObject.GetComponent<Obstacle>())
        {
            DestroyPlane("Hit water!");
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if(coll.gameObject.GetComponent<Terrain>())
        {
            if(checkFallingRoutine != null)
            {
                StopCoroutine(checkFallingRoutine);

            }
            checkFallingRoutine = StartCoroutine(CheckFallingRoutine());
        }
        
    }

    public void DestroyPlane(string cause)
    {
        planeDestroyedEvent.Invoke(cause);
        Destroy(gameObject);

    }

    //If we are still falling after 0.5 seconds we consider ourselves in the air
    IEnumerator CheckFallingRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        RaycastHit hitInfo = new RaycastHit();
       
        Debug.Log("Casting ray!");
        Physics.Raycast(transform.position + new Vector3(0, 10, 0), Vector3.up * -1,  out hitInfo, 12f, 9);
        Debug.DrawRay(transform.position + new Vector3(0, 10, 0), Vector3.up * -12, Color.white, 3);

        if (hitInfo.collider == null) {// hitInfo.collider.gameObject.transform.root.gameObject != gameObject) {

            EnterState(PlaneState.Falling);
        }else
        {
            Debug.Log(hitInfo.collider.gameObject.name);
        }
    }


    void EnterState(PlaneState planeState)
    {

        this.planeState = planeState;


        stateChangedEvent.Invoke(planeState);

    }
}
