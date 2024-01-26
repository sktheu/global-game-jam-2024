using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject danceMove;
    private int spawnTime;
    public int maxSpawnTime;
    public int minSpawnTime;

    //Tempo que vai ficar lançando as danças
    public float limitTime = 20f;
    private float startTime;
    public bool spawnerSwitch = true;

    void Start()
    {
        startTime = Time.time;
        StartCoroutine(SpawnObjectsCoroutine());
    }

    private void Update()
    {
        if (Time.time - startTime >= limitTime)
        {
            //Para o funcionamento do spawner
            spawnerSwitch = false;    
        }
    }

    IEnumerator SpawnObjectsCoroutine()
    {
        while (spawnerSwitch == true)
        {
            yield return new WaitForSeconds(spawnTime);

            SpawnObject();
        }
        Debug.Log("Acabou Minigame");
    }

    void SpawnObject()
    {
        Instantiate(danceMove, transform.position, Quaternion.identity);
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
}