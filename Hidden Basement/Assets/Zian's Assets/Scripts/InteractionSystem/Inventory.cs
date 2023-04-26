using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Inventory : MonoBehaviour
{
    public bool HasBlueKey = false;
    public bool HasRedKey = false;
    public bool HasYellowKey = false;
    public bool HasGreenKey = false;
    public bool HasPurpleKey = false;
    public bool HasBlackKey = false;

    private void Update()
    {
        //if (Keyboard.current.qKey.wasPressedThisFrame) HasKey = !HasKey;
    }
}
