using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public Image fadeOverlay;
    public float fadeDuration = 1.0f;
    public PlayerHealth playerhealth;

    private void Start()
    {
        // Call the FadeIn method to start with a fade-in effect when the scene starts
        FadeIn();
        playerhealth = FindObjectOfType<PlayerHealth>();
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1.0f, 0.0f));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0.0f, 1.0f));
    }

    private IEnumerator Fade(float startAlpha, float targetAlpha)
    {
        if (playerhealth.switchscene == true)
        {
            // Set the initial alpha value of the fade overlay
            fadeOverlay.color = new Color(fadeOverlay.color.r, fadeOverlay.color.g, fadeOverlay.color.b, startAlpha);

            // Smoothly animate the fade effect over time
            float elapsedTime = 0.0f;
            while (elapsedTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
                fadeOverlay.color = new Color(fadeOverlay.color.r, fadeOverlay.color.g, fadeOverlay.color.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Set the final alpha value of the fade overlay
            fadeOverlay.color = new Color(fadeOverlay.color.r, fadeOverlay.color.g, fadeOverlay.color.b, targetAlpha);

            // Load the new scene or perform any other actions after the fade effect is complete
            if (targetAlpha == 0.0f)
            {
                // Fade out completed, load the new scene
                SceneManager.LoadScene("DeathScreen");
            }
        }
    }
}
