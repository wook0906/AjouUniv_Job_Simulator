using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InviteTryResult_Popup : UI_Popup
{
    LobbyScene scene;
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
    public void SetInfo(EInviteMyWaitingRoomResult result, string nickname, int seatIdx)
    {
        scene = Managers.Scene.CurrentScene as LobbyScene;
        switch (result)
        {
            case EInviteMyWaitingRoomResult.Success:
                GetLabel((int)Labels.Notice_Label).text = $"{nickname} 초대 성공";
                scene.customRoomManagement.SetSlotState(seatIdx, nickname, Define.CustomRoomSlotState.WaitPlayer);     
                break;
            case EInviteMyWaitingRoomResult.NotExist:
                GetLabel((int)Labels.Notice_Label).text = "존재하지 않는 플레이어입니다.";
                scene.customRoomManagement.SetEmptySlotState(seatIdx);
                break;
            case EInviteMyWaitingRoomResult.AlreadyPlayUser:
                GetLabel((int)Labels.Notice_Label).text = "이미 다른 게임을 플레이중인 플레이어입니다.";
                scene.customRoomManagement.SetEmptySlotState(seatIdx);
                break;
            case EInviteMyWaitingRoomResult.ExitGame:
                GetLabel((int)Labels.Notice_Label).text = "해당 플레이어가 접속중이지 않습니다.";
                scene.customRoomManagement.SetEmptySlotState(seatIdx);
                break;
            case EInviteMyWaitingRoomResult.AlreadyInvite:
                GetLabel((int)Labels.Notice_Label).text = "이미 초대한 플레이어 입니다.";
                break;
            case EInviteMyWaitingRoomResult.AlreadyEnter:
                GetLabel((int)Labels.Notice_Label).text = "이미 들어와 있는 플레이어 입니다.";
                break;
            default:
                break;
        }

    }

}
