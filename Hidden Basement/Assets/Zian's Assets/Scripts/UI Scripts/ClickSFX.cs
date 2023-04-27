using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickSFX : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    private Button button;
    private AudioSource audioSource;

    private void Start()
    {
        // Get the button component and add a listener to the onClick event
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        // Get the AudioSource component and set its clip to the click sound
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clickSound;
    }

    public void OnClick()
    {
        // Play the click sound
        audioSource.Play();
    }
}
