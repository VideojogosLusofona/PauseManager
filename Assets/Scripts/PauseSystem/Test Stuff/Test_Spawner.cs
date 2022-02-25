using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float      spawnTime;

    float timeToSpawn;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToSpawn -= Time.deltaTime;

        if (timeToSpawn <= 0.0f)
        {
            timeToSpawn = spawnTime;

            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}
