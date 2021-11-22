using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextListAssist : MonoBehaviour
{
    UITextList textList;

    public string key;

    void Start()
    {
        textList = GetComponent<UITextList>();
        textList.Add(Managers.Localization.GetLocalizedValue(key));
    }

}
