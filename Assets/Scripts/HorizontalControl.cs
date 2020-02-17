using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalControl : MonoBehaviour
{
    HorizontalMove mover;

    void Start()
    {
        mover = GetComponent<HorizontalMove>();
    }

    private void Update()
    {
        mover.Move(Input.GetAxis("Horizontal"));
    }
}
 