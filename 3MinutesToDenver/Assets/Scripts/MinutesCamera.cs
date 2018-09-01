using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinutesCamera : MonoBehaviour {

    public GameObject target;
    public int distance;
	
	// Update is called once per frame
	void Update () {
        if(target != null)
        {
            transform.position = target.transform.position + Vector3.up * distance;
        }
    }
}
