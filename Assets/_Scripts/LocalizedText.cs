using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour {

    public string key;
    private UILabel label;
    // Use this for initialization
    void Start() 
    {
        
    }

    private void OnEnable()
    {
        label = GetComponent<UILabel>();
        ReloadText();
    }

    public void ReloadText()
    {
        if (label != null)
        {
            label.text = Managers.Localization.GetLocalizedValue(key);
        }
    }
}