using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LobbyScene_UI : UI_Scene
{
    enum Sliders
    {
        EXP_Slider,
    }
    enum Sprites
    {
        Nickname_Sprtie,
        RobotName,
        Hanger_Icon,
        Module_Icon,
        Ach_Icon,
        Emoticon_Icon,
        Rank_Icon, Shop_Icon,
        ExclamationMark,
        ExclamationMark_Icon,
        EXP_Slider,
    }

    enum Buttons
    {
        Community_Btn,
        Hanger_Btn,
        Module_Btn,
        Ach_Btn,
        Emoticon_Btn,
        ChangeRobot_Btn_Prev,
        ChangeRobot_Btn_Next,
        StartGame_Btn,
        Option_Btn,
        Shop_Btn,
        Training_Btn,
        RobotInfo_Btn,
        Profile_Btn,
        ProfilePicture_Btn,
        CustomRoom_Btn,
    }

    enum Labels
    {
        NickName_Label,
        GameStart_Label,
        Training_Label,
        Level_Label,
        CustomRoon_Label,
    }
    private Volt_LobbyRobotViewSection robotViewSection;
    private GameObject startGameBtnOutlieEffect;

    private LobbyScene lobbyScene;

    public override void Init()
    {
        base.Init();
        Bind<UISprite>(typeof(Sprites));
        Bind<UIButton>(typeof(Buttons));
        Bind<UILabel>(typeof(Labels));
        Bind<UISlider>(typeof(Sliders));

        lobbyScene = Managers.Scene.CurrentScene as LobbyScene;

        GetSprite((int)Sprites.ExclamationMark).gameObject.SetActive(false);

        GetButton((int)Buttons.Ach_Btn).onClick.Add(new EventDelegate(() =>
        {
            Managers.UI.ShowSceneUI<AchScene_UI>();
            lobbyScene.SetOffAllRobotCameras();
        }));
        GetButton((int)Buttons.Hanger_Btn).onClick.Add(new EventDelegate(() =>
        {
            Managers.UI.ShowSceneUI<HangarScene_UI>();
        }));

        GetButton((int)Buttons.Module_Btn).onClick.Add(new EventDelegate(() =>
        {
            Managers.UI.ShowSceneUI<ModuleScene_UI>();
            lobbyScene.SetOffAllRobotCameras();
        }));

        GetButton((int)Buttons.Emoticon_Btn).onClick.Add(new EventDelegate(() =>
        {
            Managers.UI.ShowSceneUI<EmoticonScene_UI>();
            lobbyScene.SetOffAllRobotCameras();
        }));

        GetButton((int)Buttons.Option_Btn).onClick.Add(new EventDelegate(() =>
        {
            Managers.UI.ShowPopupUIAsync<SystemOption_Popup>();
        }));

        GetButton((int)Buttons.ChangeRobot_Btn_Next).onClick.Add(new EventDelegate(() =>
        {
            if (!robotViewSection)
                robotViewSection = FindObjectOfType<Volt_LobbyRobotViewSection>();

            robotViewSection.OnClickNextModelBtn();
            ChangeRobotNameLabelToSelectedRobot();
        }));
        GetButton((int)Buttons.ChangeRobot_Btn_Prev).onClick.Add(new EventDelegate(() =>
        {
            if (!robotViewSection)
                robotViewSection = FindObjectOfType<Volt_LobbyRobotViewSection>();

            robotViewSection.OnClickPrevModelBtn();
            ChangeRobotNameLabelToSelectedRobot();
        }));

        GetButton((int)Buttons.Training_Btn).onClick.Add(new EventDelegate(() =>
        {
            Managers.UI.ShowPopupUIAsync<TrainingMap_Popup>();
        }));

        GetButton((int)Buttons.Shop_Btn).onClick.Add(new EventDelegate(() =>
        {
            //TODO: 상점 씬으로 이동~
            PlayerPrefs.SetString("Volt_ShopEnterKey", "Battery");
            Managers.Scene.LoadSceneAsync(Define.Scene.Shop);
        }));

        GetButton((int)Buttons.RobotInfo_Btn).onClick.Add(new EventDelegate(() =>
        {
            Managers.UI.ShowPopupUIAsync<RobotInfo_Popup>();
        }));


        GetButton((int)Buttons.StartGame_Btn).onClick.Add(new EventDelegate(() =>
        {
            Managers.UI.ShowPopupUIAsync<SelectMatch_Popup>();
        }));

        GetButton((int)Buttons.Profile_Btn).onClick.Add(new EventDelegate(() =>
        {
            Managers.UI.ShowPopupUIAsync<MyProfile_Popup>();
        }));

        GetButton((int)Buttons.Community_Btn).onClick.Add(new EventDelegate(() =>
        {
             Managers.UI.ShowPopupUIAsync<Community_Popup>();
            
        }));
        GetButton((int)Buttons.CustomRoom_Btn).onClick.Add(new EventDelegate(() =>
        {
            lobbyScene.SetOffAllRobotCameras();
            Managers.UI.ShowPopupUIAsync<CustomRoom_Popup>();

        }));
        GetButton((int)Buttons.ProfilePicture_Btn).onClick.Add(new EventDelegate(() =>
        {
            Managers.UI.ShowPopupUIAsync<ProfilePictureSetup_Popup>();
        }));

        GetLabel((int)Labels.NickName_Label).text = Volt_PlayerData.instance.NickName;
        RenewExpValue();
        RenewLevel();

        startGameBtnOutlieEffect = GetButton((int)Buttons.StartGame_Btn).transform.Find("LobbyStart_fix").gameObject;
        robotViewSection = FindObjectOfType<Volt_LobbyRobotViewSection>();
        StartCoroutine(DelayedStart());
        
    }

    public void OnClickStartGame()
    {
        Debug.Log("StartGame!");
        //Get<UIWidget>((int)Widgets.InputBlock).gameObject.SetActive(true);
        FindObjectOfType<LobbyScene_AssetUI>().SetOnInputBlock();
        //if (PlayerPrefs.GetInt("Volt_TutorialDone") == 0) //게임 플레이하고 왔는지 분기 나눠야할듯...
        //{
        //    Volt_TutorialManager.S.FindContentsByName("WaitClickGameStartBtn").gameObject.SetActive(false);
        //}
        StartCoroutine(CoStartGame());
    }

    IEnumerator CoStartGame()
    {
        Volt_LobbyRobotViewSection.S.PlayLobbyAnimation();
        yield return new WaitForSeconds(2f);
        Managers.Scene.LoadSceneAsync(Define.Scene.GameScene);
    }

    public override void OnActive()
    {
        //Managers.UI.PushToUILayerStack(this);
        ChangeRobotNameLabelToSelectedRobot();
        if (IsAnyAchHasntBeenRewardedYet())
            ShowACHExclamationMark();
        else
            HideACHExclamationMark();
    }
    private void OnEnable()
    {
        if (GetSprite((int)Sprites.ExclamationMark) == null) return;

        if (IsAnyAchHasntBeenRewardedYet())
            ShowACHExclamationMark();
        else
            HideACHExclamationMark();
    }

    public bool IsAnyAchHasntBeenRewardedYet()
    {

        foreach (var item in Volt_PlayerData.instance.AchievementProgresses)
        {
            if (!item.Value.IsAccomplishACH)
            {
                //Debug.LogError(item.Value.ACHProgressCount + "만큼 달성 " + SystemInfoManager.instance.achConditionInfos[item.Key].condition + " 까지 달성필요");
                if (item.Value.ACHProgressCount >= SystemInfoManager.instance.achConditionInfos[item.Key].condition)
                {
                    //Debug.Log(item.Key.ToString() + "이거 아직 보상 안받았는데?");
                    return true;
                }
            }
        }
        //Debug.Log("보상 다 받았네!");
        return false;
    }

    public void ShowACHExclamationMark()
    {
        GetSprite((int)Sprites.ExclamationMark).gameObject.SetActive(true);
    }

    public void HideACHExclamationMark()
    {
        GetSprite((int)Sprites.ExclamationMark).gameObject.SetActive(false);
    }

    public void ShowStartGameBtnOutline()
    {

        startGameBtnOutlieEffect.SetActive(true);
        
        GetButton((int)Buttons.StartGame_Btn).enabled = true;
    }

    public void HideStartGameBtnOutline()
    {
        startGameBtnOutlieEffect.SetActive(false);
        //TODO: 배터리가 없어 게임을 플레이할 수 없다라고 팝업 띄우는게 좋겠음
        GetButton((int)Buttons.StartGame_Btn).enabled = false;
    }

    private void ChangeRobotNameLabelToSelectedRobot()
    {
        switch (Volt_LobbyRobotViewSection.S.SelectRobotType)
        {
            case RobotType.Volt:
                GetSprite((int)Sprites.RobotName).spriteName = "VOLT";
                break;
            case RobotType.Mercury:
                GetSprite((int)Sprites.RobotName).spriteName = "MERCURY";
                break;
            case RobotType.Hound:
                GetSprite((int)Sprites.RobotName).spriteName = "THE HOUND";
                break;
            case RobotType.Reaper:
                GetSprite((int)Sprites.RobotName).spriteName = "REAPER";
                break;
            default:
                break;
        }
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.2f);
        
        if (IsHaveAccomplishedACH())
            ShowACHExclamationMark();
        else
            HideACHExclamationMark();

        LobbyScene scene = Managers.Scene.CurrentScene as LobbyScene;
        scene.OnLoadedLobbySceneUI();

        
    }

    private bool IsHaveAccomplishedACH()
    {
        //해당 업적의 condition값과 플레이어가 갖고있는 해당업적의 진행도를 대조하여 완료 조건이 달성 되었는지 판단하고, 해당 업적의 보상을 수령하였는지도 판단한다.
        //-> 완료 조건은 달성하였는데 보상을 수령했는지 안했는지 판단해야함.
        for (int i = 0; i < 5; i++)
        {
            int achID = DBManager.instance.daliyACHConditionInfos[i].ID;

            if (DBManager.instance.daliyACHConditionInfos[i].condition == Volt_PlayerData.instance.GetACHProgressCount(achID) && !Volt_PlayerData.instance.IsAccomplishACH(achID))
            {
                //Debug.Log(achID + " 이 완료가 되었지만 보상을 안받았네?");
                return true;
            }
        }
        for (int i = 0; i < 33; i++)
        {
            int achID = DBManager.instance.normalACHConditionInfos[i].ID;

            if (DBManager.instance.normalACHConditionInfos[i].condition == Volt_PlayerData.instance.GetACHProgressCount(achID) && !Volt_PlayerData.instance.IsAccomplishACH(achID))
            {
                //Debug.Log(achID + " 이 완료가 되었지만 보상을 안받았네?");
                return true;
            }
        }
        return false;
    }

    public void RenewExpValue()
    {
        GetSlider((int)Sliders.EXP_Slider).value = (float)Volt_PlayerData.instance.Exp / Volt_PlayerData.instance.MaxExp;
    }
    public void RenewLevel()
    {
        GetLabel((int)Labels.Level_Label).text = $"Lv.{Volt_PlayerData.instance.Level}";
    }
    public void ChangeProfilePicture(string PAID)
    {
        UIButton pictureBtn = GetButton((int)Buttons.ProfilePicture_Btn);
        pictureBtn.normalSprite = PAID;
        pictureBtn.hoverSprite = PAID;
        pictureBtn.pressedSprite = PAID;
        pictureBtn.GetComponent<UISprite>().spriteName = PAID;
        //TODO 프로필사진 변경을 서버에다 저장해야함.
    }

    
}
