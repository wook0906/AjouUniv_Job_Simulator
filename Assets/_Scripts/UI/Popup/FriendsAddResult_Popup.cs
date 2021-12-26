using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsAddResult_Popup : UI_Popup
{
    string otherPlayerNickname = string.Empty;
    enum Buttons
    {
        Confirm_Button,
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

    public void SetInfo(string otherPlayerNickname, ERequestFriendAddResult result)
    {
        this.otherPlayerNickname = otherPlayerNickname;
        switch (result)
        {
            case ERequestFriendAddResult.Success:
                GetLabel((int)Labels.Notice_Label).text = $"{otherPlayerNickname} 님에게 친구요청을 보냈습니다.";
                break;
            case ERequestFriendAddResult.NotExist:
                GetLabel((int)Labels.Notice_Label).text = $"{otherPlayerNickname} 님은 존재하지 않습니다.";
                break;
            case ERequestFriendAddResult.DBError:
                GetLabel((int)Labels.Notice_Label).text = $"서버 에러로 인해 {otherPlayerNickname} 님과 친구요청을 보내지 못했습니다.";
                break;
            default:
                break;
        }
       
    }
}
