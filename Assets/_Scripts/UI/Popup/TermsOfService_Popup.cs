using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermsOfService_Popup : UI_Popup
{
    enum Toggles
    {
        TOS_Agreement_Btn,
        PC_Agreement_Btn,
    }

    enum Buttons
    {
        Ok_Btn
    }

    

    public override void Init()
    {
        base.Init();

        Bind<UIToggle>(typeof(Toggles));
        Bind<UIButton>(typeof(Buttons));

    }

    
}
