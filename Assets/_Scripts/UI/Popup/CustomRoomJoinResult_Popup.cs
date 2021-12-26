using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CustomRoomJoinResult_Popup : UI_Popup
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
        
    }

    public void SetInfo(EEnterWaitingRoomResult result)
    {
        switch (result)
        {
            case EEnterWaitingRoomResult.Fail:
                GetLabel((int)Labels.Notice_Label).text = "방 입장에 실패하였습니다.";
                break;
            case EEnterWaitingRoomResult.HostOut:
                GetLabel((int)Labels.Notice_Label).text = "방장이 방을 닫았습니다.";
                break;
            case EEnterWaitingRoomResult.Timeout:
                GetLabel((int)Labels.Notice_Label).text = "초대 시간이 만료되었습니다.";
                break;
            default:
                break;
        }
        
    }

}
