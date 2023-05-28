using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostage : MonoBehaviour
{
    private Animator animator;
    private GameObject hostage;
    public Transform targetPosition; //Position to move hostage once saved
    public bool thankful; //Boolean to initialize thankful animation
    private bool initialized; //Boolean to make sure code is executed only once
    // Start is called before the first frame update
    void Start()
    {
        hostage = GameObject.Find("Hostage");
        animator = GetComponent<Animator>();
        animator.SetBool("thanfulAnimation", thankful);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("thanfulAnimation", thankful);
        if (thankful == true && initialized == false)
        {
            hostage.transform.position = targetPosition.position;
            Quaternion targetRotation = Quaternion.Euler(0f, -90f, 0f);
            hostage.transform.rotation = targetRotation;
            Destroy(hostage, 5f);
            Vector3 scale = new Vector3(1.3f, 1.3f, 1.3f);
            hostage.transform.localScale = scale;
            initialized = true;
        }
    }
}
