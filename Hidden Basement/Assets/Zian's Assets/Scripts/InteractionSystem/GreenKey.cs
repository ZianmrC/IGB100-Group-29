using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenKey : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public GameObject InteractableGameObject => gameObject;


    public string InterationPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var keyItem = GameObject.FindObjectOfType<Inventory>();
        keyItem.HasGreenKey = true;
        ItemCounter.instance.AddKey(1);
        //Inventory.instance.AddItem()
        Destroy(gameObject);
        return true;
    }
}
