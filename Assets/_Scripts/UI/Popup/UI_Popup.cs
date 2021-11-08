using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UIBase
{
    public bool ignoreBackBtn = false;

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
