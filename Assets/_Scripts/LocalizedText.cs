using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour {

    public string key;
    private UILabel label;
    private bool _isInit = false;

    // Use this for initialization
    void Start() 
    {
        label = GetComponent<UILabel>();
        ReloadText();
        _isInit = true;

        // Noti: UITextList를 사용할 시
        //Label에 바로 텍스트 넣으면 사라짐!!
        if (transform.parent == null)
            return;

        UITextList textList = transform.parent.GetComponent<UITextList>();
        if (textList == null)
            return;

        // 이와같이 텍스트 리스트의 add 함수를 이용해 사용해야함!
        textList.Add(Managers.Localization.GetLocalizedValue(key));  
    }

    private void OnEnable()
    {
        if (_isInit == false)
            return;

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