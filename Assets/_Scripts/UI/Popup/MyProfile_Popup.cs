using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyProfile_Popup : UI_Popup
{
    
    enum Buttons
    {
        StateMSGConfirm_Btn,
        Exit_Btn,
    }
    enum Labels
    {
        TotalPlayCount_Label,
        DefeatPlayCount_Label,
        WinPlayCount_Label,
        KillCount_Label,
        DeathCount_Label,
        Nickname_Label,
        StateMSG_Label,
    }


    public override void Init()
    {
        base.Init();

        Bind<UIButton>(typeof(Buttons));
        Bind<UILabel>(typeof(Labels));

        Get<UIButton>((int)Buttons.StateMSGConfirm_Btn).onClick.Add(new EventDelegate(() =>
        {
            //TODO 입력된 상태메세지를 서버에 저장할 수 있어야함.
        }));
        Get<UIButton>((int)Buttons.Exit_Btn).onClick.Add(new EventDelegate(() =>
        {
            OnClose();
        }));

        SetInfo();
    }
    public void SetInfo()
    {
        Get<UILabel>((int)Labels.Nickname_Label).text = Volt_PlayerData.instance.NickName;
        Get<UILabel>((int)Labels.TotalPlayCount_Label).text = Volt_PlayerData.instance.PlayCount.ToString();
        Get<UILabel>((int)Labels.DefeatPlayCount_Label).text = Volt_PlayerData.instance.DeathCount.ToString();
        Get<UILabel>((int)Labels.WinPlayCount_Label).text = Volt_PlayerData.instance.VictoryCount.ToString();
        Get<UILabel>((int)Labels.KillCount_Label).text = Volt_PlayerData.instance.KillCount.ToString();
        Get<UILabel>((int)Labels.DeathCount_Label).text = Volt_PlayerData.instance.DeathCount.ToString();
        Get<UILabel>((int)Labels.StateMSG_Label).text = Volt_PlayerData.instance.StateMSG;
    }
    public override void OnClose()
    {
        base.OnClose();
        LobbyScene lobbyScene = Managers.Scene.CurrentScene as LobbyScene;
        lobbyScene.ChangeToLobbyCamera();
        ClosePopupUI();
    }
}
