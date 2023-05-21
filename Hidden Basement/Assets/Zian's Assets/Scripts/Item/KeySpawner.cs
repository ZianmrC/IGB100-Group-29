using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KeySpawner : MonoBehaviour
{
    public Transform[] KeySpawnPoints;
    public int[] OccupiedKeySpawns;

    //Keys variables
    public bool getblueKey;
    public bool getgreenKey;
    public bool getredKey;
    public bool getblackKey;
    public bool getyellowKey;

    //Key Objects
    public GameObject blueKey;
    public GameObject greenKey;
    public GameObject redKey;
    public GameObject blackKey;
    public GameObject yellowKey;

    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, KeySpawnPoints.Length);
        Transform spawnPoint = KeySpawnPoints[index];

        // spawn the enemy at the chosen spawn point
        Instantiate(blueKey, spawnPoint.position, spawnPoint.rotation);
        Instantiate(greenKey, spawnPoint.position, spawnPoint.rotation);
        Instantiate(redKey, spawnPoint.position, spawnPoint.rotation);
        Instantiate(blackKey, spawnPoint.position, spawnPoint.rotation);
        Instantiate(yellowKey, spawnPoint.position, spawnPoint.rotation);
    }
    void SpawnKey(GameObject key)
    {
        int index = Random.Range(0, KeySpawnPoints.Length);
        if (OccupiedKeySpawns.Contains(index) == false)
        {
            Transform spawnPoint = KeySpawnPoints[index];
            Instantiate(key, spawnPoint.position, spawnPoint.rotation);
        }
        else SpawnKey(key);
    }
}
