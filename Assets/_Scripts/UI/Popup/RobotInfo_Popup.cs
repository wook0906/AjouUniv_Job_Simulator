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

        Managers.UI.PushToUILayerStack(this);

        Get<UIButton>((int)Buttons.Exit_Btn).onClick.Add(new EventDelegate(()=>
        {
            OnClose();
        }));


        //TODO 정보세팅
        SetInfo((RobotType)PlayerPrefs.GetInt("SELECTED_ROBOT"));
    }

    public void SetInfo(RobotType robotType)
    {
        UILabel slogan = Get<UILabel>((int)Labels.Slogan_Label);
        UILabel companyName = Get<UILabel>((int)Labels.CompanyName_Label);
        UILabel explaination = Get<UILabel>((int)Labels.Explaination_Label);
        UILabel country = Get<UILabel>((int)Labels.CountryName_Label);
        UILabel map = Get<UILabel>((int)Labels.MapName_Label);
        UILabel specificity = Get<UILabel>((int)Labels.SpecificityName_Label);
        UISprite companyIcon = Get<UISprite>((int)Sprites.CompanyIcon);

        switch (robotType)
        {
            case RobotType.Volt:
                slogan.text = "벽을 뛰어넘는 암살자";
                companyName.text = "Nippon Electrics";
                explaination.text = "일본의 에너지 연구 회사";
                country.text = "일본";
                map.text = "Tokyo";
                specificity.text = "사선 기동";
                companyIcon.spriteName = "NipponElectrics";
                break;
            case RobotType.Mercury:
                slogan.text = "나비처럼 날아 벌처럼 쏜다";
                companyName.text = "Vulcan Pizza";
                explaination.text = "호주의 유명한 피자가게";
                country.text = "호주";
                map.text = "Rome";
                specificity.text = "회피 기동";
                companyIcon.spriteName = "VulcanPizza";
                break;
            case RobotType.Hound:
                slogan.text = "최강의 기동력";
                companyName.text = "Max Drift";
                explaination.text = "미국의 레이싱 회사";
                country.text = "미국";
                map.text = "Twin Cities";
                specificity.text = "위치 교환";
                companyIcon.spriteName = "MaxDrift";
                break;
            case RobotType.Reaper:
                slogan.text = "튼튼하고 강한 톱날 사신";
                companyName.text = "Boost Pharma";
                explaination.text = "독일의 유명한 제약회사";
                country.text = "독일";
                map.text = "Ruhrgebiet";
                specificity.text = "쉴드 장치";
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
