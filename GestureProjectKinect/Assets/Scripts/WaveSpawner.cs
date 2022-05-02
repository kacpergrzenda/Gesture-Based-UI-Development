using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    
    /* Public Variables */
    public float spawnRate = 1.0f;
    public int enemyCount;
    public GameObject enemy;
    public Transform[] spawnPoints;

    // Update is called once per frame
    void Update()
    {
        
    }

    void Start()
    {
        StartCoroutine(waveSpawner());
    }

    /* Coroutine to create enemies */
    IEnumerator waveSpawner() {

        // Create Enemy
        for (int i = 0; i < enemyCount; i++)
        {
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Get a Random SpawnPoint.
            Instantiate(enemy, randomPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
