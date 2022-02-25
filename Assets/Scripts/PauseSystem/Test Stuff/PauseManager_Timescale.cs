using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager_Timescale : MonoBehaviour
{
    [SerializeField] GameObject canvasObject;
                     bool       isPaused = false;

    void Start()
    {
        canvasObject?.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0.0f;
            canvasObject?.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            canvasObject?.SetActive(false);
        }
    }
}
