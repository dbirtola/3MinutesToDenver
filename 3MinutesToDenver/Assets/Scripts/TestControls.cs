using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControls : MonoBehaviour {

    public float speed = 1;

    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            transform.position = transform.position + Vector3.forward * speed;
        }
    }
}
