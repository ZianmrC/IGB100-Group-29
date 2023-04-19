using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iInteractable
{
    public string InterationPrompt { get; }

    public bool Interact(Interactor interactor);
}
