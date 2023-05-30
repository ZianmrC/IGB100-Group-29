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
            if (book != null && book.TryGetComponent<PickUp>(out PickUp pickUp))
            {
                pickUp.transform.position = here.position;
                pickUp.transform.rotation = here.rotation;
                BookFound.instance.AddBook(1);
                pickUp.transform.parent = here.parent;
            }
            //GetComponent<PickUp>().transform.position = here.position;
        }
        else
        {
            if (gameObject != null && away != null)
            {
                gameObject.transform.position = away.position;
            }
            //gameObject.transform.position = away.position;
        }
    }
    
}
