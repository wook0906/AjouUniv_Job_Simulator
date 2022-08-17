using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialExplaination_Popup : UI_Popup
{
    bool isPlayTyping = false;
    bool isTouched = false;

    UILabel guideLabel;
    UISprite bgSprite;
    UIButton bgButton;
    
    UISprite blockBGSprite;
    UIButton blockBGBtn;
    GameObject spriteAnimation;
    string texts;
    [SerializeField]
    private string tutorialDataRef;

    enum Labels
    {
        GuideLabel
    }

    enum GameObjects
    {
        BlockBG,
        BG,
        ArrowSpriteAnimation
    }


    public override void Init()
    {
        base.Init();

        TutorialData.S.isOnTutorialPopup = true;

        Bind<GameObject>(typeof(GameObjects));
        Bind<UILabel>(typeof(Labels));

        bgButton = Get<GameObject>((int)GameObjects.BG).GetComponent<UIButton>();
        bgButton.onClick.Add(new EventDelegate(OnClickButton));
        blockBGBtn = Get<GameObject>((int)GameObjects.BlockBG).GetComponent<UIButton>();
        blockBGBtn.onClick.Add(new EventDelegate(OnClickButton));
        blockBGSprite = Get<GameObject>((int)GameObjects.BlockBG).GetComponent<UISprite>();
        bgSprite = Get<GameObject>((int)GameObjects.BG).GetComponent<UISprite>();
        guideLabel = Get<UILabel>((int)Labels.GuideLabel);
        spriteAnimation = Get<GameObject>((int)(GameObjects.ArrowSpriteAnimation));

        Volt_GMUI.S._3dObjectInteractable = false;
    }
    
    public void SetWindow(TutorialExplainPopupSetupData data)
    {
        Transform tmp = transform.parent;
        UIRoot root = transform.root.GetComponent<UIRoot>();
        texts = Managers.Localization.GetLocalizedValue(data.keyForLocalize);
        tutorialDataRef = data.keyForLocalize;

        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        int gcd = Util.gcd(screenWidth, screenHeight);
        int widthRatio = screenWidth / gcd;
        int heightRatio = screenHeight / gcd;

        while (screenWidth < 1920)
            screenWidth += widthRatio;
        while (screenHeight < 1080)
            screenHeight += heightRatio;

        int widthOffset = screenWidth / Screen.width;
        int heightOffset = screenHeight / Screen.height;
        

        blockBGSprite.width = screenWidth;
        blockBGSprite.height = screenHeight;

        bgSprite.width = data.width * widthOffset;
        bgSprite.height = data.height * heightOffset;
        
        guideLabel.fontSize = data.fontSize * widthOffset;
        
        switch (Managers.Scene.CurrentScene.SceneType)
        {
            case Define.Scene.Title:
            case Define.Scene.Lobby:
                transform.localPosition = Vector3.zero;
                bgSprite.transform.localPosition = new Vector3(screenWidth * data.windowAnchor.x, screenHeight * data.windowAnchor.y, 0f);
                if (bgSprite.transform.localPosition.x + (data.width) > (screenWidth / 2))
                {
                    float overXValue = bgSprite.transform.localPosition.x + (data.width) - (screenWidth / 2);
                    Vector3 pos = bgSprite.transform.localPosition;
                    pos.x -= overXValue;
                    bgSprite.transform.localPosition = pos;
                }
                else if (bgSprite.transform.localPosition.x - (data.width) < (screenWidth / -2))
                {
                    float overXValue = bgSprite.transform.localPosition.x - (data.width) + (screenWidth / 2);
                    Vector3 pos = bgSprite.transform.localPosition;
                    pos.x += Mathf.Abs(overXValue);
                    bgSprite.transform.localPosition = pos;
                }
                Debug.Log($"BasedWidth : {screenWidth} , BasedHeight : {screenHeight}, result : {bgSprite.transform.localPosition}");
                break;
            case Define.Scene.Shop:
            case Define.Scene.Twincity:
            case Define.Scene.Rome:
            case Define.Scene.Ruhrgebiet:
            case Define.Scene.Tokyo:
            case Define.Scene.ResultScene:
            case Define.Scene.GameScene:
                transform.localPosition = Vector3.zero;
                bgSprite.transform.localPosition = new Vector3(screenWidth * data.windowAnchor.x, screenHeight * data.windowAnchor.y, 0f);
                if (bgSprite.transform.localPosition.x + (data.width) > (screenWidth / 2))
                {  
                    float overXValue = bgSprite.transform.localPosition.x + (data.width) - (screenWidth / 2);
                    Vector3 pos = bgSprite.transform.localPosition;
                    pos.x -= overXValue;
                    bgSprite.transform.localPosition = pos;
                }
                else if(bgSprite.transform.localPosition.x - (data.width) < (screenWidth / -2))
                {
                    float overXValue = bgSprite.transform.localPosition.x - (data.width) + (screenWidth / 2);
                    Vector3 pos = bgSprite.transform.localPosition;
                    pos.x += Mathf.Abs(overXValue);
                    bgSprite.transform.localPosition = pos;
                }
                Debug.Log($"BasedWidth : {screenWidth} , BasedHeight : {screenHeight}, result : {bgSprite.transform.localPosition}");

                break;
            default:
                break;
        }
        bgButton.isEnabled = data.isButton;
        blockBGBtn.isEnabled = data.isButton;
        spriteAnimation.transform.localPosition = new Vector3(screenWidth * data.arrowAnchor.x, screenHeight * data.arrowAnchor.y,0f);
        spriteAnimation.SetActive(data.isNeedArrow);

        ShowText(data);
    }
    public void ShowText(TutorialExplainPopupSetupData data)
    {
        StartCoroutine(FlowText(data));
    }
    IEnumerator FlowText(TutorialExplainPopupSetupData data)
    {
        if (isPlayTyping) yield break;
        isPlayTyping = true;
        //GetComponent<UIButton>().enabled = false;
        
        if (texts.Length >= 0)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                if (texts.IndexOf('[', i) == i)
                {
                    i = texts.IndexOf(']', i) + 1;
                }
                yield return new WaitForSeconds(0.03f);
                if (isTouched)
                {
                    guideLabel.text = texts;
                    isPlayTyping = false;
                    //GetComponent<UIButton>().enabled = true;
                    yield break;
                }
                else
                    guideLabel.text = texts.Substring(0, i);
            }
            guideLabel.text = texts;
        }
        isPlayTyping = false;
        if (!data.isButton)
        {
            Get<GameObject>((int)GameObjects.BlockBG).SetActive(data.isButton);
            Volt_GMUI.S._3dObjectInteractable = true;
        }
    }

    private void OnClickButton()
    {
        if (isPlayTyping)
        {
            //Debug.Log("왜?@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
            isTouched = true;
            return;
        }
        else
        {
            TutorialData.S.curTutorialIdx++;
            Volt_GMUI.S._3dObjectInteractable = true;
            TutorialData.S.isOnTutorialPopup = false;
            ClosePopupUI();
        }
        
    }
}
