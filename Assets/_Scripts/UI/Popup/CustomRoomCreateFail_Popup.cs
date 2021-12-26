using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CustomRoomCreateFail_Popup : UI_Popup
{
    enum Buttons
    {
        Confirm_Button,
        Deny_Button,
    }
    enum Labels
    {
        Notice_Label,
    }
    public override void Init()
    {
        base.Init();
        Bind<UIButton>(typeof(Buttons));
        Bind<UILabel>(typeof(Labels));

        GetButton((int)Buttons.Confirm_Button).onClick.Add(new EventDelegate(() =>
        {
            ClosePopupUI();
        }));


        GetLabel((int)Labels.Notice_Label).text = "방 생성에 실패하였습니다.";
    }

}
