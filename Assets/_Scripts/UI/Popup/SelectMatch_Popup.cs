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

        Managers.UI.PushToUILayerStack(this);

        Get<UIButton>((int)Buttons.Exit_Btn).onClick.Add(new EventDelegate(() =>
        {
            OnClose();
        }));
        Get<UIButton>((int)Buttons.MatchForNormal_Btn).onClick.Add(new EventDelegate(() =>
        {
            Managers.UI.GetSceneUI<LobbyScene_UI>().OnClickStartGame();
            OnClose();
        }));


        //TODO 정보세팅

    }
    public override void OnClose()
    {
        base.OnClose();
        ClosePopupUI();
    }
}
