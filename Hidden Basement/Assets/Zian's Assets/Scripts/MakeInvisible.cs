using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeInvisible : MonoBehaviour
{
    void Start()
    {
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
