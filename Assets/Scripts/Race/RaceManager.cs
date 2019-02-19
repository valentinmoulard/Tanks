using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour {

    public GameObject DisableTurrets;
    private bool TurretsAreActive = true;
    public GameObject DisableTank;
    private bool TankIsActive = true;

    private bool recorded = false;
    
    public GameObject FinishPanel;

    public GameObject MinuteDisplay;
    public GameObject SecondDisplay;
    public GameObject MilliDisplay;

    void Update()
    {
        if (LapComplete.lap > 1)
        {
            DisableTank.GetComponent<TankMovement>().enabled = false;
            TankIsActive = false;

            for (int i = 0; i < DisableTurrets.transform.childCount; ++i)
            {
                DisableTurrets.transform.GetChild(i).gameObject.GetComponent<TurretShooting>().enabled = false;
            }

            TurretsAreActive = false;
        }

        if (TankIsActive == false && TurretsAreActive == false)
        {
            if (recorded == false)
            {
                recorded = true;
                LapComplete.lap = 1;
                PlayerPrefs.SetFloat("bestMin", LapComplete.minuteRecord);
                PlayerPrefs.SetFloat("bestSec", LapComplete.secondeRecord);
                PlayerPrefs.SetFloat("bestMilli", LapComplete.milliRecord);
            }
            SetRecord();
            FinishPanel.SetActive(true);
            
        }
    }

    void SetRecord()
    {
        if (LapComplete.secondeRecord <= 9)
        {
            SecondDisplay.GetComponent<Text>().text = "0" + LapComplete.secondeRecord + ".";
        }
        else
        {
            SecondDisplay.GetComponent<Text>().text = "" + LapComplete.secondeRecord + ".";
        }

        if (LapComplete.minuteRecord <= 9)
        {
            MinuteDisplay.GetComponent<Text>().text = "0" + LapComplete.minuteRecord + ":";
        }
        else
        {
            MinuteDisplay.GetComponent<Text>().text = "" + LapComplete.minuteRecord + ":";
        }
        MilliDisplay.GetComponent<Text>().text = LapComplete.milliRecord.ToString();
    }

    
}
