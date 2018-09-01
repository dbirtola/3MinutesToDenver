using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialNotification : MonoBehaviour {


    Airplane plane;
    GameManager gameManager;

    public Text specialText;

    public void TrackHangtime()
    {
        gameManager = FindObjectOfType<GameManager>();
        plane = gameManager.currentPlane;
        gameObject.SetActive(true);
        StartCoroutine(HangtimeRoutine());
    }

    IEnumerator HangtimeRoutine()
    {

        while(plane.GetComponent<AirplaneState>().planeState == PlaneState.Falling)
        {
            Debug.Log("What");
            specialText.text = "Hangtime Bonus! +" + gameManager.hangtimePoints;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
