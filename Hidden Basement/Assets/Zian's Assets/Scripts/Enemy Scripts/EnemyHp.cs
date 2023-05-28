using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour, IInteractable
{
    public float maxHealth;
    private float health;
    public GameObject deathEffect;
    public GameObject enemy;
    public GameObject parentObject; //The overall parent object of the enemy
    [SerializeField] private string _prompt;
    [SerializeField] private bool Phase1Enemy;

    // Disable Detection UI effects if Enemy is killed
    public AudioSource audioNormal;
    public AudioSource audioDetected;
    public Image screenFlash;
    public GameObject music;

    private List<Transform> childObjects = new List<Transform>();

    public string InterationPrompt => _prompt;
    public GameObject InteractableGameObject => gameObject;

    private GameObject player;
    PlayerHealth playerHealth;

    private void Start()
    {
        health = maxHealth;
        music = GameObject.Find("Music");
        audioNormal = music.GetComponents<AudioSource>()[0];
        audioDetected = music.GetComponents<AudioSource>()[1];
        screenFlash = GameObject.Find("FlashImage")?.GetComponent<Image>();

        player = GameObject.Find("Capsule");
        playerHealth = player.GetComponent<PlayerHealth>();

        // Get references to all child objects
        foreach (Transform child in enemy.transform)
        {
            childObjects.Add(child);
        }
    }

    public bool Interact(Interactor interactor)
    {
        takeDamage(10.0f);
        return true;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0.0f)
        {
            // Disable child objects
            DisableChildObjects();

            Instantiate(deathEffect, transform.position, transform.rotation);
            if (EnemyVision2.isScreenFlashRunning == true)
            {
                screenFlash.enabled = false;
                EnemyVision2.isScreenFlashRunning = false;
            }
            if (EnemyVision2.NormalAudio == false)
            {
                audioDetected.enabled = false;
                audioNormal.enabled = true;
                EnemyVision2.NormalAudio = true;
            }
            if (Phase1Enemy == true)
            {
                health = maxHealth;
                gameObject.SetActive(true);
                StartCoroutine(RespawnTimer());
            }
        }
    }

    private void DisableChildObjects()
    {
        // Disable each child object
        Respawner.DisableFunctionality(enemy);
        foreach (Transform child in childObjects)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            takeDamage(1.0f);
            playerHealth.currentHealth++;
            Debug.Log("Hit!");
        }
    }        

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(15f);
        Respawner.RespawnEnemy(parentObject);
        Respawner.RespawnEnemy(enemy);
        Respawner.EnableFunctionality(enemy);
    }
}
