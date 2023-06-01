using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour //IInteractable
{
    public Transform theDest;

    public bool holding = false;

    [SerializeField] private string _prompt;

    public GameObject InteractableGameObject => gameObject;
    public string InterationPrompt => _prompt;
    
    void OnMouseDown()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = theDest.position;
        this.transform.parent = GameObject.Find("PickUpSlot").transform;
        this.transform.rotation = theDest.rotation;
    }

    void OnMouseUp()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
    }
    /*
    public bool Interact(Interactor interactor)
    {
        if (!holding)
        {
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = theDest.position;
            this.transform.parent = GameObject.Find("PickUpSlot").transform;
            this.transform.rotation = theDest.rotation;
            
            if (holding && Input.GetKeyDown("e"))
            {
                this.transform.parent = null;
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<BoxCollider>().enabled = true;
                holding = false;
            }
            holding = true;
        }

        
        return true;
    }*/
    
}
