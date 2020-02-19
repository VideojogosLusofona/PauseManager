using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLocalRight : MonoBehaviour
{
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
