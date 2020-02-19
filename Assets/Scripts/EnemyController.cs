using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EnemyController : MonoBehaviour
{
    public Transform frontDetector;
    public LayerMask groundMask;
    public bool      enableShoot;
    [ShowIf("enableShoot")]
    public LayerMask shootTargetMask;
    public string[]  shootTargetTags;

    HorizontalMove horizontalMover;
    Weapon         weapon;

    float dir;

    void Start()
    {
        dir = transform.right.x;
        horizontalMover = GetComponent<HorizontalMove>();
        weapon = GetComponent<Weapon>();
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(frontDetector.position, 2, groundMask) != null)
        {
            dir = -dir;
        }

        horizontalMover.Move(dir);

        if (enableShoot)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(weapon.shootPoint.position, weapon.shootPoint.right, shootTargetMask);
            foreach (var hit in hits)
            {
                if ((shootTargetTags != null) && (shootTargetTags.Length > 0))
                {
                    if (System.Array.IndexOf(shootTargetTags, hit.collider.tag) == -1) continue;
                }

                weapon.Shoot();
            }
        }
    }
}
