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
    UIButton blockBGBtn;
    GameObject spriteAnimation;
    string texts;

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
        bgSprite.width = data.width;
        bgSprite.height = data.height;
        guideLabel.fontSize = data.fontSize;
        switch (Managers.Scene.CurrentScene.SceneType)
        {
            case Define.Scene.Title:
            case Define.Scene.Lobby:
            case Define.Scene.Shop:
                transform.localPosition = new Vector3(root.manualWidth * data.windowAnchor.x, root.manualHeight * data.windowAnchor.y, 0f);
                Debug.Log($"BasedWidth : {root.manualWidth} , BasedHeight : {root.manualHeight}, result : {transform.position}");
                break;

            case Define.Scene.Twincity:
            case Define.Scene.Rome:
            case Define.Scene.Ruhrgebiet:
            case Define.Scene.Tokyo:
            case Define.Scene.ResultScene:
            case Define.Scene.GameScene:
                transform.localPosition = new Vector3(Screen.width * data.windowAnchor.x, Screen.height * data.windowAnchor.y, 0f);
                Debug.Log($"BasedWidth : {Screen.width} , BasedHeight : {Screen.height}, result : {transform.position}");
                break;
            default:
                break;
        }
        bgButton.isEnabled = data.isButton;
        blockBGBtn.isEnabled = data.isButton;
        spriteAnimation.transform.localPosition = new Vector3(root.manualWidth * data.arrowAnchor.x, root.manualHeight * data.arrowAnchor.y,0f);
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
