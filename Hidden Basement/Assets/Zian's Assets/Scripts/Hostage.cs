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

    //Random Dialogue
    public AudioClip[] dialogues;
    private AudioSource audioSource;
    private bool repeatAudio; //Bool to ensure that another dialogue cannot start until the last dialogue is finished

    //Trigger 2nd Phase
    private GameObject trigger;
    TriggerEscapePhase triggerPhase;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        hostage = GameObject.Find("Hostage");
        animator = GetComponent<Animator>();
        animator.SetBool("thankfulAnimation", thankful);
        trigger = GameObject.Find("Trigger2ndPhase");
        triggerPhase = GameObject.Find("Trigger2ndPhase").GetComponent<TriggerEscapePhase>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("thankfulAnimation", thankful);
        if (thankful == true && initialized == false)
        {
            hostage.transform.position = targetPosition.position;
            Quaternion targetRotation = Quaternion.Euler(0f, -90f, 0f);
            hostage.transform.rotation = targetRotation;
            Vector3 scale = new Vector3(1.3f, 1.3f, 1.3f);
            hostage.transform.localScale = scale;
            initialized = true;
        }
    }
    public void PlayRandomDialogue()
    {
        if (repeatAudio == false)
        {
            thankful = true;
            //Move Hostage
            hostage.transform.position = targetPosition.position;
            Quaternion targetRotation = Quaternion.Euler(0f, -90f, 0f);
            hostage.transform.rotation = targetRotation;
            //Play dialogue
            int randomIndex = Random.Range(0, dialogues.Length);
            AudioClip selectedDialogue = dialogues[randomIndex];
            audioSource.clip = selectedDialogue;
            audioSource.Play();
            repeatAudio = true;
            Debug.Log("talking");
            Invoke("SetAudio", audioSource.clip.length + 2f); //After clip length
        }
    }
    private void SetAudio()
    {
        repeatAudio = false;
        Destroy(hostage);
        triggerPhase.Trigger2ndPhase();
    }

}
