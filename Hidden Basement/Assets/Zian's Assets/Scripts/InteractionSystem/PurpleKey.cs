using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleKey : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public GameObject InteractableGameObject => gameObject;


    public string InterationPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var keyItem = GameObject.FindObjectOfType<Inventory>();
        keyItem.HasPurpleKey = true;
        ItemCounter.instance.AddKey(1);
        //Inventory.instance.AddItem()
        Destroy(gameObject);
        return true;
    }
}
