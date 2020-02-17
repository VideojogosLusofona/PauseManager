using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpControl : MonoBehaviour
{
    public float    jumpSustainMaxTime = 0.2f;
    public float    gravityJumpMultiplier = 4.0f;

    Rigidbody2D     rb;
    GroundDetection groundDetection;
    JumpMover       jumpMover;

    float           jumpTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundDetection = GetComponent<GroundDetection>();
        jumpMover = GetComponent<JumpMover>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (Mathf.Abs(rb.velocity.y) < 0.01f)
            {
                if (groundDetection.isGrounded)
                {
                    jumpMover.Jump();

                    jumpTime = Time.time;

                    rb.gravityScale = 1.0f;
                }
            }

        }
        else if (Input.GetButton("Jump"))
        {
            if (rb.velocity.y > 0.01f)
            {
                if ((Time.time - jumpTime) > jumpSustainMaxTime)
                {
                    rb.gravityScale = gravityJumpMultiplier;
                }
            }
            else
            {
                rb.gravityScale = gravityJumpMultiplier;
            }
        }
        else
        {
            rb.gravityScale = gravityJumpMultiplier;
        }
    }
}
