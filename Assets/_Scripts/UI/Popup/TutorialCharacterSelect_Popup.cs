
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCharacterSelect_Popup : UI_Popup
{
    enum Labels
    {
        VoltLabel,
        MercuryLabel,
        HoundLabel,
        ReaperLabel,
    }

    enum Buttons
    {
        Volt,
        Mercury,
        Hound,
        Reaper,
    }

    public override void Init()
    {
        base.Init();
        Bind<UILabel>(typeof(Labels));
        Bind<UIButton>(typeof(Buttons));

        TutorialMatchSetup phase = GameController.instance.CurrentPhase as TutorialMatchSetup;

        GetButton((int)Buttons.Volt).onClick.Add(new EventDelegate(() =>
        {
            phase.SelectedRobot = RobotType.Volt;
            Managers.UI.ShowPopupUIAsync<TutorialCharacterSelectConfirm_Popup>();
        }));
        GetButton((int)Buttons.Mercury).onClick.Add(new EventDelegate(() =>
        {
            phase.SelectedRobot = RobotType.Mercury;
            
            Managers.UI.ShowPopupUIAsync<TutorialCharacterSelectConfirm_Popup>();
        }));
        GetButton((int)Buttons.Hound).onClick.Add(new EventDelegate(() =>
        {
            phase.SelectedRobot = RobotType.Hound;
            Managers.UI.ShowPopupUIAsync<TutorialCharacterSelectConfirm_Popup>();
        }));
        GetButton((int)Buttons.Reaper).onClick.Add(new EventDelegate(() =>
        {
            phase.SelectedRobot = RobotType.Reaper;
            Managers.UI.ShowPopupUIAsync<TutorialCharacterSelectConfirm_Popup>();
        }));

        

    }
}
