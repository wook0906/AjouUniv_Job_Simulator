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
                characterLabel.text = "볼트";
                break;
            case RobotType.Mercury:
                characterImage.spriteName = "Mercury_Select";
                characterLabel.text = "머큐리";
                break;
            case RobotType.Hound:
                characterImage.spriteName = "Hound_Select";
                characterLabel.text = "하운드";
                break;
            case RobotType.Reaper:
                characterImage.spriteName = "Reaper_Select";
                characterLabel.text = "리퍼";
                break;
            default:
                break;
        }

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
