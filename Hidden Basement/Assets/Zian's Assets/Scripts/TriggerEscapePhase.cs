using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerEscapePhase : MonoBehaviour
{
    private GameObject triggerObject;
    public GameObject Spawner;
    public GameObject escape1;
    public GameObject escape2;
    public TMP_Text objective;
    private GameObject textObject;
    public GameObject gun;
    public Text tooltip;
    private Image reticle;

    //Reference Hostage
    private GameObject hostage;
    private Hostage hostageScript;

    public bool secondPhase; //Editable boolean to trigger 2nd phase or not

    void Start()
    {
        hostage = GameObject.Find("Hostage");
        hostageScript = hostage.GetComponent<Hostage>();
        reticle = GameObject.Find("Reticle").GetComponent<Image>();
        triggerObject = GameObject.Find("PlayerCapsule");
        gun = GameObject.Find("M1911 Handgun_Silver");
        if (secondPhase == true)
        {
            Trigger2ndPhase();
            //Debug.Log("test1");
        }
        else
        {
            reticle.enabled = false;
            Spawner.SetActive(false);
            escape1.SetActive(false);
            escape2.SetActive(false);
            objective.text = "Find all 6 Keys scattered across the 2 top floors";
            hostageScript.thankful = false;
            gun.SetActive(false);
            //Debug.Log("test2");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == triggerObject.gameObject.name && Door.doorUnlocked == true)
        {
            Trigger2ndPhase();
        }
        else if(ItemCounter.key <6 || Door.doorUnlocked == false)
        {
            tooltip.gameObject.SetActive(true);
            tooltip.text = "You still need to complete the previous objectives before freeing the hostage";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == triggerObject.gameObject.name && ItemCounter.key < 6)
        {
            DeactivateTooltip();
        }
        else DeactivateTooltip();
    }

    public void Trigger2ndPhase()
    {
        if (secondPhase == false) //If 2ndPhase variable is set to false, invoke code after delay
        {
            Invoke("DelayedExecution", 10f);
            hostageScript.PlayRandomDialogue();
        }
        else
        {
            DelayedExecution();
        }
    }
    private void DeactivateTooltip()
    {
        tooltip.gameObject.SetActive(false);
    }
    void DelayedExecution()
    {
        reticle.enabled = true;
        Spawner.SetActive(true);
        escape1.SetActive(true);
        escape2.SetActive(true);
        objective.text = "Escape!\nFind a random cube and walk into it (Placeholder)";
        gun.SetActive(true);
    }

}
