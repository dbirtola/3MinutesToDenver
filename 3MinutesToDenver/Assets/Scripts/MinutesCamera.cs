using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinutesCamera : MonoBehaviour {

    public GameObject target;
    public int distance;

    static MinutesCamera minutesCamera;
    bool introPan = false;

    float panSpeed = 0.5f;

    Quaternion defaultrotation;

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

        defaultrotation = transform.rotation;

    }

    public void StartIntroPan()
    {
        introPan = true;
        StartCoroutine(IntroPanRoutine());
    }

    public void StopIntroPan()
    {
        StartCoroutine(StopPanRoutine());
    }

	// Update is called once per frame
	void Update () {
        if(target != null && introPan == false)
        {
            transform.position = target.transform.position + Vector3.up * distance - Vector3.forward * 25;
           // transform.rotation = Quaternion.Euler(23, transform.rotation.y, transform.rotation.z);
        }
    }

    IEnumerator IntroPanRoutine()
    {
        transform.position = target.transform.position + Vector3.up * distance - Vector3.forward * 25;

        while (introPan == true)
        {
            transform.RotateAround(target.transform.position, target.transform.up, panSpeed);
            yield return null;
        }


       
    }

    IEnumerator StopPanRoutine()
    {
        if (introPan == true)
        {
            FindObjectOfType<PlayerController>().enabled = false;
            Vector3 targetPos = target.transform.position + Vector3.up * distance - Vector3.forward * 25; ;
            
           
            while (Vector3.Distance(transform.position, targetPos) > 1)
            {
                transform.RotateAround(target.transform.position, target.transform.up, panSpeed * 12);
                yield return null;
            }

        }
        FindObjectOfType<PlayerController>().enabled = true;


        introPan = false;

        transform.rotation = defaultrotation;
    }
    
}
