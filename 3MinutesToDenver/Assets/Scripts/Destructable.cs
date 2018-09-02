using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

    public int pointValue;
    public GameObject destructionParticlePrefab;

    //If set to false, object will still trigger particles but be left as debris
    public bool destroyOnImpact;

    public PlaneSize requiredSizeToDestroy;

    bool hasBeenHit = false;

    public GameManager gameManager;

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Start()
    {
        //RaycastHit raycast;
        //Physics.Raycast(transform.position + Vector3.up * 100, -1 * Vector3.up, out raycast, 120);
        //transform.position = raycast.point;
        GetComponent<Rigidbody>().Sleep();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (hasBeenHit)
        {
            return;
        }

        

        if (coll.gameObject.GetComponent<Airplane>() && coll.gameObject.GetComponent<AirplaneState>().planeSize >= requiredSizeToDestroy)
        {

            if (destroyOnImpact)
            {
                Destroy(gameObject);
            }

            hasBeenHit = true;
            gameManager.destructableDestroyedEvent.Invoke(this);
        }
    }
}
