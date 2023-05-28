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
    private GameObject textObject;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        textObject = GameObject.Find("TooltipText");
        text = textObject.GetComponent<Text>();
        player = GameObject.Find("Capsule");
        playerHealth = player.GetComponent<PlayerHealth>();
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
