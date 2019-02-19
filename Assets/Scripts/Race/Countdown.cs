using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    public GameObject CountDown;
    public AudioSource GetReady;
    public AudioSource GoAudio;
    public GameObject LapTimer;
    public GameObject TankControl;
    public AudioSource LevelMusic;

    
    void Start () {
        GetReady.volume = PlayerPrefs.GetFloat("volume") * 0.5f;
        GoAudio.volume = PlayerPrefs.GetFloat("volume") * 0.5f;
        LevelMusic.volume = PlayerPrefs.GetFloat("volume") * 0.5f;
        StartCoroutine(CountStart());
	}

    IEnumerator CountStart()
    {
        //attend 0.5 second avant de commencer le countdown
        yield return new WaitForSeconds(0.5f);
        CountDown.GetComponent<Text>().text = "3";
        GetReady.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);

        CountDown.GetComponent<Text>().text = "2";
        GetReady.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);

        CountDown.GetComponent<Text>().text = "1";
        GetReady.Play();
        CountDown.SetActive(true);
        yield return new WaitForSeconds(1);
        CountDown.SetActive(false);
        
        GoAudio.Play();
        LevelMusic.Play();
        LapTimer.SetActive(true);
        TankControl.SetActive(true);
    }

}
