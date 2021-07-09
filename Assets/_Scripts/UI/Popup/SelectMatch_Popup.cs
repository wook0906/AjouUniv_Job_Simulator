using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMatch_Popup : UI_Popup
{
    
    enum Buttons
    {
        Exit_Btn,
        MatchForAI_Btn,
        MatchForNormal_Btn,
    }


    public override void Init()
    {
        base.Init();

        Bind<UIButton>(typeof(Buttons));

        Get<UIButton>((int)Buttons.Exit_Btn).onClick.Add(new EventDelegate(() =>
        {
            ClosePopupUI();
        }));
        Get<UIButton>((int)Buttons.MatchForNormal_Btn).onClick.Add(new EventDelegate(() =>
        {
            Managers.UI.GetSceneUI<LobbyScene_UI>().OnClickStartGame();
        }));


        //TODO 정보세팅

    }
}
