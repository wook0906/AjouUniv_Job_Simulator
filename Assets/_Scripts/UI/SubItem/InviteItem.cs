using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviteItem : UIBase
{
    public string nickName;
    public UIButton inviteButton;
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

        inviteButton = Get<UIButton>((int)Buttons.Invite_Btn);
        inviteButton.onClick.Add(new EventDelegate(() =>
        {
            OnClickInviteBtn();
        }));
    }
    public void SetInfo(Define.ProfileData data)
    {
        this.nickName = data.nickname;
        this.level = data.level;
        Get<UILabel>((int)Labels.NickName_Label).text = nickName;
        Get<UILabel>((int)Labels.Level_Label).text = ($"Lv.{ level.ToString()}");
    }
    void OnClickInviteBtn()
    {
        LobbyScene scene = Managers.Scene.CurrentScene as LobbyScene;
        scene.customRoomManagement.InvitePlayer(nickName);
        GetButton((int)Buttons.Invite_Btn).isEnabled = false;
    }
}
