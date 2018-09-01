using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinutesCamera : MonoBehaviour {

    public GameObject target;
    public int distance;

    static MinutesCamera minutesCamera;

    void Awake()
    {
        if(minutesCamera != null)
        {
            Destroy(gameObject);
            return;
        }else
        {
            minutesCamera = this;
        }

    }
	// Update is called once per frame
	void Update () {
        if(target != null)
        {
            transform.position = target.transform.position + Vector3.up * distance - Vector3.forward * 40;
        }
    }
}
