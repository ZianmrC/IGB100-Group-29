using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBook : MonoBehaviour
{
    public GameObject book;

    public Transform here;

    public Transform away;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == book)
        {
            other.transform.position = here.position;
            other.transform.rotation = here.rotation;
            other.transform.parent = GameObject.Find("PutBookHere").transform;
            BookFound.instance.AddBook(1);
        }
        else
        {
            other.transform.position = away.position;
        }
    }
}
