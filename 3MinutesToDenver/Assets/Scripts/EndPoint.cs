using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndPoint : MonoBehaviour {

	public UnityEvent playerEnteredZoneEvent { get; private set; }
    

    public void Awake()
    {
        playerEnteredZoneEvent = new UnityEvent();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<TestControls>())
        {
            Debug.Log("Triggered");
            playerEnteredZoneEvent.Invoke();
        }
    }
}
