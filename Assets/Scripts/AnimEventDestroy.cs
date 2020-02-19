using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventDestroy : AnimEvent
{

    protected override void ThrowEvent(string eventName, string paramName)
    {
        Destroy(gameObject);
    }
}
