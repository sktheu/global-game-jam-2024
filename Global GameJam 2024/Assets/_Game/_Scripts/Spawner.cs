using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject danceMove;
    public float spawnTime = 1.0f;

    void Start()
    {
        InvokeRepeating("SpawnObject", 2f, spawnTime);
    }

    void SpawnObject()
    {
        Instantiate(danceMove, transform.position, Quaternion.identity);
    }
}
