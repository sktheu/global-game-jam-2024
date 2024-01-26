using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject danceMove;
    public int spawnTime;
    public int maxSpawnTime;
    public int minSpawnTime;

    void Start()
    {
        StartCoroutine(SpawnObjectsCoroutine());
    }

    IEnumerator SpawnObjectsCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            SpawnObject();
        }
    }

    void SpawnObject()
    {
        Instantiate(danceMove, transform.position, Quaternion.identity);
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
}