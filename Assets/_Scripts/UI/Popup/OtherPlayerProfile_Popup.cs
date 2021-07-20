using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerProfile_Popup : UI_Popup
{
    enum Buttons
    {
        Exit_Btn,
    }
    enum Labels
    {
        StateMSG_Label,
        TotalPlayCount_Label,
        DefeatPlayCount_Label,
        WinPlayCount_Label,
        KillCount_Label,
        DeathCount_Label,
        Nickname_Label,
    }


    public override void Init()
    {
        base.Init();

        Bind<UIButton>(typeof(Buttons));
        Bind<UILabel>(typeof(Labels));

        Get<UIButton>((int)Buttons.Exit_Btn).onClick.Add(new EventDelegate(() =>
        {
            OnClose();
        }));
    }
    public void SetInfo(Define.ProfileData profileData)
    {
        Get<UILabel>((int)Labels.Nickname_Label).text = profileData.nickname;
        Get<UILabel>((int)Labels.TotalPlayCount_Label).text = profileData.totalPlayCnt.ToString();
        Get<UILabel>((int)Labels.DefeatPlayCount_Label).text = profileData.defeatCnt.ToString();
        Get<UILabel>((int)Labels.WinPlayCount_Label).text = profileData.winCnt.ToString();
        Get<UILabel>((int)Labels.KillCount_Label).text = profileData.killCnt.ToString();
        Get<UILabel>((int)Labels.DeathCount_Label).text = profileData.deathCnt.ToString();
        Get<UILabel>((int)Labels.StateMSG_Label).text = profileData.StateMSG.ToString();
    }
    public override void OnClose()
    {
        base.OnClose();
        LobbyScene lobbyScene = Managers.Scene.CurrentScene as LobbyScene;
        lobbyScene.ChangeToLobbyCamera();
        ClosePopupUI();
    }
}
