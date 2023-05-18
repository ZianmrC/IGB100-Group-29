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
    public GameObject text;
    public GameObject Spawner;
    public TMP_Text objective;
    public GameObject gun;

    //Win Conditions
    public GameObject escape1;
    public GameObject escape2;

    void Start()
    {
        gun.SetActive(false);
        text.SetActive(false);
    }
    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<Inventory>();

        if (inventory == null) return false;

        if(inventory.HasBlueKey && inventory.HasRedKey && inventory.HasYellowKey && inventory.HasGreenKey && inventory.HasPurpleKey && inventory.HasBlackKey)
        {
            //_prompt = "Door is unlocked. press E to open";
            Destroy(gameObject);
            Spawner.SetActive(true);
            escape1.SetActive(true);
            escape2.SetActive(true);
            objective.text = "Escape!\nFind a random cube and walk into it (Placeholder)";
            /*
            var sceneSwitcher = FindObjectOfType<SceneSwitcher>();
            if (sceneSwitcher != null)
            {
                sceneSwitcher.sceneToLoad = "MissionCompleteScreen";
                sceneSwitcher.LoadScene();
            }
            */
            return true;

        }
        text.SetActive(true);
        Invoke("SetInactive", 5f);
        return false;
        
    }
    public void SetInactive()
    {
        text.SetActive(false);
    }
}
