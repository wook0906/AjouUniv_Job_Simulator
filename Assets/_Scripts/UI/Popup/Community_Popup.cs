using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Community_Popup : UI_Popup
{
    [SerializeField]
    GameObject friendsItemRoot;

    enum Buttons
    {
        Exit_Btn,
        AddFriends_Btn,
        FriendsRequestList_Btn,
    }
    enum GameObjects
    {
        FriendsItemRoot
    }
    enum Labels
    {
        AddFriends_InputField,
    }

    private LobbyScene lobbyScene;

    public override void Init()
    {
        base.Init();

        Bind<UIButton>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<UILabel>(typeof(Labels));

        

        lobbyScene = Managers.Scene.CurrentScene as LobbyScene;

        Get<UIButton>((int)Buttons.Exit_Btn).onClick.Add(new EventDelegate(() =>
        {
            ClosePopupUI();
            
        }));
        Get<UIButton>((int)Buttons.AddFriends_Btn).onClick.Add(new EventDelegate(() =>
        {
            PacketTransmission.SendRequestFriendAddPacket(GetLabel((int)Labels.AddFriends_InputField).text);
            GetLabel((int)Labels.AddFriends_InputField).text = string.Empty;
            //TODO : 현재 입력되어있는 닉네임(혹은 고유값)으로 친추추가 요청 패킷전송
        }));
        Get<UIButton>((int)Buttons.FriendsRequestList_Btn).onClick.Add(new EventDelegate(() =>
        {
            Managers.UI.ShowPopupUIAsync<FriendsRequestList_Popup>();
            //TODO : 현재 입력되어있는 닉네임(혹은 고유값)으로 친추추가 요청 패킷전송
        }));

        friendsItemRoot = Get<GameObject>((int)GameObjects.FriendsItemRoot);
        friendsItemRoot.transform.parent.GetComponent<UIPanel>().depth = GetComponent<UIPanel>().depth + 1;
        RenewFriendsInfo();
    }

    public void RenewFriendsInfo()
    {
        //플레이어 데이터를 긁어와서 그만큼 FriendsItem을 생성한다
        foreach (Transform item in friendsItemRoot.transform)
        {
            Managers.Resource.Destroy(item.gameObject);
        }
        StartCoroutine(CorRenewFriendsInfo());
    }
    IEnumerator CorRenewFriendsInfo()
    {
        foreach (var item in Volt_PlayerData.instance.friendsProfileDataDict)
        {
            AsyncOperationHandle<GameObject> handle = Managers.UI.MakeSubItemAsync<FriendsItem>(friendsItemRoot.transform);
            yield return new WaitUntil(() => { return handle.IsDone; });

            FriendsItem friendsitem = handle.Result.GetComponent<FriendsItem>();
            //item.GetComponent<UIPanel>().depth = friendsItemRoot.transform.parent.GetComponent<UIPanel>().depth + 1;
            friendsitem.SetInfo(item.Value);
            friendsitem.gameObject.layer = transform.root.gameObject.layer;
            friendsitem.transform.localPosition = Vector3.zero;
            friendsitem.transform.localScale = Vector3.one;
            //Vector3 moveVector = Vector3.up * friendsItemRoot.GetComponent<UIGrid>().cellHeight * i;

            //item.transform.localPosition -= moveVector;
        }
        //for (int i = 0; i < Volt_PlayerData.instance.friendsProfileDataDict.Count; i++)
        //{
            
        //}
        friendsItemRoot.GetComponent<UIGrid>().Reposition();
        GetComponent<UIPanel>().gameObject.SetActive(false);
        Invoke("Redraw",0f);
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

    }
    //public override void OnActive()
    //{
    //    base.OnActive();
    //    //Managers.UI.PushToUILayerStack(this);
    //}

    public void ShowRequestConfirmPopup(EConfirmFriendAddResult result, string nickname)
    {
        StartCoroutine(CorShowRequestConfirmPopup(result, nickname));
    }
    IEnumerator CorShowRequestConfirmPopup(EConfirmFriendAddResult result, string nickname)
    {
        AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<FriendsConfirmResult_Popup>();
        yield return new WaitUntil(() => handle.IsDone);
        handle.Result.GetComponent<FriendsConfirmResult_Popup>().SetInfo(nickname, result);
    }
    public void ShowRequestAddPopup(ERequestFriendAddResult result, string nickname)
    {
        StartCoroutine(CorShowRequestAddPopup(result, nickname));
    }
    IEnumerator CorShowRequestAddPopup(ERequestFriendAddResult result, string nickname)
    {
        AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<FriendsAddResult_Popup>();
        yield return new WaitUntil(() => handle.IsDone);
        handle.Result.GetComponent<FriendsAddResult_Popup>().SetInfo(nickname, result);
    }


}
