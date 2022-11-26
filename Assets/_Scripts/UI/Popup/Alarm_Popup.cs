using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm_Popup : UI_Popup
{
    enum Labels
    {
        Msg_Label
    }

    enum Buttons
    {
        Ok_Btn
    }

    private bool _isInit;

    public override void Init()
    {
        if (_isInit)
            return;
        _isInit = true;
        Debug.Log("Show Alarm Popup");
        base.Init();

        Bind<UIButton>(typeof(Buttons));
        Bind<UILabel>(typeof(Labels));
    }

    public void AddOkButtonClickEvent(Define.ClickAction action)
    {
        UIButton btn = GetButton((int)Buttons.Ok_Btn);
        if(btn == null)
        {
            Init();
            btn = GetButton((int)Buttons.Ok_Btn);
        }

        switch (action)
        {
            case Define.ClickAction.CloseAllPopup:
                Debug.Log("Close All Popup");
                btn.onClick.Add(new EventDelegate(Managers.UI.CloseAllPopupUI));
                break;
            case Define.ClickAction.ClosePopup:
                Debug.Log("Close Popup");
                btn.onClick.Add(new EventDelegate(ClosePopupUI));
                break;
            default:
                break;
        }
    }

    public void SetMessage(string localizedTextKey)
    {
        Debug.Log($"[Alarm Popup] String Key is {localizedTextKey}");
        UILabel label = GetLabel((int)Labels.Msg_Label);
        if (label == null)
        {
            Init();
            label = GetLabel((int)Labels.Msg_Label);
        }

        LocalizedText localizedText = label.GetComponent<LocalizedText>();
        localizedText.key = localizedTextKey;
        localizedText.ReloadText();
    }
}
