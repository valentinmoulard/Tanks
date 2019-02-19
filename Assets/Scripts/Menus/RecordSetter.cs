using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordSetter : MonoBehaviour {

    public GameObject BestMin;
    public GameObject BestSec;
    public GameObject BestMilli;
    

    void OnEnable () {
        float Min = 0;
        float Sec = 0;
        float Milli = 0;
        Type t = PlayerPrefs.GetFloat("bestMin").GetType();
        if (t.Equals(typeof(float)))
        {
            Min = PlayerPrefs.GetFloat("bestMin");
            Sec = PlayerPrefs.GetFloat("bestSec");
            Milli = PlayerPrefs.GetFloat("bestMilli");
        }


        if (Sec <= 9)
        {
            BestSec.GetComponent<Text>().text = "0" + Sec + ":";
        }
        else
        {
            BestSec.GetComponent<Text>().text = Sec + ":";
        }

        BestMin.GetComponent<Text>().text = Min.ToString() + " :";
        BestMilli.GetComponent<Text>().text = Milli.ToString();
    }

}
