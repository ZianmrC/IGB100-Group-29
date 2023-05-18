using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowKey : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public GameObject InteractableGameObject => gameObject;

    public string InterationPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var keyItem = GameObject.FindObjectOfType<Inventory>();
        keyItem.HasYellowKey = true;
        //Inventory.instance.AddItem()
        ItemCounter.instance.AddKey(1);
        Destroy(gameObject);
        return true;
    }
}
