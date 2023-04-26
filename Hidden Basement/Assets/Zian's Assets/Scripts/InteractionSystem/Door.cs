using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string InterationPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<Inventory>();

        if (inventory == null) return false;

        if(inventory.HasBlueKey && inventory.HasRedKey && inventory.HasYellowKey && inventory.HasGreenKey && inventory.HasPurpleKey && inventory.HasBlackKey)
        {
            //_prompt = "Door is unlocked. press E to open";
            Destroy(gameObject);
            return true;
        }
        Debug.Log("Missing a 'key' item. You better get looking");
        return false;
        
    }
}
