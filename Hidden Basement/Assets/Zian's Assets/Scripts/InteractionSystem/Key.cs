using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, iInteractable
{
    [SerializeField] private string _prompt;



    public string InterationPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("You have key");
        return true;
    }
}
