using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueKey : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;



    public string InterationPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var keyItem = GameObject.FindObjectOfType<Inventory>();
        keyItem.HasBlueKey = true;
        ItemCounter.instance.AddKey(1);
        //Inventory.instance.AddItem()
        Destroy(gameObject);
        return true;
    }
}
