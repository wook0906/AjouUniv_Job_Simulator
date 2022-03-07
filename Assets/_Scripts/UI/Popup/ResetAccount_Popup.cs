using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAccount_Popup : UI_Popup
{
    enum Sprites
    {
        Block,
        BG,
        ResetConfirm
    }

    enum Buttons
    {
        Yes_Btn,
        No_Btn
    }

    enum Labels
    {
        Yes_Label,
        No_Label,
        Guide_Label,
        ResetConfirm_Label
    }

    enum Inputs
    {
        ResetConfirm_Input
    }

    public override void Init()
    {
        base.Init();
        Bind<UISprite>(typeof(Sprites));
        Bind<UIButton>(typeof(Buttons));
        Bind<UILabel>(typeof(Labels));
        Bind<UIInput>(typeof(Inputs));

        //BindSprite<UISprite>(typeof(Sprites));
        //BindSprite<UIButton>(typeof(Buttons));

        //SetButtonSwap(typeof(Buttons));

        GetButton((int)Buttons.No_Btn).onClick.Add(new EventDelegate(() =>
        {
            ClosePopupUI();
        }));

        GetButton((int)Buttons.Yes_Btn).onClick.Add(new EventDelegate(OnClickConfirmResetUsers));
        GetButton((int)Buttons.Yes_Btn).isEnabled = false;
        GetInputField((int)Inputs.ResetConfirm_Input).onChange.Add(new EventDelegate(CompareNickname));
    }
    private void CompareNickname()
    {
        if(GetInputField((int)Inputs.ResetConfirm_Input).label.text == Volt_PlayerData.instance.NickName)
        {
            GetButton((int)Buttons.Yes_Btn).isEnabled = true;
        }
        else
        {
            GetButton((int)Buttons.Yes_Btn).isEnabled = false;
        }
            
    }

    private void OnClickConfirmResetUsers()
    {
        PacketTransmission.SendDeleteUserPacket(Volt_PlayerData.instance.NickName.Length,
            Volt_PlayerData.instance.NickName);
        PlayerPrefs.DeleteAll();
        ClosePopupUI();
        Managers.UI.ShowPopupUIAsync<ResetAccountCompleted_Popup>();
    }
}
