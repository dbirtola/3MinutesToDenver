using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTrigger : MonoBehaviour {
    

    public string sceneToBeLoaded;

    public bool hasBeenLoaded = false;

    public void OnTriggerEnter(Collider col)
    {
        if (hasBeenLoaded == false && col.gameObject.GetComponent<Airplane>())
        {
            SceneManager.LoadSceneAsync(sceneToBeLoaded, LoadSceneMode.Additive);
            hasBeenLoaded = true;
           
        }
    }
}
