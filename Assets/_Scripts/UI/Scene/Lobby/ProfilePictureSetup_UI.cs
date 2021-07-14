using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfilePictureSetup_UI : UI_Scene
{
    UIButton[] pictureBtns;

    enum GameObjects
    {
        PictureItemRoot,
    }
    enum Buttons
    {
        Exit_Btn,
    }

    private LobbyScene lobbyScene;

    public override void Init()
    {
        base.Init();

        Bind<UIButton>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        lobbyScene = Managers.Scene.CurrentScene as LobbyScene;

        Get<UIButton>((int)Buttons.Exit_Btn).onClick.Add(new EventDelegate(() =>
        {
            OnClose();
        }));

        pictureBtns = Get<GameObject>((int)GameObjects.PictureItemRoot).transform.GetComponentsInChildren<UIButton>();
        ItemInit();

        lobbyScene.OnLoadedProfilePictureSetupUI();
    }
    public void ItemInit()
    {
        for (int i = 0; i < pictureBtns.Length; i++)
        {
            //pictureBtns[i].GetComponent<UISprite>().spriteName = "$ProfileImageName$" + i;
            pictureBtns[i].GetComponent<UISprite>().spriteName = "Profile Image";
        }
    }
    public override void OnClose()
    {
        base.OnClose();
        lobbyScene.ChangeToLobbyCamera();
        Managers.UI.CloseSceneUI(this);
    }
    public override void OnActive()
    {
        base.OnActive();
        Managers.UI.PushToUILayerStack(this);
    }
}
