using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUpScript : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private TextMeshProUGUI itemNameText;


    private ContextMenuItemAttribute itemBeingPickedUp;

    private void Update()
    {
        //SelectItemBeingPickedUpFromRay();

        //if (HasItemTargetted())
        //{
           // if()
        //}
    }
}
