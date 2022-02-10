using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CustomRoom_Popup : UI_Popup
{
    [SerializeField]
    GameObject inviteItemRoot;
    public List<InviteItem> inviteItems = new List<InviteItem>();

    enum Buttons
    {
        Exit_Btn,
        AddFriends_Btn,
        Start_Btn,
    }
    enum GameObjects
    {
        InviteItemRoot
    }
    enum Labels
    {
        NickName_Label1,
        NickName_Label2,
        NickName_Label3,
        NickName_Label4,
        
        State_Label1,
        State_Label2,
        State_Label3,
        State_Label4,

        Level_Label1,
        Level_Label2,
        Level_Label3,
        Level_Label4,
        OnlyHost_Label,
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
            lobbyScene.customRoomManagement.SendExitRoomRequest();
        }));
        UIButton startButton = Get<UIButton>((int)Buttons.Start_Btn);
        startButton.onClick.Add(new EventDelegate(()=>
        {
            lobbyScene.customRoomManagement.StartMatch();
        }));
        if (PlayerPrefs.GetInt("CustomRoomMySlotNumber") != 0)
        {
            startButton.isEnabled = false;
            GetLabel((int)Labels.OnlyHost_Label).color = Color.white;
        }
        else
        {
            PacketTransmission.SendLoginFriendListPacket();
            GetLabel((int)Labels.OnlyHost_Label).color = Color.clear;
        }

        inviteItemRoot = Get<GameObject>((int)GameObjects.InviteItemRoot);
        inviteItemRoot.transform.parent.GetComponent<UIPanel>().depth = GetComponent<UIPanel>().depth + 1;
        //SetInviteInfo();
    }

    public void SetInviteInfo(Dictionary<string, Define.ProfileData> dict)
    {
        foreach (Transform item in inviteItemRoot.transform)
        {
            Managers.Resource.Destroy(item.gameObject);
        }
        StartCoroutine(CorSetInviteInfo(dict));
    }
    IEnumerator CorSetInviteInfo(Dictionary<string, Define.ProfileData > dict)
    {
        foreach (var item in dict)
        {
            AsyncOperationHandle<GameObject> handle = Managers.UI.MakeSubItemAsync<InviteItem>(inviteItemRoot.transform);
            yield return new WaitUntil(() => { return handle.IsDone; });

            InviteItem inviteItem = handle.Result.GetComponent<InviteItem>();
            
            inviteItem.SetInfo(item.Value);

            inviteItem.transform.localPosition = Vector3.zero;
            inviteItem.transform.localScale = Vector3.one;
            inviteItems.Add(inviteItem);
        }
        inviteItemRoot.GetComponent<UIGrid>().Reposition();
        GetComponent<UIPanel>().gameObject.SetActive(false);
        Invoke("Redraw", 0f);
        yield return new WaitForSeconds(3f);
        PacketTransmission.SendLoginFriendListPacket();
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

    public void SetSlotState(int slotIdx, string nickname, Define.CustomRoomSlotState slotState)
    {
        switch (slotIdx)
        {
            case 0:
                GetLabel((int)Labels.Level_Label1).text = "1";
                GetLabel((int)Labels.State_Label1).text = slotState.ToString();
                GetLabel((int)Labels.NickName_Label1).text = nickname;
                break;
            case 1:
                GetLabel((int)Labels.Level_Label2).text = "1";
                GetLabel((int)Labels.State_Label2).text = slotState.ToString();
                GetLabel((int)Labels.NickName_Label2).text = nickname;
                break;
            case 2:
                GetLabel((int)Labels.Level_Label3).text = "1";
                GetLabel((int)Labels.State_Label3).text = slotState.ToString();
                GetLabel((int)Labels.NickName_Label3).text = nickname;
                break;
            case 3:
                GetLabel((int)Labels.Level_Label4).text = "1";
                GetLabel((int)Labels.State_Label4).text = slotState.ToString();
                GetLabel((int)Labels.NickName_Label4).text = nickname;
                break;
            default:
                Debug.LogError($"SetSlotState Error slot Type : {slotIdx}");
                break;
        }
        lobbyScene.customRoomManagement.roomInfoDict[slotIdx].isNotEmpty = true;
    }
    public void SetEmptySlotState(int slotIdx)
    {
        switch (slotIdx)
        {
            case 0:
                GetLabel((int)Labels.Level_Label1).text = "-";
                GetLabel((int)Labels.State_Label1).text = "-";
                GetLabel((int)Labels.NickName_Label1).text = "-";
                break;
            case 1:
                GetLabel((int)Labels.Level_Label2).text = "-";
                GetLabel((int)Labels.State_Label2).text = "-";
                GetLabel((int)Labels.NickName_Label2).text = "-";
                break;
            case 2:
                GetLabel((int)Labels.Level_Label3).text = "-";
                GetLabel((int)Labels.State_Label3).text = "-";
                GetLabel((int)Labels.NickName_Label3).text = "-";
                break;
            case 3:
                GetLabel((int)Labels.Level_Label4).text = "-";
                GetLabel((int)Labels.State_Label4).text = "-";
                GetLabel((int)Labels.NickName_Label4).text = "-";
                break;
            default:
                Debug.LogError($"SetEmptySlotState Error slot Type : {slotIdx}");
                break;
        }
        lobbyScene.customRoomManagement.roomInfoDict[slotIdx].isNotEmpty = false;
    }
    public void SetWaitPlayerInfo()
    {
        //TODO : 다른곳에다가 현재 방정보를 저장할 클래스를 제작해서 그 클래스로부터 정보를 받아와서 세팅하는걸로
    }
    public void RenewRoomInfo()
    {
        for (int i = 0; i < 4; i++)
        {
            Define.CustomRoomItemSlotStateInfo info = lobbyScene.customRoomManagement.roomInfoDict[i];
            if (info.isNotEmpty)
                SetEmptySlotState(i);
            else
            {
                if (info.nickname == Volt_PlayerData.instance.NickName)
                    PlayerPrefs.SetInt("CustomRoomMySlotNumber", i);
                if(i == 0)
                    SetSlotState(i, info.nickname, Define.CustomRoomSlotState.Host);
                else
                    SetSlotState(i, info.nickname, info.state);
            }
        }
    }
    
}
