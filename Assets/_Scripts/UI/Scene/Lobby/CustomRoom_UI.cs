using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CustomRoom_UI : UI_Scene
{
    [SerializeField]
    GameObject inviteItemRoot;

    enum Buttons
    {
        Exit_Btn,
        AddFriends_Btn,
    }
    enum GameObjects
    {
        InviteItemRoot
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

        inviteItemRoot = Get<GameObject>((int)GameObjects.InviteItemRoot);

        SetInviteInfo();


    }

    void SetInviteInfo()
    {
        //플레이어 데이터를 긁어와서 그만큼 FriendsItem을 생성한다.
        StartCoroutine(CorSetInviteInfo());
    }
    IEnumerator CorSetInviteInfo()
    {
        for (int i = 0; i < 8; i++)
        {
            AsyncOperationHandle<GameObject> handle = Managers.UI.MakeSubItemAsync<FriendsItem>(inviteItemRoot.transform);
            yield return new WaitUntil(() => { return handle.IsDone; });

            FriendsItem item = handle.Result.GetComponent<FriendsItem>();
            //TODO : InviteItem으로 교체할것

            item.SetInfo($"temp{i}");

            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;

        }
        inviteItemRoot.GetComponent<UIGrid>().Reposition();

        lobbyScene.OnLoadedCustomRoomUI();

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
