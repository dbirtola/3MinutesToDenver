using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntEvent : UnityEvent<int>{

}

public class FloatEvent : UnityEvent<float>
{

}

public class DestructableEvent : UnityEvent<Destructable>
{

}

public class GameManager : MonoBehaviour {

    public UnityEvent playerFinishedRoundEvent { get; private set; }
    public DestructableEvent destructableDestroyedEvent { get; private set; }
    public IntEvent pointsGainedEvent { get; private set; }
    public FloatEvent panicGainedEvent { get; private set; }
    public UnityEvent comboStartedEvent { get; private set; }
    public UnityEvent comboEndedEvent { get; private set; }

    public MinutesCamera minutesCamera;
    public PlayerController player;
    public Airplane airplanePrefab;
    public StartPoint startPoint;
    public EndPoint endPoint;

    public Airplane currentPlane;

    public int roundPoints { get; private set; }
    public float panicPoints { get; private set; }

    public const int MAX_EXPECTED_RECENT_POINTS = 100;

    public float panicGainedRecently = 0;

    public const float panicDeteriorationPerSecond = 20;

    public float comboMultiplier { get; private set; }

    public bool roundInProgress = false;

    public void Awake()
    {
        playerFinishedRoundEvent = new UnityEvent();
        destructableDestroyedEvent = new DestructableEvent();
        pointsGainedEvent = new IntEvent();
        panicGainedEvent = new FloatEvent();
        comboStartedEvent = new UnityEvent();
        comboEndedEvent = new UnityEvent();

    }

    public void Start()
    {
        destructableDestroyedEvent.AddListener(ProcessDestruction);
        endPoint.playerEnteredZoneEvent.AddListener(ProcessResults);

        StartGame();
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

    public void AddPanic(int panic)
    {
        panicGainedRecently += panic;
        panicGainedEvent.Invoke(panic);
    }

    //Combo will be a range of 1-5x points in current setup, evenly spaced based on how close to max player is 
    void CalculateCombo()
    {
        comboMultiplier = (int)(panicGainedRecently * 5 / MAX_EXPECTED_RECENT_POINTS) + 1;
    }

    public void RemovePanic(float panic)
    {
        panicPoints -= panic;
    }

    public void StartGame()
    {
        if(currentPlane != null)
        {
            Destroy(currentPlane.gameObject);
        }
        //Spawn characters plane at the spawn point
        panicPoints = 0;
        roundPoints = 0;
        panicGainedRecently = 0;
        comboMultiplier = 1f;
        roundInProgress = true;

        currentPlane  = Instantiate(airplanePrefab, startPoint.transform.position, startPoint.transform.rotation);
        player.player = currentPlane.gameObject;
        minutesCamera.target = currentPlane.gameObject;
        
    }

    public void ProcessResults()
    {
        //Turn players points into cash
        roundInProgress = false;
        playerFinishedRoundEvent.Invoke();
        Debug.Log("FinishedRound");

    }

    void ProcessDestruction(Destructable destructable)
    {

        AddPanic(destructable.pointValue);
        AwardPoints(destructable.pointValue);
        
    }


    public void AwardPoints(int points)
    {
        roundPoints += (int)(points * comboMultiplier);
        pointsGainedEvent.Invoke(points);
    }
}
