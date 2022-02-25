using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAllComponents : MonoBehaviour
{
    [SerializeField] bool includeChildren;

    PauseManager                    pauseManager;
    Dictionary<Behaviour, bool>     prevState;

    void Awake()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        if (pauseManager == null)
        {
            Destroy(this);
        }
        else
        {
            pauseManager.onPause += OnPause;
        }
    }

    void OnDestroy()
    {
        if (pauseManager != null)
        {
            pauseManager.onPause -= OnPause;
        }
    }

    void OnPause(bool isPaused)
    {
        if (isPaused)
        {
            if (prevState != null) return;
            prevState = new Dictionary<Behaviour, bool>();

            Behaviour[] components;

            if (includeChildren) components = GetComponentsInChildren<Behaviour>();
            else components = GetComponents<Behaviour>();

            foreach (var component in components)
            {
                if (component.enabled)
                {
                    component.enabled = false;
                    prevState[component] = true;
                }
            }
        }
        else
        {
            if (prevState != null)
            {
                foreach (var component in prevState.Keys)
                {
                    component.enabled = true;
                }
                prevState = null;
            }
        }
    }
}
