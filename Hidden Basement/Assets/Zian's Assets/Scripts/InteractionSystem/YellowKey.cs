using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowKey : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;



    public string InterationPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var keyItem = GameObject.FindObjectOfType<Inventory>();
        keyItem.HasYellowKey = true;
        //Inventory.instance.AddItem()
        Destroy(gameObject);
        return true;
    }
}