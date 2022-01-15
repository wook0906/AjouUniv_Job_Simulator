using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class FriendsRequestList_Popup : UI_Popup
{
    [SerializeField]
    GameObject requestItemRoot;
    enum Buttons
    {
        Close_Btn,
        Refresh_Btn,
    }
    enum GameObjects
    {
        RequestItemRoot,
    }
    public override void Init()
    {
        base.Init();
        Bind<UIButton>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.Close_Btn).onClick.Add(new EventDelegate(() =>
        {
            ClosePopupUI();
        }));
        GetButton((int)Buttons.Refresh_Btn).onClick.Add(new EventDelegate(() =>
        {
            PacketTransmission.SendFriendRequestListPacket();
        }));
        requestItemRoot = Get<GameObject>((int)GameObjects.RequestItemRoot);
        requestItemRoot.transform.parent.GetComponent<UIPanel>().depth = GetComponent<UIPanel>().depth + 1;

    }
    public void RenewFriendsInfo()
    {
        //플레이어 데이터를 긁어와서 그만큼 RequestItem을 생성한다
        foreach (Transform item in requestItemRoot.transform)
        {
            Managers.Resource.Destroy(item.gameObject);
        }
        StartCoroutine(CorRenewFriendsInfo());
    }
    IEnumerator CorRenewFriendsInfo()
    {
        foreach (var item in Volt_PlayerData.instance.friendsRequestList)
        {
            AsyncOperationHandle<GameObject> handle = Managers.UI.MakeSubItemAsync<FriendsRequestItem>(requestItemRoot.transform);
            yield return new WaitUntil(() => { return handle.IsDone; });

            FriendsRequestItem friendsitem = handle.Result.GetComponent<FriendsRequestItem>();
            friendsitem.SetInfo(item.Key);
            friendsitem.gameObject.layer = transform.root.gameObject.layer;
            friendsitem.transform.localPosition = Vector3.zero;
            friendsitem.transform.localScale = Vector3.one;
        }

        requestItemRoot.GetComponent<UIGrid>().Reposition();
        GetComponent<UIPanel>().gameObject.SetActive(false);
        Invoke("Redraw", 0f);
    }
    void Redraw()
    {
        GetComponent<UIPanel>().gameObject.SetActive(true);
        GetComponent<UIPanel>().alpha = 1f;
    }
    

}
