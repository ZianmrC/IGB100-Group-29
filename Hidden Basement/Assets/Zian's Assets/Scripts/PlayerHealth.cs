using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    private GameObject player;
    public HealthBarScript healthBar;
    public float healthRegen = 0.1f; // The time intervals for when health is regenerated
    private float timeSinceLastRegen; // Time elapsed since last health regeneration

    public SceneTransitionManager sceneTransitionManager;
    public bool switchscene = false; //Bool to initiate deathscreen transition once true

    // Start is called before the first frame update
    void Awake()
    {
        healthBar = FindObjectOfType<HealthBarScript>();
        player = GameObject.Find("Capsule");
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Debug.Log(currentHealth);
        timeSinceLastRegen = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.Sethealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(player);
            switchscene = true;
            // Call the FadeOut method on the SceneTransitionManager script to start the fade-out effect
            sceneTransitionManager.FadeOut();

            // Load the "DeathScreen" scene after the fade-out effect is complete
            StartCoroutine(LoadDeathScreen());
        }

        // Check if enough time has elapsed since last health regeneration
        timeSinceLastRegen += Time.deltaTime;
        if (timeSinceLastRegen >= healthRegen && currentHealth < maxHealth)
        {
            RegenerateHealth();
            timeSinceLastRegen = 0.0f;
        }
    }
    IEnumerator LoadDeathScreen()
    {
        // Wait for the fade-out effect to complete
        yield return new WaitForSeconds(sceneTransitionManager.fadeDuration);

        // Load the "DeathScreen" scene
        SceneManager.LoadScene("DeathScreen");
    }

    void TakeDamage()
    {
        currentHealth--;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.tag == "EnemyBullet")
        {
            TakeDamage();
            Debug.Log(currentHealth);
        }
    }

    void RegenerateHealth()
    {
        currentHealth++;
        healthBar.Sethealth(currentHealth);
    }
}
