using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints; // Array of spawn points
    public GameObject enemyPrefab; // Enemy GameObject to spawn
    public float spawnTimer = 2f; // Time interval between spawns

    private float timer = 0f; // Timer to track spawn intervals

    // Start is called before the first frame update
    void Start()
    {
        // Start the timer at a random value to avoid all enemies spawning at once
        timer = Random.Range(0f, spawnTimer);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to spawn a new enemy
        if (timer >= spawnTimer)
        {
            // Reset the timer
            timer = 0f;

            // Spawn a new enemy at a random spawn point
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Choose a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Instantiate the enemy at the chosen spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
