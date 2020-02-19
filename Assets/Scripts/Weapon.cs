using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float        cooldown = 0.25f;
    public Transform    shootPoint;
    public GameObject   shotPrefab;

    Animator anim;
    float    cooldownTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (shootPoint == null) shootPoint = transform;
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        if (cooldownTimer > 0.0f)
        {
            return;
        }

        anim.SetTrigger("Attack");

        cooldownTimer = cooldown;

        if (shotPrefab) 
        {
            Instantiate(shotPrefab, shootPoint.position, shootPoint.rotation);
        }
    }
}
