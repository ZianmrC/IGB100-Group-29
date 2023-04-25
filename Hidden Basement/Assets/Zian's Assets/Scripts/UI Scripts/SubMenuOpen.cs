using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuOpen : MonoBehaviour
{
    public GameObject ControlPage;
    // Start is called before the first frame update
    void Start()
    {
        CloseControlPage();
    }

    public void OpenControlPage()
    {
        ControlPage.SetActive(true);
    }
    public void CloseControlPage()
    {
        ControlPage.SetActive(false);
    }

}
