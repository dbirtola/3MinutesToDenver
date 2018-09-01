using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinutesHUD : MonoBehaviour {

    public Text pointText;
    public Text comboText;
    public Slider panicSlider;

    public GameManager gameManager;

    public FloatingText floatingTextPrefab;


    public RoundFinishedPanel roundFinishedPanel;

    public void Start()
    {
        gameManager.pointsGainedEvent.AddListener(ShowPoints);
        gameManager.playerFinishedRoundEvent.AddListener(() => { roundFinishedPanel.Show(); });
    }

    public void ShowPoints(int points)
    {
        var txt = Instantiate(floatingTextPrefab, transform);
        txt.Float("+" + points, pointText.transform.position + Vector3.right * 100 - Vector3.up * 25);
    }

    public void Update()
    {
        pointText.text = gameManager.roundPoints.ToString();
        panicSlider.value = (float)gameManager.panicGainedRecently / GameManager.MAX_EXPECTED_RECENT_POINTS;
        comboText.text = gameManager.comboMultiplier + "x";
    }

    public void HidePanicSlider()
    {
        panicSlider.gameObject.SetActive(false);
    }

}
