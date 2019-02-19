using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : MonoBehaviour {
    public GameObject PauseMenu;

    public void Resume()
    {
        if (Time.timeScale == 0 && PauseMenu.activeSelf == true)
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
        }
    }
	
}
