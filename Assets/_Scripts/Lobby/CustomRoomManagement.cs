using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CustomRoomManagement : MonoBehaviour
{
    CustomRoom_Popup customRoomUI;
    public int curRoomID;
    public int mySlotNumber;
    

    public Dictionary<int, Define.CustomRoomItemSlotStateInfo> roomInfoDict = new Dictionary<int, Define.CustomRoomItemSlotStateInfo>()
    {
        {0,new Define.CustomRoomItemSlotStateInfo()},
        {1,new Define.CustomRoomItemSlotStateInfo()},
        {2,new Define.CustomRoomItemSlotStateInfo()},
        {3,new Define.CustomRoomItemSlotStateInfo()}
    };

    public void JoinWaitingRoom()
    {
        StartCoroutine(CorJoinWaitingRoom());
    }
    IEnumerator CorJoinWaitingRoom()
    {
        AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<CustomRoom_Popup>();
        yield return new WaitUntil(() => handle.IsDone);
        customRoomUI = handle.Result.GetComponent<CustomRoom_Popup>();
        customRoomUI.RenewRoomInfo();
    }
    public void SendCreateRoomRequest()
    {
        PacketTransmission.SendCreateWaitingRoomPacket(PlayerPrefs.GetInt("SELECTED_ROBOT"), PlayerPrefs.GetInt($"{(RobotType)PlayerPrefs.GetInt("SELECTED_ROBOT")}_skin"));
    }
    public void CreateCustomRoomUI(int roomID)
    {
        this.curRoomID = roomID;
        StartCoroutine(CorCreateCustomRoomUI());
    }
    IEnumerator CorCreateCustomRoomUI()
    {
        AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<CustomRoom_Popup>();
        yield return new WaitUntil(() => handle.IsDone);
        customRoomUI = handle.Result.GetComponent<CustomRoom_Popup>();

    }
    public void SelfCreateCustomRoomUI(int roomID)
    {
        this.curRoomID = roomID;
        this.mySlotNumber = 0;
        StartCoroutine(CorSelfCreateCustomRoomUI());
    }
    IEnumerator CorSelfCreateCustomRoomUI()
    {
        AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<CustomRoom_Popup>();
        yield return new WaitUntil(() => handle.IsDone);
        customRoomUI = handle.Result.GetComponent<CustomRoom_Popup>();
        customRoomUI.SetSlotState(0, Volt_PlayerData.instance.NickName, Define.CustomRoomSlotState.Host);

    }
    public void SendExitRoomRequest()
    {
        Debug.Log($"Exit Room ID : {curRoomID}");
        Debug.Log($"mySeatNumber : {mySlotNumber}");
        PacketTransmission.SendExitWaitingRoomPacket(curRoomID,mySlotNumber);
    }
    public void CloseRoom()
    {
        customRoomUI.ClosePopupUI();
        roomInfoDict.Clear();
        for (int i = 0; i < 4; i++)
        {
            roomInfoDict[i] = new Define.CustomRoomItemSlotStateInfo();
        }
    }
    public void InvitePlayer(string nickname)
    {
        int slotNumber = GetEmptySlotNumber();
        PacketTransmission.SendInviteMyWaitingRoomPacket(nickname, slotNumber);
        //customRoomUI.SetSlotState(slotNumber, nickname, Define.CustomRoomSlotState.WaitPlayer);
    }
    public void ShowInviteResponse(int roomid, string hostName, int seatIdx)
    {
        StartCoroutine(CorShowInviteResponse(roomid,hostName,seatIdx));
    }
    IEnumerator CorShowInviteResponse(int roomid, string hostName, int seatIdx)
    {
        AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<InviteResponse_Popup>();
        yield return new WaitUntil(() => handle.IsDone);
        handle.Result.GetComponent<InviteResponse_Popup>().SetInfo(roomid, hostName, seatIdx);
    }
    int GetEmptySlotNumber()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!roomInfoDict[i].isEmpty)
            {
                roomInfoDict[i].isEmpty = true;
                return i;
            }
        }
        Debug.LogError("No Empty Slot");
        return -1;
    }
    public void SetEmptySlotState(int slotIdx)
    {
        customRoomUI.SetEmptySlotState(slotIdx);
        roomInfoDict[slotIdx].isEmpty = false;
    }
    public void SetSlotState(int slotNumber, string nickname, Define.CustomRoomSlotState slotState)
    {
        StartCoroutine(CorSetSlotState(slotNumber, nickname, slotState));
    }
    IEnumerator CorSetSlotState(int slotNumber, string nickname, Define.CustomRoomSlotState slotState)
    {
        yield return new WaitUntil(() => customRoomUI);
        customRoomUI.SetSlotState(slotNumber, nickname, slotState);
    }
    public void ShowJoinResult(EEnterWaitingRoomResult result)
    {
        StartCoroutine(CorShowJoinResult(result));
    }
    IEnumerator CorShowJoinResult(EEnterWaitingRoomResult result)
    {
        AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<CustomRoomJoinResult_Popup>();
        yield return new WaitUntil(() => handle.IsDone);
        handle.Result.GetComponent<CustomRoomJoinResult_Popup>().SetInfo(result);

    }
    public void ShowInviteTryResult(EInviteMyWaitingRoomResult result, string nickname, int seatIdx )
    {
        StartCoroutine(CorShowInviteTryResult(result, nickname, seatIdx));
    }
    IEnumerator CorShowInviteTryResult(EInviteMyWaitingRoomResult result, string nickname, int seatIdx)
    {
        AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<InviteTryResult_Popup>();
        yield return new WaitUntil(() => handle.IsDone);
        handle.Result.GetComponent<InviteTryResult_Popup>().SetInfo(result,nickname,seatIdx);

    }
    public void StartMatch()
    {
        PlayerPrefs.SetInt("isCustomGame", 1);
        Volt_PlayerData.instance.currnetCustomRoomID = curRoomID;
        Managers.Scene.LoadSceneAsync(Define.Scene.GameScene);
    }
    //1. 방생성, 삭제
    //2. 갱신
    //3. 초대
    //4. 참가
}
