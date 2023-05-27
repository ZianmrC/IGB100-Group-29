using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerEscapePhase : MonoBehaviour
{
    public GameObject triggerObject;
    public GameObject Spawner;
    public GameObject escape1;
    public GameObject escape2;
    public TMP_Text objective;
    public GameObject gun;

    public bool secondPhase; //Editable boolean to trigger 2nd phase or not

    void Start()
    {
        if (secondPhase == true)
        {
            Trigger2ndPhase();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == triggerObject.gameObject.name)
        {
            Trigger2ndPhase();
        }
    }
    private void Trigger2ndPhase()
    {
        Spawner.SetActive(true);
        escape1.SetActive(true);
        escape2.SetActive(true);
        objective.text = "Escape!\nFind a random cube and walk into it (Placeholder)";
        gun.SetActive(true);

    }

}
