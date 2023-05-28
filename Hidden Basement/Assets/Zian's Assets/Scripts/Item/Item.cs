using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject InteractableGameObject => gameObject;
    
    [SerializeField] private GameObject inHandItem;

    [SerializeField] private Transform pickUpParent;

    //public string InterationPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        inHandItem = this.gameObject;
        inHandItem.transform.position = Vector3.zero;
        inHandItem.transform.rotation = Quaternion.identity;
        inHandItem.transform.SetParent(pickUpParent.transform, false);
        return true;
    }
}
