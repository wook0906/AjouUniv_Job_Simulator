using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsConfirmResult_Popup : UI_Popup
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

    public void SetInfo(string otherPlayerNickname, EConfirmFriendAddResult result)
    {
        this.otherPlayerNickname = otherPlayerNickname;
        switch (result)
        {
            case EConfirmFriendAddResult.Success:
                GetLabel((int)Labels.Notice_Label).text = $"{otherPlayerNickname} 님과 친구가 되었습니다.";
                break;
            case EConfirmFriendAddResult.NotExist:
                GetLabel((int)Labels.Notice_Label).text = $"{otherPlayerNickname} 님은 존재하지 않습니다.";
                break;
            case EConfirmFriendAddResult.DBError:
                GetLabel((int)Labels.Notice_Label).text = $"서버 에러로 인해 {otherPlayerNickname} 님과 친구가 되지 못했습니다.";
                break;
            default:
                break;
        }
       
    }
}
