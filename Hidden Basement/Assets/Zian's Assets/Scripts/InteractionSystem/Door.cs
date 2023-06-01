using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public GameObject InteractableGameObject => gameObject;
    SceneSwitcher sceneSwitcher;

    public string InterationPrompt => _prompt;
    public Text text;
    public TextMeshProUGUI objective;

    public static bool doorUnlocked; //Boolean to represent if the door has been unlocked

    void Start()
    {
        doorUnlocked = false;
        text.gameObject.SetActive(false);
    }
    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<Inventory>();

        if (inventory == null) return false;

        if(inventory.HasBlueKey && inventory.HasRedKey && inventory.HasYellowKey && inventory.HasGreenKey && inventory.HasPurpleKey && inventory.HasBlackKey)
        {
            //_prompt = "Door is unlocked. press E to open";
            Destroy(this.gameObject);
            
            var sceneSwitcher = FindObjectOfType<SceneSwitcher>();
            if (sceneSwitcher != null)
            {
                sceneSwitcher.sceneToLoad = "MissionCompleteScreen";
                sceneSwitcher.LoadScene();
            }
            objective.text = "Free the Hostage";
            doorUnlocked = true;
            return true;

        }
        text.gameObject.SetActive(true);
        text.text = $"Missing {6 - ItemCounter.key} out of 6 keys, keep searching the floors above";
        Invoke("SetActive", 5f);
        return false;
        
    }
    public void SetActive()
    {
        text.gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("test");
        }
    }

}
