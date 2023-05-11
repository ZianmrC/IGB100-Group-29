using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip normalClip;
    public AudioClip detectedClip;
    public bool loop = true;

    private AudioSource audioSource;
    private EnemyVision enemyVision;
    public bool isDetected;
    private float timeUndetected;

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Set the audio clip and loop settings
        audioSource.clip = normalClip;
        audioSource.loop = loop;

        // Play the audio
        audioSource.Play();

        // Get reference to EnemyVision script
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // Check if the audio has finished playing and loop is enabled
        if (!audioSource.isPlaying && loop)
        {
            // Play the appropriate audio clip based on whether the player is detected or not
            if (isDetected)
            {
                audioSource.clip = detectedClip;
            }
            else
            {
                audioSource.clip = normalClip;
            }

            // Play the audio
            audioSource.Play();
        }

        // Check if the player is detected
        isDetected = enemyVision.isPlayerDetected;

        // Check if the player has been undetected for at least 5 seconds
        if (!isDetected)
        {
            timeUndetected += Time.deltaTime;

            if (timeUndetected >= 5f)
            {
                // Stop the audio
                audioSource.Stop();
            }
        }
        else
        {
            // Reset the time undetected
            timeUndetected = 0f;
        }
        Debug.Log(isDetected);
        */
    }
}
