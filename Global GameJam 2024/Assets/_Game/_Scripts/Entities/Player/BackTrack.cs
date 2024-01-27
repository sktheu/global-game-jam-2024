using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackTrack : MonoBehaviour
{
    private static int spawnPointIndex = 1;
    [SerializeField] private float sceneLoadTime;

    public static string PreviousScene;

    private static bool lastFlip;

    private CollisionLayersManager _collisionLayersManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _collisionLayersManager = FindObjectOfType<CollisionLayersManager>();
    }

    private void Start()
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        GetComponent<SpriteRenderer>().flipX = lastFlip;

        if (GameObject.Find("Player Spawn Point " + spawnPointIndex) != null)
            transform.position = GameObject.Find("Player Spawn Point " + spawnPointIndex).transform.position;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == _collisionLayersManager.TriggerLevel.Index)
        {
            var trigger = col.gameObject.GetComponent<LevelTrigger>();
            LoadNewScene(trigger.nextScene, trigger.nextSpawnPoint);
        }
    }

    private void LoadNewScene(string sceneName, int nextSpawnPointIndex)
    {
        spawnPointIndex = nextSpawnPointIndex;

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Animator>().speed = 0f;

        StartCoroutine(LoadInterval(sceneName, sceneLoadTime));
    }

    private IEnumerator LoadInterval(string sceneName, float t)
    {
        PreviousScene = SceneManager.GetActiveScene().name;
        lastFlip = GetComponent<SpriteRenderer>().flipX;
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(sceneName);
    }
}
