using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile_Popup : UI_Popup
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
    }


    public override void Init()
    {
        base.Init();

        Bind<UIButton>(typeof(Buttons));

        Get<UIButton>((int)Buttons.StateMSGConfirm_Btn).onClick.Add(new EventDelegate(() =>
        {
            //TODO 입력된 상태메세지를 서버에 저장할 수 있어야함.
        }));
        Get<UIButton>((int)Buttons.Exit_Btn).onClick.Add(new EventDelegate(() =>
        {
            ClosePopupUI();
        }));
    }
    public void SetInfo(int totalPlayCnt, int defeatCnt, int winCnt, int killCnt, int deathCnt)
    {
        Get<UILabel>((int)Labels.TotalPlayCount_Label).text = totalPlayCnt.ToString();
        Get<UILabel>((int)Labels.DefeatPlayCount_Label).text = deathCnt.ToString();
        Get<UILabel>((int)Labels.WinPlayCount_Label).text = winCnt.ToString();
        Get<UILabel>((int)Labels.KillCount_Label).text = killCnt.ToString();
        Get<UILabel>((int)Labels.DeathCount_Label).text = deathCnt.ToString();
    }
}
