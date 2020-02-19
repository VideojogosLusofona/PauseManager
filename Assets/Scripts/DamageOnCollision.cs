using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

public class DamageOnCollision : OnCollision
{
    public float damage = 1;

    protected override void ThrowCollisionEvent(Collider2D objCollider)
    {
        Health health = objCollider.GetComponent<Health>();
        if (health == null) health = objCollider.GetComponentInParent<Health>();

        if (health == null) return;

        health.DealDamage(damage);

    }
}
