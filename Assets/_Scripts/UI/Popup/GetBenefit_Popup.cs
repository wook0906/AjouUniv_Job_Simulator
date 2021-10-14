using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBenefit_Popup : UI_Popup
{
    enum Buttons
    {
        Confirm_Btn,
    }

    enum Labels
    {
        Notice_Label
    }


    public override void Init()
    {
        base.Init();
        Bind<UIButton>(typeof(Buttons));
        Bind<UILabel>(typeof(Labels));

        //Managers.UI.PushToUILayerStack(this);

        GetButton((int)Buttons.Confirm_Btn).onClick.Add(new EventDelegate(() =>
        {
            OnClose();
        }));

        switch (Managers.Data.CurrentGetBenefitType)
        {
            case EBenefitType.Pack1Battery:
                if (Managers.Data.GetBenefitResult == 0)//성공
                    GetLabel((int)Labels.Notice_Label).text = Managers.Localization.GetLocalizedValue("GetBenefit_Popup_Success_Battery");
                else
                    GetLabel((int)Labels.Notice_Label).text = Managers.Localization.GetLocalizedValue("GetBenefit_Popup_Fail_Battery");
                break;
            default:
                break;
        }
    }
    public override void OnClose()
    {
        base.OnClose();
        ClosePopupUI();
    }
}
