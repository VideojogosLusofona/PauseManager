using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventSndPlay : AnimEvent 
{
    [System.Serializable]
    public struct Sound
    {
        public string       name;
        public AudioSource  audio;
    }

    public Sound[] sounds;

    protected override void ThrowEvent(string eventName, string paramName)
    {
        if (sounds == null) return;
         
        foreach (var snd in sounds)
        {
            if (snd.name == paramName)
            {
                snd.audio.Play();
            }
        }
    }
}
