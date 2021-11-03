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
            ClosePopupUI();
        }));
        GameObject pictureItemRoot = Get<GameObject>((int)GameObjects.PictureItemRoot);
        pictureItemRoot.transform.parent.GetComponent<UIPanel>().depth = GetComponent<UIPanel>().depth + 1;
        pictureBtns = pictureItemRoot.transform.GetComponentsInChildren<UIButton>();
        

        Invoke("Redraw", 0f);
    }
    void Redraw()
    {
        GetComponent<UIPanel>().gameObject.SetActive(false);
        GetComponent<UIPanel>().gameObject.SetActive(true);
        ItemInit();
        GetComponent<UIPanel>().alpha = 1f;
        
    }
   
    public void ItemInit()
    {

        int idx = 0;
        for (Define.ProfileArtItemID PAID = Define.ProfileArtItemID.ProfileArt001; PAID < Define.ProfileArtItemID.MAX; PAID++)
        {
            EventDelegate eventDelegate = new EventDelegate(this, "OnClickPictureItem");

            pictureBtns[idx].GetComponent<UISprite>().spriteName = PAID.ToString();
            pictureBtns[idx].normalSprite = PAID.ToString();
            pictureBtns[idx].hoverSprite = PAID.ToString();
            pictureBtns[idx].pressedSprite = PAID.ToString();
            pictureBtns[idx].onClick.Add(eventDelegate);

            eventDelegate.parameters[0] = Util.MakeParameter(pictureBtns[idx], typeof(UIButton));

            idx++;
        }
    }
    public override void OnClose()
    {
        base.OnClose();
        lobbyScene.ChangeToLobbyCamera();
    }
    void OnClickPictureItem(UIButton pictureItem)
    {
        Managers.UI.GetSceneUI<LobbyScene_UI>().ChangeProfilePicture(pictureItem.normalSprite);
    }
    //public override void OnActive()
    //{
    //    base.OnActive();
    //    //Managers.UI.PushToUILayerStack(this);
    //}
}
