using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMove : MonoBehaviour
{
    public float moveSpeed = 64;
    public float drag = 0.1f;

    float           movementDir;
    Vector2         currentVelocity;
    GroundDetection groundDetect;
    Rigidbody2D     rb;
    Animator        anim;

    void Start()
    {
        groundDetect = GetComponent<GroundDetection>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        currentVelocity = rb.velocity;

        if (Mathf.Abs(movementDir) > 0.01f)
        {
            currentVelocity.x = movementDir * moveSpeed;
        }
        else
        {
            currentVelocity.x *= (1.0f - drag);
        }

        rb.velocity = currentVelocity;
    }

    private void Update()
    {
        float absMovementX = Mathf.Abs(movementDir);
        anim.SetFloat("AbsSpeedX", absMovementX);

        Vector2 right = transform.right;

        if (right.x * movementDir < 0.0f)
        {
            if (movementDir > 0.01f) transform.rotation = Quaternion.identity;
            else if (movementDir < -0.01f) transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
    }   

    public void Move(float dir)
    {
        movementDir = dir;
    }
}
