using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MinutesCamera : MonoBehaviour {

    public GameObject target;
    public int distance;

    static MinutesCamera minutesCamera;
    bool introPan = false;

    float panSpeed = 0.5f;

    public Vector2[] planeCameraInfo;


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
        StopCoroutine(IntroPanRoutine());
        StartCoroutine(StopPanRoutine());
    }

	// Update is called once per frame
	void Update () {
        if(target != null && introPan == false)
        {
            PlaneSize size = FindObjectOfType<GameManager>().currentPlaneSize;
            transform.position = target.transform.position + Vector3.up * planeCameraInfo[(int)size].x - Vector3.forward * planeCameraInfo[(int)size].y;
           // transform.rotation = Quaternion.Euler(23, transform.rotation.y, transform.rotation.z);
        }
    }

    IEnumerator IntroPanRoutine()
    {
        PlaneSize size = FindObjectOfType<GameManager>().currentPlaneSize;
        transform.position = target.transform.position + Vector3.up * planeCameraInfo[(int)size].x - Vector3.forward * planeCameraInfo[(int)size].y;

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
            PlaneSize size = FindObjectOfType<GameManager>().currentPlaneSize;

            FindObjectOfType<PlayerController>().enabled = false;


            /*
            Vector3 startPos = transform.position;
            Vector3 targetPos = target.transform.position + Vector3.up * planeCameraInfo[(int)size].x - Vector3.forward * planeCameraInfo[(int)size].y;

            Vector3 startRotation = transform.rotation.eulerAngles;
            Vector3 targetRotation = target.transform.eulerAngles;

            Vector3 deltaPosition = (targetPos - startPos) / 30;

            
            while (transform.position != targetPos) {// && (transform.rotation.eulerAngles) != targetRotation) {

                float distance = Vector3.Distance(targetPos, transform.position);

                if(distance > 2)
                {
                    transform.position += deltaPosition;
                }else
                {
                    transform.position = targetPos;
                }

                /*

                float distanceRot = Vector3.Distance(targetRotation, startRotation);

                Vector3 deltaQuat = (targetRotation - startRotation) / 30;
                if (distanceRot > 2)
                {
                    transform.Rotate(deltaQuat);// += deltaQuat;
                }else
                {
                    transform.rotation = Quaternion.Euler(targetRotation);
                }
                
                yield return null;
            } 
            */


            
            //PlaneSize size = FindObjectOfType<GameManager>().currentPlaneSize;
            //FindObjectOfType<PlayerController>().enabled = false;
            Vector3 targetPos = target.transform.position + Vector3.up * planeCameraInfo[(int)size].x - Vector3.forward * planeCameraInfo[(int)size].y;



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
