using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Reference to the UI Image representing the screen overlay
    public Image screenOverlay;

    // Color when player has full health
    public Color fullHealthColor = Color.clear;

    // Color when player has low health
    public Color lowHealthColor = Color.red;

    [HideInInspector]
    // Manage Damage Interactions
    public float maxHealth = 5;
    public float currentHealth;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentHealth);
        // Call the UpdateScreen method to update the screen color based on the player's health
        UpdateScreen();
        if (currentHealth <= 0)
        {
            Destroy(player);
        }
    }

    // Remove "public" before the method declaration, and remove the semicolon after the method name
    public void PlayerTakeDamage()
    {
        currentHealth--;
        Debug.Log($"Health is {currentHealth}.");
    }

    // Update the screen color based on the player's health
    void UpdateScreen()
    {
        // Calculate the normalized health value (between 0 and 1)
        float normalizedHealth = (float)currentHealth / 5; // Assuming max health is 5

        // Update the color and alpha value of the screen overlay based on the normalized health value
        Color screenColor = Color.Lerp(lowHealthColor, fullHealthColor, normalizedHealth);
        screenOverlay.color = screenColor;
    }
}
