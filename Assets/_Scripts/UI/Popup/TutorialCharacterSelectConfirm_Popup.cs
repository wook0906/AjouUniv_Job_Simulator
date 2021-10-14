using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCharacterSelectConfirm_Popup : UI_Popup
{
    enum Labels
    {
        CharacterLabel,
    }

    enum Sprites
    {
        CharacterImage,
    }

    enum Buttons
    {
        ConfirmBtn,
        CancelBtn,
    }

    public override void Init()
    {
        base.Init();
        Bind<UILabel>(typeof(Labels));
        Bind<UIButton>(typeof(Buttons));
        Bind<UISprite>(typeof(Sprites));

        TutorialMatchSetup phase = GameController.instance.CurrentPhase as TutorialMatchSetup;

        UISprite characterImage = Get<UISprite>((int)Sprites.CharacterImage);
        UILabel characterLabel = Get<UILabel>((int)Labels.CharacterLabel);
        switch (phase.SelectedRobot)
        {
            case RobotType.Volt:
                characterImage.spriteName = "Volt_Select";
                break;
            case RobotType.Mercury:
                characterImage.spriteName = "Mercury_Select";
                break;
            case RobotType.Hound:
                characterImage.spriteName = "Hound_Select";
                break;
            case RobotType.Reaper:
                characterImage.spriteName = "Reaper_Select";
                break;
            default:
                break;
        }
        characterLabel.text = Managers.Localization.GetLocalizedValue($"TutorialCharacterSelect_Popup_{phase.SelectedRobot}");


        GetButton((int)Buttons.ConfirmBtn).onClick.Add(new EventDelegate(() =>
        {
            Managers.UI.CloseAllPopupUI();
            phase.RobotSelectDone();
        }));
        GetButton((int)Buttons.CancelBtn).onClick.Add(new EventDelegate(() =>
        {
            ClosePopupUI();
        }));
    }
}
