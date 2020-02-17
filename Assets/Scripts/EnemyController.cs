using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform frontDetector;
    public LayerMask groundMask;

    HorizontalMove horizontalMover;

    float dir;

    void Start()
    {
        dir = transform.right.x;
        horizontalMover = GetComponent<HorizontalMove>();
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(frontDetector.position, 2, groundMask) != null)
        {
            dir = -dir;
        }

        horizontalMover.Move(dir);
    }
}
