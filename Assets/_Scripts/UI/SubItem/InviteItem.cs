using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviteItem : UIBase
{
    string nickName;
    int level;

    enum Labels
    {
        Level_Label,
        NickName_Label
    }
    enum Buttons
    {
        Invite_Btn,
    }
    public override void Init()
    {
        Bind<UILabel>(typeof(Labels));
        Bind<UIButton>(typeof(Buttons));

        GetComponent<UIDragScrollView>().scrollView = transform.parent.parent.GetComponent<UIScrollView>();

        Get<UIButton>((int)Buttons.Invite_Btn).onClick.Add(new EventDelegate(() =>
        {
            OnClickInviteBtn();
        }));
    }
    public void SetInfo(string nickName, int level)
    {
        this.nickName = nickName;
        this.level = level;
        Get<UILabel>((int)Labels.NickName_Label).text = nickName;
        Get<UILabel>((int)Labels.Level_Label).text = ($"Lv.{ level.ToString()}");
    }
    void OnClickInviteBtn()
    {
        LobbyScene scene = Managers.Scene.CurrentScene as LobbyScene;
        scene.customRoomManagement.InvitePlayer(nickName);
    }
}
