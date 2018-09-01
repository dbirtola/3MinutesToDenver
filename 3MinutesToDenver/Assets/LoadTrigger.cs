using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTrigger : MonoBehaviour {
    

    public string sceneToBeLoaded;

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Airplane>())
        {
            SceneManager.LoadSceneAsync(sceneToBeLoaded, LoadSceneMode.Additive);
        }
    }
}
