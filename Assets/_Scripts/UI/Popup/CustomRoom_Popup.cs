using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CustomRoom_Popup : UI_Popup
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
        inviteItemRoot.transform.parent.GetComponent<UIPanel>().depth = GetComponent<UIPanel>().depth + 1;
        SetInviteInfo();


    }

    void SetInviteInfo()
    {
        StartCoroutine(CorSetInviteInfo());
    }
    IEnumerator CorSetInviteInfo()
    {
        for (int i = 0; i < 8; i++)
        {
            AsyncOperationHandle<GameObject> handle = Managers.UI.MakeSubItemAsync<InviteItem>(inviteItemRoot.transform);
            yield return new WaitUntil(() => { return handle.IsDone; });

            InviteItem item = handle.Result.GetComponent<InviteItem>();
     
            item.SetInfo($"Friends {i}", Random.Range(0,100));

            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;

        }
        inviteItemRoot.GetComponent<UIGrid>().Reposition();
        GetComponent<UIPanel>().gameObject.SetActive(false);
        Invoke("Redraw", 0f);
    }
    void Redraw()
    {
        GetComponent<UIPanel>().gameObject.SetActive(true);
        GetComponent<UIPanel>().alpha = 1f;
    }
    public override void OnClose()
    {
        base.OnClose();
        lobbyScene.ChangeToLobbyCamera();
        ClosePopupUI();
    }

    public void SetWaitPlayerInfo()
    {
        //TODO : 다른곳에다가 현재 방정보를 저장할 클래스를 제작해서 그 클래스로부터 정보를 받아와서 세팅하는걸로
    }
}
