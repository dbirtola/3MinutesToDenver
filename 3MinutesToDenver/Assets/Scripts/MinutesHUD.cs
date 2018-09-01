using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinutesHUD : MonoBehaviour {

    public Text pointText;
    public Text comboText;
    public Text cashText;
    public Slider panicSlider;

    public GameManager gameManager;

    public FloatingText floatingTextPrefab;

    public SpecialNotification specialNotification;

    public RoundFinishedPanel roundFinishedPanel;

    static MinutesHUD minutesHUD;

    public void Awake()
    {
        if(minutesHUD != null)
        {
            Destroy(gameObject);
            return;
        }else
        {
            minutesHUD = this;
        }

    }

    public void Start()
    {
        gameManager.roundStartedEvent.AddListener(() => { RoundStart(); });
        gameManager.pointsGainedEvent.AddListener(ShowPoints);
        gameManager.playerFinishedRoundEvent.AddListener(roundFinishedPanel.Show);
        gameManager.cashGainedEvent.AddListener(UpdateCash);
  
        gameObject.SetActive(false);
    }

    public void RoundStart()
    {
        gameObject.SetActive(true);
        gameManager.currentPlane.GetComponent<AirplaneState>().leftGroundEvent.AddListener(() => { specialNotification.TrackHangtime(); });
    }

    public void UpdateCash(int newCash)
    {
        cashText.text = "Cash MoFuckin Money: $$" + gameManager.cash;
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
