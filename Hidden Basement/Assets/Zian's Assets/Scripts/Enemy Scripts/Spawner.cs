using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints; // Array of spawn points
    public GameObject[] prespawnEnemies; //Array of spawn points upon initialization
    public GameObject enemyPrefab; // Enemy GameObject to spawn
    public float spawnTimer = 2f; // Time interval between spawns

    private float timer = 0f; // Timer to track spawn intervals
    public GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        // Start the timer at a random value to avoid all enemies spawning at once
        timer = Random.Range(0f, spawnTimer);
        foreach (GameObject spawnPoint in prespawnEnemies)
        {
            spawnPoint.SetActive(true);
        }
        gun.SetActive(true);
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
    void SpawnEnemy(Transform spawnPoint)
    {
        // Instantiate the enemy at the chosen spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
    public static void RespawnEnemy(GameObject enemy)
    {
        enemy.SetActive(true);
        foreach (Transform child in enemy.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

}
