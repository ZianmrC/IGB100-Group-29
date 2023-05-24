using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;


    [SerializeField] private float _interactionPointRadius = 0.5f;

    [SerializeField] private LayerMask _interactableMask;

    public TextMeshProUGUI promptText;
    //[SerializeField] private InteractionPromptUi _interactionPromptUi;

    private readonly Collider[] _colliders = new Collider[4];

    [SerializeField] private int _numFound;

    public GameObject InteractableGameObject => gameObject;

    private IInteractable _interactable;

    private GameObject _interactableGameObject;

    [SerializeField] private LayerMask pickableLayerMask;

    [SerializeField] private Transform playerCameraTransform;

    [SerializeField] [Min(1)] private float hitRange = 3;

    private RaycastHit hit;

    [SerializeField] private GameObject inHandItem;

    [SerializeField] private Transform pickUpParent;

    private void Start()
    {
    }

    private void Update()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);

        if (inHandItem != null)
        {
            return;
        }

        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();

            if(_interactable != null)
            {
                //if (!_interactionPromptUi.IsDisplayed)
                //{
                //   _interactionPromptUi.SetUp(_interactable.InterationPrompt);
                //}
                GameObject interactableObject = (_interactable as Component)?.gameObject;

                if (interactableObject.name == "Ch35_nonPBR" && EnemyVision2.PlayerDetected == false)
                {
                    promptText.text = "Press 'e' to perform stealth takedown";
                }
                else if (interactableObject.name.Contains(" Key"))
                {
                    promptText.text = "Press 'e' to pick up key";
                }
                else if (interactableObject.name == "Door")
                {
                    promptText.text = "Press 'e' to open door";
                }
                else if(interactableObject.name == "Book")
                {
                    promptText.text = "Press 'e' to pick up book";
                }

                if (_interactable != null)
                {
                    if (Keyboard.current.eKey.wasPressedThisFrame)
                    {
                        if (interactableObject.name == "Ch35_nonPBR" && EnemyVision2.PlayerDetected)
                        {
                            return;
                        }
                       // else if(interactableObject.name == "Book_1")
                      //  {
                      //      inHandItem = book1;
                       //     inHandItem.transform.SetParent(pickUpParent.transform, false);
                       //     return;
                       // }
                       // else if (interactableObject.name == "Book_2")
                        //{
                         //   inHandItem = book2;
                         //   inHandItem.transform.SetParent(pickUpParent.transform, false);
                       //     return;
                        //}
                        //else if (interactableObject.name == "Book_3")
                       // {
                       //     inHandItem = book3;
                       //     inHandItem.transform.SetParent(pickUpParent.transform, false);
                        //    return;
                       // }
                        else { _interactable.Interact(this); }
                    }
                }

            }
            else
            {
                if (_interactable != null)
                {
                    _interactable = null;
                }

                //else if (_interactionPromptUi.IsDisplayed)
               // {
                   // _interactionPromptUi.Close();
                //}
            }
        }
        else
        {
            promptText.text = "";
            return;
        }

        //if(Keyboard.current.eKey.wasPressedThisFrame)
        //{
        //     if(hit.collider != null)
        //    {
        //        Debug.Log(hit.collider.name);
        //        if(hit.collider.GetComponent<Item>())
        //        {
        //            Debug.Log("Its a book");
        //            inHandItem = hit.collider.gameObject;
        //            inHandItem.transform.position = Vector3.zero;
        //            inHandItem.transform.rotation = Quaternion.identity;
        //            inHandItem.transform.SetParent(pickUpParent.transform, false);
        //            return;
        //        }
        //    }
        //     else
        //    {
        //        return;
        //    }
        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }

}
