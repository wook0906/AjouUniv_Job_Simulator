using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfilePictureSetup_Popup : UI_Popup
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
        GameObject pictureItemRoot = Get<GameObject>((int)GameObjects.PictureItemRoot);
        pictureItemRoot.transform.parent.GetComponent<UIPanel>().depth = GetComponent<UIPanel>().depth + 1;
        pictureBtns = pictureItemRoot.transform.GetComponentsInChildren<UIButton>();
        ItemInit();

        Invoke("Redraw", 0f);
    }
    void Redraw()
    {
        GetComponent<UIPanel>().gameObject.SetActive(false);
        GetComponent<UIPanel>().gameObject.SetActive(true);
        GetComponent<UIPanel>().alpha = 1f;
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
        ClosePopupUI();
    }
    //public override void OnActive()
    //{
    //    base.OnActive();
    //    //Managers.UI.PushToUILayerStack(this);
    //}
}
