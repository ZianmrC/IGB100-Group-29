using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Image fadeOverlay; // Reference to the Image component of the FadeOverlay object
    public float fadeDuration = 1.0f; // Duration of the fade-in effect in seconds

    private float currentFadeTime = 0.0f; // Time elapsed since the fade-in effect started

    //Variables to disable once the player dies
    private GameObject playerCamera;
    private GameObject playerCapsule;
    public GameObject gun;
    CharacterController characterController;
    void Start()
    {
        playerCamera = GameObject.Find("PlayerFollowCamera");
        playerCapsule = GameObject.Find("PlayerCapsule");
        characterController = playerCapsule.GetComponent<CharacterController>();
        fadeOverlay.gameObject.SetActive(false);
        currentHealth = maxHealth;
        Cursor.visible = false;
    }
    void Update()
    {
        healthBar.Sethealth(currentHealth);
        if (currentHealth <= 0)
        {
            StartCoroutine(FadeIn());
            Destroy(player);
            switchscene = true;
            // Call the FadeOut method on the SceneTransitionManager script to start the fade-out effect
            sceneTransitionManager.FadeOut();

            // Load the "DeathScreen" scene after the fade-out effect is complete
            StartCoroutine(LoadDeathScreen());
            //Disable Player movement and camera
            characterController.enabled = false;
            playerCamera.SetActive(false);
            gun.SetActive(false);
        }

        // Check if enough time has elapsed since last health regeneration
        timeSinceLastRegen += Time.deltaTime;
        if (timeSinceLastRegen >= healthRegen)
        {
            RegenerateHealth();
            timeSinceLastRegen = 0.0f;
            if( currentHealth > maxHealth) currentHealth = maxHealth;
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    IEnumerator FadeIn()
    {
        // Get the starting alpha value of the fadeOverlay
        float startAlpha = fadeOverlay.color.a;

        // Set the fadeOverlay to active
        fadeOverlay.gameObject.SetActive(true);

        // Loop until the fade duration is reached
        while (currentFadeTime < fadeDuration)
        {
            // Update the current fade time
            currentFadeTime += Time.deltaTime;

            // Calculate the target alpha value using the current fade time and duration
            float targetAlpha = Mathf.Lerp(startAlpha, 1.0f, currentFadeTime / fadeDuration);

            // Update the alpha value of the fadeOverlay's color
            Color fadeColor = fadeOverlay.color;
            fadeColor.a = targetAlpha;
            fadeOverlay.color = fadeColor;

            yield return null;
        }

        // Set the final alpha value to 1.0f to ensure it's fully faded in
        Color finalFadeColor = fadeOverlay.color;
        finalFadeColor.a = 1.0f;
        fadeOverlay.color = finalFadeColor;
    }

    IEnumerator LoadDeathScreen()
    {
        // Wait for the fade-out effect to complete
        yield return new WaitForSeconds(sceneTransitionManager.fadeDuration);

        // Load the "DeathScreen" scene
        SceneManager.LoadScene("DeathScreen");
    }

    public void TakeDamage()
    {
        currentHealth--;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            TakeDamage();
        }
    }
    void RegenerateHealth()
    {
        currentHealth++;
    }
}
