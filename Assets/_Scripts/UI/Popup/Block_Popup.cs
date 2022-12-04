using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Popup : UI_Popup
{
    enum Sprites
    {
        BG,
        Loading_img
    }

    public override void Init()
    {
        base.Init();

        Bind<UISprite>(typeof(Sprites));

        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        Get<UISprite>((int)Sprites.BG).SetRect(0f, 0f, screenWidth, screenHeight);

        int imgWidth = (int)(screenHeight * 0.1f);
        int imgHeight = (int)(screenHeight * 0.1f);
        Get<UISprite>((int)Sprites.Loading_img).SetRect(0f, 0f, imgWidth, imgHeight);
    }
}
