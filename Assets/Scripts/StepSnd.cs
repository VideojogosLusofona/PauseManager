using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSnd : MonoBehaviour
{
    public AudioSource audioSource;

    public void StepSound()
    {
        if (audioSource)
        {
            audioSource.Play();
        }
    }
}
