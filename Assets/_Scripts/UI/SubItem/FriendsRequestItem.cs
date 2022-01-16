using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class FriendsRequestItem : UIBase
{
    string nickname;

    enum Labels
    {
        NickName_Label,
    }
    enum Buttons
    {
        Deny_Btn,
        Accept_Btn,
    }
    public override void Init()
    {
        transform.SetChildLayer(transform.root.gameObject.layer);

        Bind<UILabel>(typeof(Labels));
        Bind<UIButton>(typeof(Buttons));

        Get<UIButton>((int)Buttons.Deny_Btn).onClick.Add(new EventDelegate(OnClickDenyButton));
        Get<UIButton>((int)Buttons.Accept_Btn).onClick.Add(new EventDelegate(OnClickAcceptButton));
        
    }
    public void SetInfo(string nickname)
    {
        this.nickname = nickname;
        GetLabel((int)Labels.NickName_Label).text = nickname;
    }
    void OnClickAcceptButton()
    {
        PacketTransmission.SendConfirmFriendAddPacket(nickname, 1);
    }
    void OnClickDenyButton()
    {
        PacketTransmission.SendConfirmFriendAddPacket(nickname, 2);
    }

    
}
