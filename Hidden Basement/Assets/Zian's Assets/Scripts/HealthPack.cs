using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPack : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InterationPrompt => _prompt;
    public GameObject InteractableGameObject => gameObject;
    private GameObject player;
    PlayerHealth playerHealth;
    public GameObject textObject;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Capsule");
        playerHealth = player.GetComponent<PlayerHealth>();
        textObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool Interact(Interactor interactor)
    {
        if (playerHealth.currentHealth == playerHealth.maxHealth)
        {
            text.gameObject.SetActive(true);
            text.text = $"You are already at max health";
            Invoke("SetInactive", 3f);
            return false;
        }
        playerHealth.currentHealth = playerHealth.maxHealth;
        Destroy(gameObject);
        return true;
    }
    private void SetInactive()
    {
        text.gameObject.SetActive(false);
    }
}
