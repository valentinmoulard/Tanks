using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    public AudioSource MenuMusic;
    public Slider volumeSlider;

    
	// Update is called once per frame
	void Update () {
        MenuMusic.volume = volumeSlider.value;
        
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }
}
