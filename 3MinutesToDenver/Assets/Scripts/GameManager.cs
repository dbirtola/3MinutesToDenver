using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class IntEvent : UnityEvent<int>{

}

public class FloatEvent : UnityEvent<float>
{

}

public class DestructableEvent : UnityEvent<Destructable>
{

}

public class RoundEvent : UnityEvent<RoundResults>{

}

public class RoundResults{
    public float distance;
    public int pointsEarned;
}

public class GameManager : MonoBehaviour {
    static GameManager gameManager;

    public RoundEvent playerFinishedRoundEvent { get; private set; }
    public DestructableEvent destructableDestroyedEvent { get; private set; }
    public IntEvent pointsGainedEvent { get; private set; }
    public FloatEvent panicGainedEvent { get; private set; }
    public UnityEvent comboStartedEvent { get; private set; }
    public UnityEvent comboEndedEvent { get; private set; }
    public UnityEvent roundStartedEvent { get; private set; }
    public IntEvent cashGainedEvent { get; private set; }

    public MinutesCamera minutesCamera;
    public PlayerController player;
    public Airplane airplanePrefab;
    public StartPoint startPoint;
    public EndPoint endPoint;

    public Airplane currentPlane;

    public int roundPoints { get; private set; }
    public float panicPoints { get; private set; }

    public const int MAX_EXPECTED_RECENT_POINTS = 1000;

    public float panicGainedRecently = 0;

    public const float panicDeteriorationPerSecond = 75;

    public float comboMultiplier { get; private set; }

    public bool roundInProgress = false;

    public const int POINTS_PER_SECOND_HANGTIME = 50;

    public int cash = 0;

    public float hangtimePoints = 0;


    public void Awake()
    {
        if(gameManager != null)
        {
            Destroy(gameObject);
            return;
        }else
        {
            gameManager = this;
        }

        playerFinishedRoundEvent = new RoundEvent();
        destructableDestroyedEvent = new DestructableEvent();
        pointsGainedEvent = new IntEvent();
        panicGainedEvent = new FloatEvent();
        comboStartedEvent = new UnityEvent();
        comboEndedEvent = new UnityEvent();
        roundStartedEvent = new UnityEvent();
        cashGainedEvent = new IntEvent();


    }

    public void Start()
    {
        destructableDestroyedEvent.AddListener(ProcessDestruction);

       // StartGame();
    }

    public void Update()
    {
        if (roundInProgress)
        {
            panicGainedRecently -= panicDeteriorationPerSecond * Time.deltaTime;

            if(panicGainedRecently < 0)
            {
                panicGainedRecently = 0;
            }
            
            CalculateCombo();
        }
    }

    public void AddCash(int cash)
    {
        this.cash += cash;
        cashGainedEvent.Invoke(cash);
    }

    public void AddPanic(float panic)
    {
        panicGainedRecently += panic;
        panicGainedEvent.Invoke(panic);
    }

    //Combo will be a range of 1-5x points in current setup, evenly spaced based on how close to max player is 
    void CalculateCombo()
    {
        comboMultiplier = (int)(panicGainedRecently * 5 / MAX_EXPECTED_RECENT_POINTS) + 1;
        if (comboMultiplier > 5)
        {
            comboMultiplier = 5;
        }
    }

    public void RemovePanic(float panic)
    {
        panicPoints -= panic;
    }

    public void RestartGame()
    {
       // SceneManager.LoadScene(1);
        StartGame();
    }

    public void StartGame()
    {
        StartCoroutine(StartRoutine());

        
    }

    //Wrapping in routine so we can wait till load scene finishes which happesn the next frame
    IEnumerator StartRoutine()
    {
        SceneManager.LoadScene(1);
        yield return null;

        startPoint = FindObjectOfType<StartPoint>();
        endPoint = FindObjectOfType<EndPoint>();

        endPoint.playerEnteredZoneEvent.AddListener(ProcessResults);

        Debug.Log("Starting");

        //SceneManager.LoadScene(0);
        if (currentPlane != null)
        {
            Destroy(currentPlane.gameObject);
        }
        //Spawn characters plane at the spawn point
        panicPoints = 0;
        roundPoints = 0;
        panicGainedRecently = 0;
        comboMultiplier = 1f;
        roundInProgress = true;

        currentPlane = Instantiate(airplanePrefab, startPoint.transform.position, startPoint.transform.rotation);
        player.player = currentPlane.gameObject;

        FindObjectOfType<MinutesCamera>().target = currentPlane.gameObject;


        currentPlane.GetComponent<AirplaneState>().stateChangedEvent.AddListener((PlaneState state) => {
        if (state == PlaneState.Falling) {
                StartCoroutine(HangtimeRoutine());
                    }
        });

        roundStartedEvent.Invoke();

        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1);
        var dialogueManager = GetComponent<DialogueManager>();

        dialogueManager.QueueDialogue("Air Traffic Control", "Hello flight 247 this is Air Traffic Control", 2.5f);
        dialogueManager.QueueDialogue("Air Traffic Control", "We're going to have you ready for flight here in just a couple seconds", 2.5f);
        dialogueManager.QueueDialogue("Air Traffic Control", "Honestly, this is my first day. So bare with me here", 2.5f);


    }

    IEnumerator HangtimeRoutine()
    {
        Debug.Log("started hangtimeRoutine");
        hangtimePoints = 0;
        while(currentPlane.GetComponent<AirplaneState>().planeState == PlaneState.Falling)
        {
            float points = POINTS_PER_SECOND_HANGTIME * Time.deltaTime;
            hangtimePoints += points;
            AddPanic(points);
            yield return null;
        }

        AwardPoints((int)hangtimePoints);
    }

    public void ProcessResults()
    {
        //Turn players points into cash

        roundInProgress = false;

        AddCash(roundPoints);

        RoundResults roundResults = new RoundResults();
        roundResults.pointsEarned = roundPoints;
        roundResults.distance = currentPlane.transform.position.z - startPoint.transform.position.z;
        playerFinishedRoundEvent.Invoke(roundResults);

    }

    void ProcessDestruction(Destructable destructable)
    {

        AddPanic(destructable.pointValue);
        AwardPoints(destructable.pointValue);
        
    }


    public void AwardPoints(int points)
    {
        int pointsGained = (int)(points * comboMultiplier);
        roundPoints += pointsGained;
        pointsGainedEvent.Invoke(pointsGained);
    }
}
