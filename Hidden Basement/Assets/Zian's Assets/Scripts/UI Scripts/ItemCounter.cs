using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCounter : MonoBehaviour
{

    public TMP_Text keyText;
    //public TMP_Text keyCountText;
    public int key = 0;
    public int keycount = 0;

    private void Start()
    {
        keycount = PlayerPrefs.GetInt("Key " + "/5", 0);
        keyText.text = "Keys " + key.ToString() + "/5";
        //keyCountText.text = "Key: " + keycount.ToString() + "/5";
    }

    public void AddKey(int newKeyValue)
    {
        key += newKeyValue;
        keyText.text = key.ToString();
        if (keycount < key)
        {
            
            PlayerPrefs.SetInt("Key",key);
        }
    }
}
