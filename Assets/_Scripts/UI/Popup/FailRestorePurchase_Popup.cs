using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailRestorePurchase_Popup : UI_Popup
{
    enum Buttons
    {
        Ok_Btn
    }

    enum Labels
    {
        Msg_Label
    }

    public override void Init()
    {
        base.Init();
        Bind<UILabel>(typeof(Labels));
        Bind<UIButton>(typeof(Buttons));

        GetButton((int)Buttons.Ok_Btn).onClick.Add(new EventDelegate(() =>
        {
            OnClose();
        }));
    }

    public override void OnClose()
    {
        base.OnClose();
        ClosePopupUI();
    }
}
