using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RoundFinishedPanel : MonoBehaviour {

    public Text pointsEarnedText;
    public Text distanceTraveledText;
    public Text upgradeCostText;
    public Text causeOfDeathText;

    public void Show(RoundResults results)
    {
        pointsEarnedText.text = "Points earned: " + results.pointsEarned;
        distanceTraveledText.text = "Distance traveled: " + results.distance;
        causeOfDeathText.text = "Cause of death: " + results.causeOfDeath;
        UpdateUpgradeButton();

        gameObject.SetActive(true);
    }


    public void UpdateUpgradeButton()
    {
        upgradeCostText.text = "Upgrade Plane: $" + FindObjectOfType<GameManager>().GetUpgradeCost();
        var gm = FindObjectOfType<GameManager>();
        if (gm.cash >= gm.GetUpgradeCost())
        {
            upgradeCostText.transform.parent.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            upgradeCostText.transform.parent.gameObject.GetComponent<Button>().interactable = false;
        }

        if(gm.currentPlaneSize == PlaneSize.large)
        {
            upgradeCostText.transform.parent.gameObject.SetActive(false);
        }
    }
}
