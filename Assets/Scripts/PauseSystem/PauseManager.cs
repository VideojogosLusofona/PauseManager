using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject canvasObject;
                     bool       isPaused = false;

    public delegate void PauseDelegate(bool b);
    public event PauseDelegate onPause;

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

        onPause?.Invoke(isPaused);
        canvasObject?.SetActive(isPaused);
    }
}
