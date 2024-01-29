using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackTrack : MonoBehaviour
{
    #region Variáveis Globais
    // Referências:
    private CollisionLayersManager _collisionLayersManager;
    
    private static int spawnPointIndex = 0;
    #endregion

    #region Funções Unity
    private void Start()
    {
        _collisionLayersManager = FindObjectOfType<CollisionLayersManager>();

        if (GameObject.Find("Player Spawn Point " + spawnPointIndex) != null)
            transform.position = GameObject.Find("Player Spawn Point " + spawnPointIndex).transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == _collisionLayersManager.TriggerLevel)
        {
            var levelTrigger = col.gameObject.GetComponent<LevelTrigger>();
            spawnPointIndex = levelTrigger.nextSpawnPoint;
            SceneManager.LoadScene(levelTrigger.nextScene);
        }
    }
    #endregion
}
