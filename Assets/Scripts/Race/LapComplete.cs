using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LapComplete : MonoBehaviour {

    public GameObject LapCompleteTrig;
    
    public GameObject MinuteDisplay;
    public GameObject SecondDisplay;
    public GameObject MilliDisplay;

    public GameObject CurrentLap;
    public static int lap = 1;

    public static float minuteRecord = Mathf.Infinity;
    public static float secondeRecord = Mathf.Infinity;
    public static float milliRecord = Mathf.Infinity;

    void Awake()
    {
        minuteRecord = Mathf.Infinity;
        secondeRecord = Mathf.Infinity;
        milliRecord = Mathf.Infinity;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tank"))
        {
            if (LapTimeManager.MinuteCount < minuteRecord)
            {
                SetRecord();
            }else if (LapTimeManager.MinuteCount == minuteRecord && LapTimeManager.SecondCount < secondeRecord)
            {
                SetRecord();
            }else if (LapTimeManager.SecondCount == secondeRecord && LapTimeManager.Millicount < milliRecord)
            {
                SetRecord();
            }

            //remet les compteur à 0
            LapTimeManager.MinuteCount = 0;
            LapTimeManager.SecondCount = 0;
            LapTimeManager.Millicount = 0;
            //tour suivant
            lap++;
            CurrentLap.GetComponent<Text>().text = lap.ToString() + "  :";
        }
    }


    void SetRecord()
    {
        //display du meilleur temps
        //LapTimeManager.SecondCount : fait référence à la variable static dans le script LapTimeManager
        if (LapTimeManager.SecondCount <= 9)
        {
            SecondDisplay.GetComponent<Text>().text = "0" + LapTimeManager.SecondCount + ".";
        }
        else
        {
            SecondDisplay.GetComponent<Text>().text = "" + LapTimeManager.SecondCount + ".";
        }

        if (LapTimeManager.MinuteCount <= 9)
        {
            MinuteDisplay.GetComponent<Text>().text = "0" + LapTimeManager.MinuteCount + ":";
        }
        else
        {
            MinuteDisplay.GetComponent<Text>().text = "" + LapTimeManager.MinuteCount + ":";
        }

        MilliDisplay.GetComponent<Text>().text = LapTimeManager.Millicount.ToString();

        minuteRecord = LapTimeManager.MinuteCount;
        secondeRecord = LapTimeManager.SecondCount;
        milliRecord = LapTimeManager.Millicount;
    }



}
