using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoInviteDenyConfirm_Popup : UI_Popup
{
    string otherPlayerNickname = string.Empty;
    enum Buttons
    {
        Confirm_Button,
    }
    enum Labels
    {
        Notice_Label,
        Confirm_Label,
    }
    public override void Init()
    {
        base.Init();
        Bind<UIButton>(typeof(Buttons));
        Bind<UILabel>(typeof(Labels));

        GetLabel((int)Labels.Confirm_Label).text = Managers.Localization.GetLocalizedValue("확인"); //수정 부탁
        GetLabel((int)Labels.Notice_Label).text = Managers.Localization.GetLocalizedValue("초대에 응하기 위해서는 로비에 있어야 합니다.");
        GetButton((int)Buttons.Confirm_Button).onClick.Add(new EventDelegate(() =>
        {
            PacketTransmission.SendJoinWaitingRoomPacket(PlayerPrefs.GetInt("CurInvitedRoomID"), PlayerPrefs.GetInt("CurInvitedRoomSeatIdx"), EJoinWaitingRoomResult.Reject, PlayerPrefs.GetInt("SELECTED_ROBOT"), PlayerPrefs.GetInt($"{(RobotType)PlayerPrefs.GetInt("SELECTED_ROBOT")}_skin"));
            PlayerPrefs.SetInt("CustomRoomMySlotNumber", -1);
            ClosePopupUI();
        }));
    }

}
