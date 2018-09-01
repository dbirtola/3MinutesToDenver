using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

    public int pointValue;
    public GameObject destructionParticlePrefab;

    //If set to false, object will still trigger particles but be left as debris
    public bool destroyOnImpact;

    bool hasBeenHit = false;

    public GameManager gameManager;

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
	
    void OnCollisionEnter(Collision coll)
    {
        if (hasBeenHit)
        {
            return;
        }

        

        if (coll.gameObject.GetComponent<TestControls>())
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
