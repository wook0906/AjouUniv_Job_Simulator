using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RobotInfo
{
    public string slogan;
    public string companyIconSpriteName;
    public string companyName;
    public string explaination;
    public string country;
    public string countryName;
    public string map;
    public string mapName;
    public string specificity;
    public string specificityName;
}
public class RobotInfo_Popup : UI_Popup
{
    
    enum Labels
    {
        Slogan_Label,
        CompanyName_Label,
        Explaination_Label,
        CountryName_Label,
        MapName_Label,
        SpecificityName_Label
    }

    enum Sprites
    {
        CompanyIcon
    }
    enum Buttons
    {
        Exit_Btn
    }


    public override void Init()
    {
        base.Init();

        Bind<UISprite>(typeof(Sprites));
        Bind<UILabel>(typeof(Labels));
        Bind<UIButton>(typeof(Buttons));

        //Managers.UI.PushToUILayerStack(this);

        Get<UIButton>((int)Buttons.Exit_Btn).onClick.Add(new EventDelegate(()=>
        {
            OnClose();
        }));


        //TODO 정보세팅
        SetInfo((RobotType)PlayerPrefs.GetInt("SELECTED_ROBOT"));
    }

    public void SetInfo(RobotType robotType)
    {
        Get<UILabel>((int)Labels.Slogan_Label).text = Managers.Localization.GetLocalizedValue($"Slogan_{robotType}");
        Get<UILabel>((int)Labels.CompanyName_Label).text = Managers.Localization.GetLocalizedValue($"CompanyName_{robotType}");
        Get<UILabel>((int)Labels.Explaination_Label).text = Managers.Localization.GetLocalizedValue($"Explaination_{robotType}");
        Get<UILabel>((int)Labels.CountryName_Label).text = Managers.Localization.GetLocalizedValue($"Country_{robotType}");
        Get<UILabel>((int)Labels.MapName_Label).text = Managers.Localization.GetLocalizedValue($"Map_{robotType}");
        Get<UILabel>((int)Labels.SpecificityName_Label).text = Managers.Localization.GetLocalizedValue($"Specificity_{robotType}");

        UISprite companyIcon = Get<UISprite>((int)Sprites.CompanyIcon);
        switch (robotType)
        {
            case RobotType.Volt:
                companyIcon.spriteName = "NipponElectrics";
                break;
            case RobotType.Mercury:
                companyIcon.spriteName = "VulcanPizza";
                break;
            case RobotType.Hound:
                companyIcon.spriteName = "MaxDrift";
                break;
            case RobotType.Reaper:
                companyIcon.spriteName = "BoostPharma";
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
