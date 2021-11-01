using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ACHItem : UIBase
{
    enum Labels
    {
        Title_Label,
        ConditionCount_Label,
        Description_Label,
        //RewardAssetCount_Label,
        ButtonName_Label
    }

    enum Sprites
    {
        //RewardAsset_Sprite
    }

    enum Buttons
    {
        Reward_Button
    }

    private int ID;

    //private EAssetsType rewardType;
    //private int rewardCount;
    private Define.ACHReward rewardInfo;
    private int conditionType;
    private int conditionCount;
    private string rewardICON;

    private string getRewardButtonActiveSprite;
    private string getRewardButtonUnActiveSprite;
    private int titleFontSize;
    private int descriptionFontSize;
    private int rewardCountFontSize;
    private int conditionCountFontSize;

    private UIPanel rewardSpriteAnimationRoot;

    public List<ACHRewardItem> achItemList = new List<ACHRewardItem>();
    private int rewarItemOffset = -465;


    public override void Init()
    {
        Bind<UILabel>(typeof(Labels));
        Bind<UISprite>(typeof(Sprites));
        Bind<UIButton>(typeof(Buttons));

        GetComponent<UIDragScrollView>().scrollView = Util.FindParent<UIScrollView>(gameObject, null, true);
    }
    #region deprecated
    //public void Init(int id, string title_KR, string title_EN, string title_GER,
    //    string title_Fren, string description_KR, string description_EN, string description_GER,
    //    string description_Fren, int reward, int rewardCount, int conditionType,
    //    int conditionCount, string rewardICON, string progressButtonName_KR,
    //    string progressButtonName_EN, string progressButtonName_GER, string progressButtonName_Fren,
    //    string getRewardButtonName_KR, string getRewardButtonName_EN, string getRewardButtonName_GER,
    //    string getRewardButtonName_Fren, string getRewardButtonActiveSprite, string getRewardButtonUnActiveSprite,
    //    int titleFontSize, int descriptionFontSize,
    //    int rewardCountFontSize, int conditionCountFontSize)
    //{
    //    this.ID = id;
    //    this.title_KR = title_KR;
    //    this.title_EN = title_EN;
    //    this.title_GER = title_GER;
    //    this.title_Fren = title_Fren;
    //    this.description_KR = description_KR;
    //    this.description_EN = description_EN;
    //    this.description_GER = description_GER;
    //    this.description_Fren = description_Fren;
    //    this.rewardType = (EAssetsType)reward;
    //    this.rewardCount = rewardCount;
    //    this.conditionType = conditionType;
    //    this.conditionCount = conditionCount;
    //    this.rewardICON = rewardICON;
    //    this.progressButtonName_KR = progressButtonName_KR;
    //    this.progressButtonName_EN = progressButtonName_EN;
    //    this.progressButtonName_GER = progressButtonName_GER;
    //    this.progressButtonName_Fren = progressButtonName_Fren;
    //    this.getRewardButtonName_KR = getRewardButtonName_KR;
    //    this.getRewardButtonName_EN = getRewardButtonName_EN;
    //    this.getRewardButtonName_GER = getRewardButtonName_GER;
    //    this.getRewardButtonName_Fren = getRewardButtonName_Fren;
    //    this.getRewardButtonActiveSprite = getRewardButtonActiveSprite;
    //    this.getRewardButtonUnActiveSprite = getRewardButtonUnActiveSprite;
    //    this.titleFontSize = titleFontSize;
    //    this.descriptionFontSize = descriptionFontSize;
    //    this.rewardCountFontSize = rewardCountFontSize;
    //    this.conditionCountFontSize = conditionCountFontSize;


    //    GetLabel((int)Labels.Title_Label).text = Managers.Localization.GetLocalizedValue($"Achievement{this.ID}_Title");
    //    GetLabel((int)Labels.Description_Label).text = Managers.Localization.GetLocalizedValue($"Achievement{this.ID}_Description");


    //    GetLabel((int)Labels.RewardAssetCount_Label).text = this.rewardCount.ToString();

    //    //Debug.Log("Atlas path: " + this.atlas);
    //    GetSprite((int)Sprites.RewardAsset_Sprite).spriteName = this.rewardICON;
    //    GetSprite((int)Sprites.RewardAsset_Sprite).color = Color.yellow;

    //    int userACHCount = Volt_PlayerData.instance.GetACHProgressCount(this.ID);
    //    userACHCount = Mathf.Min(userACHCount, this.conditionCount);

    //    float percentage = (float)userACHCount / this.conditionCount;
    //    GetLabel((int)Labels.ConditionCount_Label).text = userACHCount + "/" + this.conditionCount.ToString() + "(" +
    //        percentage.ToString("P0") + ")";
    //    GetLabel((int)Labels.ConditionCount_Label).fontSize = this.conditionCountFontSize;
    //    GetLabel((int)Labels.Description_Label).fontSize = this.descriptionFontSize;
    //    GetLabel((int)Labels.RewardAssetCount_Label).fontSize = this.rewardCountFontSize;
    //    GetLabel((int)Labels.Title_Label).fontSize = this.titleFontSize;

    //    EventDelegate eventDelegate = new EventDelegate(this, "OnClickGetRewardButton");
    //    eventDelegate.parameters[0] = Util.MakeParameter(Get<UISprite>((int)Sprites.RewardAsset_Sprite), typeof(UISprite));

    //    switch (this.rewardType)
    //    {
    //        case EAssetsType.Gold:
    //            Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/UI_Sounds/GetGoldReward.wav",
    //                (result) =>
    //                {
    //                    GetButton((int)Buttons.Reward_Button).GetComponent<UIPlaySound>().audioClip = result.Result;
    //                });
    //            break;
    //        case EAssetsType.Diamond:
    //            Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/UI_Sounds/GetDiamondReward.wav",
    //                (result) =>
    //                {
    //                    GetButton((int)Buttons.Reward_Button).GetComponent<UIPlaySound>().audioClip = result.Result;
    //                });
    //            break;
    //        default:
    //            Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/UI_Sounds/GetReward.wav",
    //                (result) =>
    //                {
    //                    GetButton((int)Buttons.Reward_Button).GetComponent<UIPlaySound>().audioClip = result.Result;
    //                });
    //            break;
    //    }
    //    GetButton((int)Buttons.Reward_Button).onClick.Add(eventDelegate);

    //    SetRewardButton(userACHCount);
    //    rewardSpriteAnimationRoot = GameObject.Find("AchScene_UI/Black_BG1/RewardSpriteAnimationRoot").GetComponent<UIPanel>();
    //}
    #endregion

    public void Init(int id, string title_KR, string title_EN, string title_GER,
        string title_Fren, string description_KR, string description_EN, string description_GER,
        string description_Fren, Define.ACHReward rewardInfo, int conditionType,
        int conditionCount, string rewardICON, string progressButtonName_KR,
        string progressButtonName_EN, string progressButtonName_GER, string progressButtonName_Fren,
        string getRewardButtonName_KR, string getRewardButtonName_EN, string getRewardButtonName_GER,
        string getRewardButtonName_Fren, string getRewardButtonActiveSprite, string getRewardButtonUnActiveSprite,
        int titleFontSize, int descriptionFontSize,
        int rewardCountFontSize, int conditionCountFontSize)
    {
        this.ID = id;
        
        this.rewardInfo = rewardInfo;
        this.conditionType = conditionType;
        this.conditionCount = conditionCount;
        this.rewardICON = rewardICON;
      
        this.getRewardButtonActiveSprite = getRewardButtonActiveSprite;
        this.getRewardButtonUnActiveSprite = getRewardButtonUnActiveSprite;
        this.titleFontSize = titleFontSize;
        this.descriptionFontSize = descriptionFontSize;
        this.rewardCountFontSize = rewardCountFontSize;
        this.conditionCountFontSize = conditionCountFontSize;

        GetLabel((int)Labels.Title_Label).text = Managers.Localization.GetLocalizedValue($"Achievement{this.ID}_Title");
        GetLabel((int)Labels.Description_Label).text = Managers.Localization.GetLocalizedValue($"Achievement{this.ID}_Description");

        //GetLabel((int)Labels.RewardAssetCount_Label).text = this.rewardCount.ToString();
        //Debug.Log("Atlas path: " + this.atlas);

        StartCoroutine(CorSetRewardInfoItem(rewardInfo));

        int userACHCount = Volt_PlayerData.instance.GetACHProgressCount(this.ID);
        userACHCount = Mathf.Min(userACHCount, this.conditionCount);

        float percentage = (float)userACHCount / this.conditionCount;
        GetLabel((int)Labels.ConditionCount_Label).text = userACHCount + "/" + this.conditionCount.ToString() + "(" +
            percentage.ToString("P0") + ")";
        GetLabel((int)Labels.ConditionCount_Label).fontSize = this.conditionCountFontSize;
        GetLabel((int)Labels.Description_Label).fontSize = this.descriptionFontSize;
        //GetLabel((int)Labels.RewardAssetCount_Label).fontSize = this.rewardCountFontSize;
        GetLabel((int)Labels.Title_Label).fontSize = this.titleFontSize;

        //EventDelegate eventDelegate = new EventDelegate(this, "OnClickGetRewardButton");
        //eventDelegate.parameters[0] = Util.MakeParameter(Get<UISprite>((int)Sprites.RewardAsset_Sprite), typeof(UISprite));

        //switch (this.rewardType)
        //{
        //    case EAssetsType.Gold:
        //        Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/UI_Sounds/GetGoldReward.wav",
        //            (result) =>
        //            {
        //                GetButton((int)Buttons.Reward_Button).GetComponent<UIPlaySound>().audioClip = result.Result;
        //            });
        //        break;
        //    case EAssetsType.Diamond:
        //        Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/UI_Sounds/GetDiamondReward.wav",
        //            (result) =>
        //            {
        //                GetButton((int)Buttons.Reward_Button).GetComponent<UIPlaySound>().audioClip = result.Result;
        //            });
        //        break;
        //    default:
        //        Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/UI_Sounds/GetReward.wav",
        //            (result) =>
        //            {
        //                GetButton((int)Buttons.Reward_Button).GetComponent<UIPlaySound>().audioClip = result.Result;
        //            });
        //        break;
        //}
        GetButton((int)Buttons.Reward_Button).onClick.Add(new EventDelegate(()=>OnClickGetRewardButton()));

        SetRewardButton(userACHCount);
        rewardSpriteAnimationRoot = GameObject.Find("AchScene_UI/Black_BG1/RewardSpriteAnimationRoot").GetComponent<UIPanel>();
    }

    IEnumerator CorSetRewardInfoItem(Define.ACHReward rewardInfo)
    {
        Vector3 pos = Vector3.zero;
        pos.y -= 100;
        if (rewardInfo.gold > 0)
        {
            AsyncOperationHandle<GameObject> handle = Managers.UI.MakeSubItemAsync<ACHRewardItem>(this.transform);
            yield return new WaitUntil(() => { return handle.IsDone; });
            handle.Result.transform.localScale = Vector3.one;
            handle.Result.transform.localPosition = Vector3.zero;
            ACHRewardItem item = handle.Result.GetComponent<ACHRewardItem>();
            item.SetInfo(EShopPurchase.Gold, rewardInfo.gold.ToString());
            if (achItemList.Count == 0)
            {
                pos.x = rewarItemOffset;
                item.transform.localPosition = pos;
            }
            else
            {
                item.transform.localPosition = achItemList[achItemList.Count - 1].transform.localPosition;
                pos.x += achItemList[achItemList.Count - 1].GetComponent<UISprite>().width / 2 + item.GetComponent<UISprite>().width / 2;
                item.transform.localPosition = pos;
            }
            achItemList.Add(item);

            yield return new WaitUntil(() => item.isInit);
        }

        if (rewardInfo.diamond > 0)
        {
            AsyncOperationHandle<GameObject> handle = Managers.UI.MakeSubItemAsync<ACHRewardItem>(this.transform);
            yield return new WaitUntil(() => { return handle.IsDone; });
            handle.Result.transform.localScale = Vector3.one;
            handle.Result.transform.localPosition = Vector3.zero;
            ACHRewardItem item = handle.Result.GetComponent<ACHRewardItem>();
            item.SetInfo(EShopPurchase.Diamond, rewardInfo.diamond.ToString());
            if (achItemList.Count == 0)
            {
                pos.x = rewarItemOffset;
                item.transform.localPosition = pos;
            }
            else
            {
                item.transform.localPosition = achItemList[achItemList.Count - 1].transform.localPosition;
                pos.x += achItemList[achItemList.Count - 1].GetComponent<UISprite>().width / 2 + item.GetComponent<UISprite>().width / 2;
                item.transform.localPosition = pos;
            }
            achItemList.Add(item);
            yield return new WaitUntil(() => item.isInit);
        }
        if (rewardInfo.battery > 0)
        {
            AsyncOperationHandle<GameObject> handle = Managers.UI.MakeSubItemAsync<ACHRewardItem>(this.transform);
            yield return new WaitUntil(() => { return handle.IsDone; });
            handle.Result.transform.localScale = Vector3.one;
            handle.Result.transform.localPosition = Vector3.zero;
            ACHRewardItem item = handle.Result.GetComponent<ACHRewardItem>();
            item.SetInfo(EShopPurchase.Battery, rewardInfo.battery.ToString());
            if (achItemList.Count == 0)
            {
                pos.x = rewarItemOffset;
                item.transform.localPosition = pos;
            }
            else
            {
                item.transform.localPosition = achItemList[achItemList.Count - 1].transform.localPosition;
                pos.x += achItemList[achItemList.Count - 1].GetComponent<UISprite>().width / 2 + item.GetComponent<UISprite>().width / 2;
                item.transform.localPosition = pos;
            }
            achItemList.Add(item);
            yield return new WaitUntil(() => item.isInit);
        }

        if (rewardInfo.skinType != 0)
        {
            AsyncOperationHandle<GameObject> handle = Managers.UI.MakeSubItemAsync<ACHRewardItem>(this.transform);
            yield return new WaitUntil(() => { return handle.IsDone; });
            handle.Result.transform.localScale = Vector3.one;
            handle.Result.transform.localPosition = Vector3.zero;
            ACHRewardItem item = handle.Result.GetComponent<ACHRewardItem>();
            item.SetInfo(EShopPurchase.Skin, rewardInfo.skinType.ToString());
            if (achItemList.Count == 0)
            {
                pos.x = rewarItemOffset;
                item.transform.localPosition = pos;
            }
            else
            {
                item.transform.localPosition = achItemList[achItemList.Count - 1].transform.localPosition;
                pos.x += achItemList[achItemList.Count - 1].GetComponent<UISprite>().width / 2 + item.GetComponent<UISprite>().width / 2;
                item.transform.localPosition = pos;
            }
            achItemList.Add(item);
            yield return new WaitUntil(() => item.isInit);
        }
    }
    public bool IsAllInit()
    {
        foreach (var item in achItemList)
        {
            if (!item.isInit)
                return false;
        }
        return true;
    }
    private void OnClickGetRewardButton()//(UISprite sprite)
    {
        Debug.Log("OnClick Reward Button");
        //switch (rewardType)
        //{
        //    case EAssetsType.Gold:
        //    case EAssetsType.Diamond:
        //        Managers.Resource.InstantiateAsync("SpriteAnimator.prefab", (result) =>
        //        {
        //            Debug.Log("Complete Instantiate SpriteAnimator");
        //            GameObject go = result.Result;
        //            go.transform.SetParent(rewardSpriteAnimationRoot.transform);
        //            go.transform.localScale = Vector3.one;
        //            go.transform.position = sprite.transform.position;

        //            switch (this.rewardType)
        //            {
        //                case EAssetsType.Gold:
        //                    go.GetComponent<SpriteAnimator>().SetSpriteFrames(SpriteFrames.GoldFrames);
        //                    break;
        //                case EAssetsType.Diamond:
        //                    go.GetComponent<SpriteAnimator>().SetSpriteFrames(SpriteFrames.DiamondFrames);
        //                    break;
        //                default:
        //                    break;
        //            }

        //        });
        //        break;
        //    default:
        //        break;
        //}
        PacketTransmission.SendAchivementCompletionPacket(ID);
    }

    public void SetUserConditionCount(int count, bool isAccomplish)
    {
        if (count < 0)
        {
            //Debug.LogWarning("[SetUserConditionCount] count below 0!!");
            return;
        }
        count = Mathf.Min(count, conditionCount);

        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.Append(count.ToString());
        builder.Append("/");
        builder.Append(this.conditionCount.ToString());
        int percentage = Mathf.RoundToInt((float)count / this.conditionCount * 100f);
        builder.Append("(" + percentage + "%)");

        SetRewardButton(count);

        GetLabel((int)Labels.ConditionCount_Label).text = builder.ToString();

        if (isAccomplish)
        {
            GetButton((int)Buttons.Reward_Button).enabled = false;

        }
    }

    public int GetID() { return ID; }

    public void ACHComplete()
    {
        Volt_PlayerData.instance.AchievementProgresses[ID].OnAccomplish();

        GetButton((int)Buttons.Reward_Button).GetComponent<UISprite>().spriteName = this.getRewardButtonActiveSprite;
        GetButton((int)Buttons.Reward_Button).normalSprite = this.getRewardButtonUnActiveSprite;
        GetButton((int)Buttons.Reward_Button).pressedSprite = null;
        GetButton((int)Buttons.Reward_Button).enabled = false;
        GetLabel((int)Labels.ButtonName_Label).text = Managers.Localization.GetLocalizedValue("Achievement_Completed");

        //switch (Application.systemLanguage)
        //{
        //    case SystemLanguage.French:
        //        break;
        //    case SystemLanguage.German:
        //        GetLabel((int)Labels.ButtonName_Label).text = "Fertig";
        //        break;
        //    case SystemLanguage.Korean:
        //        GetLabel((int)Labels.ButtonName_Label).text = "완료";
        //        break;
        //    default:
        //        GetLabel((int)Labels.ButtonName_Label).text = "Done";
        //        break;

        //}
        Color color = GetComponent<UISprite>().color;
        color.a = 0.5f;
        GetComponent<UISprite>().color = color;

       // Debug.Log("보상을 받는다요");

    }

    private void SetRewardButton(int userACHCount)
    {
        if (userACHCount >= this.conditionCount)
        {
            GetLabel((int)Labels.ButtonName_Label).text = Managers.Localization.GetLocalizedValue("Achievement_GetReward");

            //switch (Application.systemLanguage)
            //{
            //    case SystemLanguage.Korean:
            //        GetLabel((int)Labels.ButtonName_Label).text = this.getRewardButtonName_KR;
            //        break;
            //    case SystemLanguage.German:
            //        GetLabel((int)Labels.ButtonName_Label).text = this.getRewardButtonName_GER;
            //        break;
            //    case SystemLanguage.French:
            //        GetLabel((int)Labels.ButtonName_Label).text = this.getRewardButtonName_Fren;
            //        break;
            //    default:
            //        GetLabel((int)Labels.ButtonName_Label).text = this.getRewardButtonName_EN;
            //        break;
            //}
            if (Volt_PlayerData.instance.IsAccomplishACH(this.ID)) // 보상 수령함!
            {
                //Debug.Log($"{this.ID}이미 보상 수령했네");
                GetButton((int)Buttons.Reward_Button).GetComponent<UISprite>().spriteName = this.getRewardButtonActiveSprite;
                GetButton((int)Buttons.Reward_Button).normalSprite = this.getRewardButtonUnActiveSprite;
                GetButton((int)Buttons.Reward_Button).pressedSprite = null;
                GetButton((int)Buttons.Reward_Button).enabled = false;
                GetLabel((int)Labels.ButtonName_Label).text = Managers.Localization.GetLocalizedValue("Achievement_Completed");

                //switch (Application.systemLanguage)
                //{
                //    case SystemLanguage.French:
                //        GetLabel((int)Labels.ButtonName_Label).text = "Done";
                //        break;
                //    case SystemLanguage.German:
                //        GetLabel((int)Labels.ButtonName_Label).text = "Fertig";
                //        break;
                //    case SystemLanguage.Korean:
                //        GetLabel((int)Labels.ButtonName_Label).text = "완료";
                //        break;
                //    default:
                //        GetLabel((int)Labels.ButtonName_Label).text = "Done";
                //        break;

                //}
                Color color = GetComponent<UISprite>().color;
                color.a = 0.5f;
                GetComponent<UISprite>().color = color;

            }
            else // 보상 수령 안함!
            {
                //Debug.Log($"{this.ID}보상 수령안했네");
                GetLabel((int)Labels.ButtonName_Label).text = Managers.Localization.GetLocalizedValue("Achievement_GetReward");

                //switch (Application.systemLanguage)
                //{
                //    case SystemLanguage.Korean:
                //        GetLabel((int)Labels.ButtonName_Label).text = this.getRewardButtonName_KR;
                //        break;
                //    case SystemLanguage.German:
                //        GetLabel((int)Labels.ButtonName_Label).text = this.getRewardButtonName_GER;
                //        break;
                //    case SystemLanguage.French:
                //        GetLabel((int)Labels.ButtonName_Label).text = this.getRewardButtonName_Fren;
                //        break;
                //    default:
                //        GetLabel((int)Labels.ButtonName_Label).text = this.getRewardButtonName_EN;
                //        break;
                //}
                GetButton((int)Buttons.Reward_Button).GetComponent<UISprite>().spriteName = this.getRewardButtonActiveSprite;
                GetButton((int)Buttons.Reward_Button).normalSprite = this.getRewardButtonActiveSprite;
                GetButton((int)Buttons.Reward_Button).pressedSprite = null;
                GetButton((int)Buttons.Reward_Button).enabled = true;
            }
        }
        else
        {
            GetLabel((int)Labels.ButtonName_Label).text = Managers.Localization.GetLocalizedValue("Achievement_Progress");

            //switch (Application.systemLanguage)
            //{
            //    case SystemLanguage.Korean:
            //        GetLabel((int)Labels.ButtonName_Label).text = this.progressButtonName_KR;
            //        break;
            //    case SystemLanguage.German:
            //        GetLabel((int)Labels.ButtonName_Label).text = this.progressButtonName_GER;
            //        break;
            //    case SystemLanguage.French:
            //        GetLabel((int)Labels.ButtonName_Label).text = this.progressButtonName_Fren;
            //        break;
            //    default:
            //        GetLabel((int)Labels.ButtonName_Label).text = this.progressButtonName_EN;
            //        break;
            //}
            GetButton((int)Buttons.Reward_Button).GetComponent<UISprite>().spriteName = this.getRewardButtonUnActiveSprite;
            GetButton((int)Buttons.Reward_Button).normalSprite = this.getRewardButtonUnActiveSprite;
            GetButton((int)Buttons.Reward_Button).pressedSprite = null;
            GetButton((int)Buttons.Reward_Button).enabled = false;
        }
    }
}
