using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip audioClip;
    public bool loop = true;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Set the audio clip and loop settings
        audioSource.clip = audioClip;
        audioSource.loop = loop;

        // Play the audio
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the audio has finished playing and loop is enabled
        if (!audioSource.isPlaying && loop)
        {
            // Play the audio again
            audioSource.Play();
        }
    }
}
