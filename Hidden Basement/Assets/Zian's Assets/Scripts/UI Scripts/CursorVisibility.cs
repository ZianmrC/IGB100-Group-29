using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorVisibility : MonoBehaviour
{

    public void MakeInvisible()
    {
        Cursor.visible = false;
    }
    public void MakeVisible()
    {
        Cursor.visible = true;
    }
}
