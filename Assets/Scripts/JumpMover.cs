using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMover : MonoBehaviour
{
    public float jumpSpeed = 128;

    Rigidbody2D rb;
    Animator    anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetFloat("SpeedY", rb.velocity.y);
    }

    public void Jump(float scale = 1.0f)
    {
        var velocity = rb.velocity;

        velocity.y = jumpSpeed * scale;

        rb.velocity = velocity;
    }
}
