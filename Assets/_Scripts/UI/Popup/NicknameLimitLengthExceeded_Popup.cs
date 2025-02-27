﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicknameLimitLengthExceeded_Popup : UI_Popup
{
    enum Labels
    {
        Msg_Label,
        Ok_Label
    }

    enum Buttons
    {
        Ok_Btn
    }

    enum Sprites
    {
        Background,
        Block
    }

    public override void Init()
    {
        base.Init();

        Bind<UILabel>(typeof(Labels));
        Bind<UIButton>(typeof(Buttons));
        Bind<UISprite>(typeof(Sprites));

        //Managers.UI.PushToUILayerStack(this);

        Get<UIButton>((int)Buttons.Ok_Btn).onClick.Add(new EventDelegate(() =>
        {
            OnClose();
        }));
    }
    public override void OnClose()
    {
        base.OnClose();
        ClosePopupUI();
    }
}
