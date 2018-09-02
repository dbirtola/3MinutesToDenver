﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoundFinishedPanel : MonoBehaviour {

    public Text pointsEarnedText;
    public Text distanceTraveledText;
    public Text upgradeCostText;

    public void Show(RoundResults results)
    {
        pointsEarnedText.text = "Points earned: " + results.pointsEarned;
        distanceTraveledText.text = "Distance traveled: " + results.distance;
        upgradeCostText.text = "Upgrade Plane: $" + FindObjectOfType<GameManager>().GetUpgradeCost();
        gameObject.SetActive(true);
    }
}
