using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBook : MonoBehaviour
{
    public GameObject book;

    public Transform here;
    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == book)
        {
            GetComponent<PickUp>().transform.position = here.position;
        }
    }
    */
}
