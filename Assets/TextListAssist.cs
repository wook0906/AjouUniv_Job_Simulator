using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextListAssist : MonoBehaviour
{
    UITextList textList;

    [TextArea]
    public string contents;

    void Start()
    {
        textList = GetComponent<UITextList>();
        textList.Add(contents);
    }

}
