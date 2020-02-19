using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    public string animEvent;

    public void RunAnimEvent(string eventData)
    {
        string evtName = eventData;
        string paramName = "";
        int    index = evtName.IndexOf(':');

        if (index != -1)
        {
            paramName = eventData.Substring(index + 1);
            evtName = eventData.Substring(0, index - 1);
        }

        if ((animEvent == "") || (animEvent == evtName))
        {
            ThrowEvent(evtName, paramName);
        }
    }

    protected virtual void ThrowEvent(string eventName, string paramName)
    {

    }
}
