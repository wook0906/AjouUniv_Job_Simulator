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
        bgSprite = Get<GameObject>((int)GameObjects.BG).GetComponent<UISprite>();
        guideLabel = Get<UILabel>((int)Labels.GuideLabel);
        spriteAnimation = Get<GameObject>((int)(GameObjects.ArrowSpriteAnimation));

        Volt_GMUI.S._3dObjectInteractable = false;
    }

    public void SetWindow(TutorialExplainPopupSetupData data)
    {
        UIRoot root = transform.root.GetComponent<UIRoot>();
        texts = data.contents;
        bgSprite.width = data.width;
        bgSprite.height = data.height;
        guideLabel.fontSize = data.fontSize;
        transform.position = new Vector3(root.manualWidth * data.windowAnchor.x, root.manualHeight * data.windowAnchor.y, 0f);
        Debug.Log($"width : {root.manualWidth} , height : {root.manualHeight}, result : {transform.position}");
        bgButton.isEnabled = data.isButton;
        spriteAnimation.transform.position = new Vector3(root.manualWidth * data.arrowAnchor.x, root.manualHeight * data.arrowAnchor.y,0f);
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
            Volt_GMUI.S._3dObjectInteractable = true;
            TutorialData.S.isOnTutorialPopup = false;
            TutorialData.S.curTutorialIdx++;
            ClosePopupUI();
        }
        
    }
}
