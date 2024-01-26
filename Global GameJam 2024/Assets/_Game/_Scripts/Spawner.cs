using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Global Variables
    [Header("Configurações:")]
    public GameObject danceMove;
    private int spawnTime;
    public int maxSpawnTime;
    public int minSpawnTime;
    [SerializeField] private int targetPoints;

    [Header("Diálogos:")]
    [SerializeField] private Dialogue initialDialogue;
    [SerializeField] private Dialogue winDialogue;

    //Tempo que vai ficar lançando as danças
    public float limitTime = 20f;
    private float startTime;
    public bool spawnerSwitch = true;

    private static bool _win = false;
    #endregion

    #region Unity Functions
    private void Start()
    {
        if (_win)
        {
            winDialogue.enabled = true;
            this.enabled = false;
        }

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
    #endregion

    #region Custom Functions
    private IEnumerator SpawnObjectsCoroutine()
    {
        while (spawnerSwitch == true)
        {
            yield return new WaitForSeconds(spawnTime);

            SpawnObject();
        }
        Debug.Log("Acabou Minigame");

        if (GuitarHero.CurrentPoints >= targetPoints)
        {
            winDialogue.enabled = true;
            this.enabled = false;
        }
        else
        {
            initialDialogue.enabled = true;
            this.enabled = false;
            GuitarHero.CurrentPoints = 0;
        }
    }

    private void SpawnObject()
    {
        Instantiate(danceMove, transform.position, Quaternion.identity);
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
    #endregion
}