using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACHRewardItem : UIBase
{
    public const int offset = 15;
    public bool isInit = false;

    enum Sprites
    {
        RewardIcon_Sprite,
    }
    enum Labels
    {
        RewardCount_Label,
    }
    public override void Init()
    {
        Bind<UISprite>(typeof(Sprites));
        Bind<UILabel>(typeof(Labels));
    }
    public void SetInfo(EShopPurchase assetsType, string content)
    {
        switch (assetsType)
        {
            case EShopPurchase.Skin:
                Get<UISprite>((int)Sprites.RewardIcon_Sprite).spriteName = "hangar_Normal";
                break;
            case EShopPurchase.Gold:
                Get<UISprite>((int)Sprites.RewardIcon_Sprite).spriteName = "Icon_Money";
                break;
            case EShopPurchase.Diamond:
                Get<UISprite>((int)Sprites.RewardIcon_Sprite).spriteName = "Icon_Diamond";
                break;
            case EShopPurchase.Battery:
                Get<UISprite>((int)Sprites.RewardIcon_Sprite).spriteName = "Icon_Battery";
                break;
            default:
                Debug.Log("ACHRewardItem SetInfo Error");
                break;
        }
        Get<UILabel>((int)Labels.RewardCount_Label).text = content;
        if (content.Length > 4)
        {
            Get<UILabel>((int)Labels.RewardCount_Label).leftAnchor.absolute += (content.Length * offset) / 2;
            Get<UILabel>((int)Labels.RewardCount_Label).rightAnchor.absolute += (content.Length * offset) / 2;
            Get<UILabel>((int)Labels.RewardCount_Label).width += content.Length * offset;
            GetComponent<UISprite>().width = 180 + content.Length * offset;
        }
        isInit = true;
    }
}
    
