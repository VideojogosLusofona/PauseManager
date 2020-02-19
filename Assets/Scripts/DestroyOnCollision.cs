using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : OnCollision
{
    public float      destructionTime;
    public GameObject effectDestruction;

    protected override void ThrowCollisionEvent(Collider2D objCollider)
    {
        if (effectDestruction)
        {
            Instantiate(effectDestruction, transform.position, transform.rotation);
        }
        Destroy(gameObject, destructionTime);
    }
}
