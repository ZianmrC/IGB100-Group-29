using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuOpen : MonoBehaviour
{
    public GameObject PageToOpen;
    public GameObject PageToClose;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OpenPage()
    {
        PageToOpen.SetActive(true);
    }
    public void ClosePage()
    {
        PageToClose.SetActive(false);
    }

}
