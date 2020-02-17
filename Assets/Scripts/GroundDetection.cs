using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public Transform groundPoint;
    public float     radius = 2;
    public LayerMask groundLayer;

    [HideInInspector]
    public bool isGrounded;

    Animator animator;

    void Start()
    {
        if (groundPoint == null) groundPoint = transform;

        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, radius, groundLayer) != null;
    }

    private void LateUpdate()
    {
        if (animator)
        {
            animator.SetBool("Grounded", isGrounded);
        }
    }
}
