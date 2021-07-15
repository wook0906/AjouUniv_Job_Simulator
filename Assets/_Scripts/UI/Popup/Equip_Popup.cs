using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip_Popup : UI_Popup
{
    enum Labels
    {
        ConfirmMsg_Label,
        Yes_Label,
        No_Label
    }

    enum Buttons
    {
        Yes_Btn,
        No_Btn,
    }

    enum Sprites
    {
        BG,
        Block
    }

    
    public override void Init()
    {
        base.Init();
        Bind<UILabel>(typeof(Labels));
        Bind<UIButton>(typeof(Buttons));
        Bind<UISprite>(typeof(Sprites));

        //Managers.UI.PushToUILayerStack(this);

        GetButton((int)Buttons.No_Btn).onClick.Add(new EventDelegate(() =>
        {;
            OnClose();
        }));

        GetButton((int)Buttons.Yes_Btn).onClick.Add(new EventDelegate(() =>
        {
            HangarScene_UI hangarUI = FindObjectOfType<HangarScene_UI>();
            hangarUI.ConfirmChangeRobotSkin();
            ClosePopupUI();
        }));
    }
    public override void OnClose()
    {
        base.OnClose();
        ClosePopupUI();
    }
}
