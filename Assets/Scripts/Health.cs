using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float        maxHealth = 100;
    public bool         invulnerabilityAfterHit = false;
    [ShowIf("invulnerabilityAfterHit")]
    public float        invulnerabilityTime;
    public UnityEvent[] onTakeDamage;

    float       health;
    Animator    anim;
    float       invulnerabilityTimer;

    public bool isInvulnerable
    {
        get { return invulnerabilityTimer > 0.0f;       }
    }

    void Start()
    {
        health = maxHealth;
        invulnerabilityTimer = 0;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (invulnerabilityTimer > 0.0f)
        {
            invulnerabilityTimer -= Time.deltaTime;

            if (invulnerabilityTimer <= 0.0f)
            {
                anim.SetBool("Invulnerable", false);
            }
        }
    }

    public void DealDamage(float damage)
    {
        if (invulnerabilityTimer > 0) return;

        health -= damage;

        if (health <=0)
        {
            anim.SetTrigger("Dead");
        }
        else
        {
            anim.SetTrigger("Hit");

            if (invulnerabilityAfterHit)
            {
                invulnerabilityTimer = invulnerabilityTime;
                anim.SetBool("Invulnerable", true);
            }

            if (onTakeDamage != null)
            {
                foreach (var evt in onTakeDamage)
                {
                    evt.Invoke();
                }
            }
        }
    }
}
