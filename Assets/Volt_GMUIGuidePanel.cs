using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GuideMSGType
{
    RobotSetup, WaitPlaceOtherPlayerRobot, BehaviourSelect, RangeSelect, Synchronization, SynchronizationWait, Victory,
    Defeat, RoundEnd, SuddenDeath
}

public class Volt_GMUIGuidePanel : MonoBehaviour
{
    public UISprite guideTexture;
    public Volt_SpriteAnimationMSG spriteAnimationPrefab;
    public UILabel guideText;
    public UIPanel guidePanel;

    // Start is called before the first frame update
    
    public void ShowSpriteAnimationMSG(GuideMSGType msgType, bool isNeedFadeOut)
    {
        Volt_SpriteAnimationMSG spriteAnimationInstance = Instantiate(spriteAnimationPrefab,transform);
        spriteAnimationInstance.SetMSG(msgType, isNeedFadeOut);
    }
    public void ShowGuideTextMSG(GuideMSGType mSGType, bool isNeedFadeOut = true)
    {
        //if (Volt_GameManager.S.pCurPhase != Phase.gameOver)
        if(GameController.instance.CurrentPhase.type != Define.Phase.GameOver)
        {
            guideText.gameObject.SetActive(true);
            guideTexture.gameObject.SetActive(true);
            
            guideText.text = Managers.Localization.GetLocalizedValue($"GuideMSG_{mSGType}");
           
            if (isNeedFadeOut)
                StartCoroutine(FadeOutDestroy());
        }
    }
    public void HideGuideText()
    {
        guideText.gameObject.SetActive(false);
        guideTexture.gameObject.SetActive(false);
    }
    IEnumerator FadeOutDestroy()
    {
        yield return new WaitForSeconds(2f);
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            guideText.alpha -= Time.deltaTime;
            Color color = guideTexture.color;
            color.a -= Time.deltaTime;
            guideTexture.color = color;
            yield return null;
        }
        guideText.gameObject.SetActive(false);
        guideTexture.gameObject.SetActive(false);
        guideText.alpha = 1f;
        guideTexture.color = Color.white;
    }
    
}
