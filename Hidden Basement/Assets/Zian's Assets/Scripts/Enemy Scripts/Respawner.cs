using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void RespawnEnemy(GameObject enemy)
    {
        enemy.SetActive(true);
        foreach (Transform child in enemy.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    public static void DisableFunctionality(GameObject parentObject)
    {
        MonoBehaviour[] scripts = parentObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }
    public static void EnableFunctionality(GameObject parentObject)
    {
        MonoBehaviour[] scripts = parentObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }
    }

}
