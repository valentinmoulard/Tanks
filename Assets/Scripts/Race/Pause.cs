using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    public GameObject pausePanel;

    void Start()
    {
        pausePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (!pausePanel.activeSelf && Time.timeScale != 0)
            {
                PauseGame();
            }
            else
            {
                ContinueGame();
            }
            
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
