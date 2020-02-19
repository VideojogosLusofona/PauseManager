using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            weapon.Shoot();
        }
    }
}
