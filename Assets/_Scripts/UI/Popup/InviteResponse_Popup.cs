using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviteResponse_Popup : UI_Popup
{
    int roomID;
    string hostName;
    int seatIdx;
    enum Buttons
    {
        Accept_Button,
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

        //LobbyScene scene = Managers.Scene.

        GetButton((int)Buttons.Accept_Button).onClick.Add(new EventDelegate(() =>
        {
            
            PacketTransmission.SendJoinWaitingRoomPacket(roomID, seatIdx, EJoinWaitingRoomResult.Ok, PlayerPrefs.GetInt("SELECTED_ROBOT"), PlayerPrefs.GetInt($"{(RobotType)PlayerPrefs.GetInt("SELECTED_ROBOT")}_skin"));
            ClosePopupUI();
        })); 
        GetButton((int)Buttons.Deny_Button).onClick.Add(new EventDelegate(() =>
        {
            PacketTransmission.SendJoinWaitingRoomPacket(roomID, seatIdx, EJoinWaitingRoomResult.Reject, PlayerPrefs.GetInt("SELECTED_ROBOT"), PlayerPrefs.GetInt($"{(RobotType)PlayerPrefs.GetInt("SELECTED_ROBOT")}_skin"));
            ClosePopupUI();
        }));
    }

    public void SetInfo(int roomID, string hostName, int seatIdx)
    {
        this.roomID = roomID;
        this.hostName = hostName;
        this.seatIdx = seatIdx;
        LobbyScene scene = Managers.Scene.CurrentScene as LobbyScene;
        scene.customRoomManagement.mySlotNumber = seatIdx;
        GetLabel((int)Labels.Notice_Label).text = $"{hostName} 님이 초대하셨습니다.";
    }
}
