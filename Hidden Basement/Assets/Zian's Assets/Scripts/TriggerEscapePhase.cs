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

    public bool secondPhase; //Editable boolean to trigger 2nd phase or not

    void Start()
    {
        reticle = GameObject.Find("Reticle").GetComponent<Image>();
        triggerObject = GameObject.Find("PlayerCapsule");
        gun = GameObject.Find("M1911 Handgun_Silver");
        if (secondPhase == true)
        {
            Trigger2ndPhase(true);
        }
        else
        {
            Trigger2ndPhase(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == triggerObject.gameObject.name && Door.doorUnlocked == true)
        {
            Trigger2ndPhase(true);
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
    private void Trigger2ndPhase(bool yesorno)
    {
        reticle.enabled = yesorno;
        Spawner.SetActive(yesorno);
        escape1.SetActive(yesorno);
        escape2.SetActive(yesorno);
        objective.text = "Escape!\nFind a random cube and walk into it (Placeholder)";
        gun.SetActive(yesorno);

    }
    private void DeactivateTooltip()
    {
        tooltip.gameObject.SetActive(false);
    }

}
