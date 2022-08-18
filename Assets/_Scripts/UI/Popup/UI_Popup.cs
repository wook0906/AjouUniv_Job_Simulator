using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_Popup : UIBase
{
    public bool ignoreBackBtn = false;

    public Action OnOpened;
    public Action OnInit;

    public override void Init()
    {
        isInit = true;
        Managers.UI.SetCanvas(gameObject, true);
        //Managers.UI.SetCanvas(gameObject, false);
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }

}
