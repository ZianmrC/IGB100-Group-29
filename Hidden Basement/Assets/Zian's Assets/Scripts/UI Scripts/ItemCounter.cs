using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCounter : MonoBehaviour
{
    public static ItemCounter instance;

    public TMP_Text keyText;
    //public TMP_Text keyCountText;
    public static int key = 0;
    public int keycount = 0;
    private int keyint = 0;

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        key = 0;
        keyint = 0;
        keycount = PlayerPrefs.GetInt("Key " + "/6", 0);
        keyText.text = "Keys " + key.ToString() + "/6";
        //keyCountText.text = "Key: " + keycount.ToString() + "/5";
    }

    public void AddKey(int newKeyValue)
    {
        key += newKeyValue;
        keyText.text = "Keys " + key.ToString() + "/6";
        if (keycount < key)
        {
            PlayerPrefs.SetInt("Key", key);
        }
        keyint++;
        if (keyint == 6)
        {
            keyText.text = "Unlock the Door in the Basement";
        }
    }
}
