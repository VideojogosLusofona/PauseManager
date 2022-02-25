using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PauseController : MonoBehaviour
{
    [SerializeField] bool               disablePhysics = true;
    [SerializeField] List<Behaviour>    componentsToPause;

    PauseManager                        pauseManager;
    Dictionary<int, bool>               prevState;
    Rigidbody2D                         rb2d;
    Rigidbody                           rb3d;
    bool                                rbState;
    Vector3                             rbVelocity;
    Vector3                             rbAngVelocity;
    

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

            rb2d = GetComponent<Rigidbody2D>();
            rb3d = GetComponent<Rigidbody>();
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
            prevState = new Dictionary<int, bool>();

            if (componentsToPause != null)
            {
                foreach (var component in componentsToPause)
                {
                    prevState.Add(component.GetInstanceID(), component.enabled);
                    component.enabled = false;
                }
            }

            if (disablePhysics)
            {
                if (rb2d)
                {
                    rbState = rb2d.simulated;
                    rb2d.simulated = false;
                }
                if (rb3d)
                {
                    rbState = rb3d.isKinematic;
                    rbVelocity = rb3d.velocity;
                    rbAngVelocity = rb3d.angularVelocity;
                    rb3d.isKinematic = true;
                }
            }
        }
        else
        {
            if (prevState != null)
            {
                foreach (var component in componentsToPause)
                {
                    component.enabled = prevState[component.GetInstanceID()];
                }
                prevState = null;
            }

            if (disablePhysics)
            {
                if (rb2d)
                {
                    rb2d.simulated = rbState;
                }
                if (rb3d)
                {
                    rb3d.isKinematic = rbState;
                    rb3d.velocity = rbVelocity;
                    rb3d.angularVelocity = rbAngVelocity;
                    rb3d.WakeUp();
                }
            }
        }
    }

    [Button("Autofill")]
    void Autofill()
    {
        componentsToPause = new List<Behaviour>();

        var components = GetComponents<Behaviour>();
        foreach (var component in components)
        {
            componentsToPause.Add(component);
        }
    }
}
