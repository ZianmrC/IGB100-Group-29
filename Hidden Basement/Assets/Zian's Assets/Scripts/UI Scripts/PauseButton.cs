using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private bool isPaused = false; // Flag to track if the game is paused
    public GameObject PausePage;
    public GameObject GameplayUI;

    private void Update()
    {
        // Check for the pause input (e.g., pressing the "P" key)
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Toggle the pause state
            isPaused = !isPaused;

            // Call the Pause() or Resume() method based on the current pause state
            if (isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
            if (PausePage.activeSelf)
            {
                Cursor.visible = false;
                PausePage.SetActive(false);
                GameplayUI.SetActive(true);
                Resume();
            }
            else if (GameplayUI.activeSelf)
            {
                Cursor.visible = true;
                PausePage.SetActive(true);
                GameplayUI.SetActive(false);
            }
        }
    }

    public void Pause()
    {
        // Pause the game
        Time.timeScale = 0f;

        // TODO: Implement any additional pause functionality (e.g., showing a pause menu, disabling player input, etc.)
    }

    public void Resume()
    {
        // Resume the game
        Time.timeScale = 1f;

        // TODO: Implement any additional resume functionality (e.g., hiding the pause menu, enabling player input, etc.)
    }
}
