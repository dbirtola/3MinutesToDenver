using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinutesHUD : MonoBehaviour {

    public Text pointText;
    public Text comboText;
    public Text cashText;
    public Text timerText;
    public Slider panicSlider;

    public GameManager gameManager;

    public FloatingText floatingTextPrefab;

    public SpecialNotification specialNotification;

    public RoundFinishedPanel roundFinishedPanel;

    static MinutesHUD minutesHUD;

    Coroutine timerRoutine;

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


        gameManager.currentPlane.GetComponent<AirplaneState>().stateChangedEvent.AddListener((PlaneState state) => {
            if (state == PlaneState.Falling)
            {
                specialNotification.TrackHangtime();
            }
        });

        StartCoroutine(TimerRoutine());
    }

    public IEnumerator TimerRoutine()
    {
        var gm = FindObjectOfType<GameManager>();
        while (true)
        {
            float timeRemaining =  gm.timeStarted + 180 - Time.time;
            timerText.text = "Time Expected: " + (int)timeRemaining / 60 + ":" + (int)timeRemaining % 60;
            yield return null;
        }
    }

    public void UpdateCash(int newCash)
    {
        cashText.text = "$" + gameManager.cash;
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
        if(gameManager.comboMultiplier > 1)
        {
            comboText.gameObject.SetActive(true);

            comboText.text = gameManager.comboMultiplier + "x";
        }else
        {
            comboText.gameObject.SetActive(false);
        }


        if (specialNotification.gameObject.activeSelf)
        {
            pointText.gameObject.SetActive(false);
        }
        else
        {
            pointText.gameObject.SetActive(true);
        }
    }

    public void HidePanicSlider()
    {
        panicSlider.gameObject.SetActive(false);
    }

}
