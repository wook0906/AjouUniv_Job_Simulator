using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notice_Popup : UI_Popup
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

        Bind<UIButton>(typeof(Buttons));
        Bind<UILabel>(typeof(Labels));

        GetButton((int)Buttons.Ok_Btn).onClick.Add(new EventDelegate(() =>
        {
            Application.Quit();
        }));
    }
}
