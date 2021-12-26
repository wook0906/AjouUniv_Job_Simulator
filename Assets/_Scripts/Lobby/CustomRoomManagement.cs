using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CustomRoomManagement : MonoBehaviour
{
    CustomRoom_Popup customRoomUI;

    public int mySlotNumber;
   
    //Empty == false
    Dictionary<int, bool> slotStateDict = new Dictionary<int, bool>()
    {
        {1,false },
        {2,false },
        {3,false },
        {4,false }
    };


    public void SendCreateRoomRequest()
    {
        PacketTransmission.SendCreateWaitingRoomPacket();
    }
    public void CreateCustomRoomUI()
    {
        customRoomUI = Managers.UI.ShowPopupUI<CustomRoom_Popup>();
    }
    public void SendExitRoomRequest()
    {
        PacketTransmission.SendExitWaitingRoomPacket(mySlotNumber);
    }
    public void CloseRoom()
    {
        customRoomUI.ClosePopupUI();
        for (int i = 1; i < 4; i++)
        {
            slotStateDict[i] = false;
        }
    }
    public void InvitePlayer(string nickname)
    {
        int slotNumber = GetEmptySlotNumber();
        PacketTransmission.SendInviteMyWaitingRoomPacket(nickname, slotNumber);
        customRoomUI.SetSlotState(slotNumber, nickname, Define.CustomRoomSlotState.WaitPlayer);
    }
    public void ShowInviteResponse(int roomid, string hostName)
    {
        StartCoroutine(CorShowInviteResponse(roomid,hostName));
    }
    IEnumerator CorShowInviteResponse(int roomid, string hostName)
    {
        AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<InviteResponse_Popup>();
        yield return new WaitUntil(() => handle.IsDone);
        handle.Result.GetComponent<InviteResponse_Popup>().SetInfo(roomid, hostName);
    }
    int GetEmptySlotNumber()
    {
        for (int i = 1; i < 4; i++)
        {
            if (!slotStateDict[i])
            {
                slotStateDict[i] = true;
                return i;
            }
        }
        Debug.LogError("No Empty Slot");
        return -1;
    }
    public void SetEmptySlotState(int slotIdx)
    {
        customRoomUI.SetEmptySlotState(slotIdx);
        slotStateDict[slotIdx] = false;
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
    public void ShowInviteTryResult(EInviteMyWaitingRoomResult result)
    {
        StartCoroutine(CorShowInviteTryResult(result));
    }
    IEnumerator CorShowInviteTryResult(EInviteMyWaitingRoomResult result)
    {
        AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<InviteTryResult_Popup>();
        yield return new WaitUntil(() => handle.IsDone);
        handle.Result.GetComponent<InviteTryResult_Popup>().SetInfo(result);

    }
    //1. 방생성, 삭제
    //2. 갱신
    //3. 초대
    //4. 참가
}
