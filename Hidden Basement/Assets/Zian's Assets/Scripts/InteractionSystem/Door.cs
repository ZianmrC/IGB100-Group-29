using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    SceneSwitcher sceneSwitcher;

    public string InterationPrompt => _prompt;
    public GameObject text;

    void Start()
    {
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

            // Load the Win screen scene
            var sceneSwitcher = FindObjectOfType<SceneSwitcher>();
            if (sceneSwitcher != null)
            {
                sceneSwitcher.sceneToLoad = "MissionCompleteScreen";
                sceneSwitcher.LoadScene();
            }
            Cursor.visible = true;
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
