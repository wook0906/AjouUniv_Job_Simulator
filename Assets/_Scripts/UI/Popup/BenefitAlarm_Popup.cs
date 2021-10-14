using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenefitAlarm_Popup : UI_Popup
{
    enum Buttons
    {
        Get_Btn,
    }

    enum Labels
    {
        Notice_Label
    }


    public override void Init()
    {
        base.Init();
        //Managers.UI.PushToUILayerStack(this);

        Bind<UIButton>(typeof(Buttons));
        Bind<UILabel>(typeof(Labels));

        GetLabel((int)Labels.Notice_Label).text = Managers.Localization.GetLocalizedValue("BenefitAlarm_Popup_Notice");

        GetButton((int)Buttons.Get_Btn).onClick.Add(new EventDelegate(() =>
        {
            OnClose();
        }));
    }
    public override void OnClose()
    {
        base.OnClose();
        PacketTransmission.SendRequestBenefit(EBenefitType.Pack1Battery);
        ClosePopupUI();
    }
}
