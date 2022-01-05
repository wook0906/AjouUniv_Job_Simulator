using UnityEditor;
using UnityEngine;



//초대 받고 수락하면 방에 들어가게되고, 방에 있는 다른이에게 이 패킷이 전송된다.
//본인의 경우 이 패킷을 받고. 추가적으로 InfoWaitingPacket을 받는다.

public enum EEnterWaitingRoomResult { Success, Fail, HostOut, Timeout }

public class JoinWaitingRoom : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        int length = ByteConverter.ToInt(buffer, startIndex);
        //지금 방에 들어온 친구
        string nickname = ByteConverter.ToString(buffer, startIndex, length);
        int seatIdx = ByteConverter.ToInt(buffer, startIndex);
        EJoinWaitingRoomResult joinResult = (EJoinWaitingRoomResult)ByteConverter.ToInt(buffer, startIndex);
        EEnterWaitingRoomResult enterResult = (EEnterWaitingRoomResult)ByteConverter.ToInt(buffer, startIndex);

        LobbyScene scene = Managers.Scene.CurrentScene as LobbyScene;
        //1.본인이 수락/거절에 대한 결과값을 받은 경우
        if (nickname == "본인닉네임")
        {
            if(joinResult == EJoinWaitingRoomResult.Reject)
            {
                //본인이 거절했으므로 나가리 처리
            }
            else
            {
                //수락 한 경우
                switch (enterResult)
                {
                    case EEnterWaitingRoomResult.Success:
                        Managers.UI.ShowPopupUI<CustomRoom_Popup>();
                        //성공. 방에 enter했음. 추가적으로 InfoWaitingPacket을 주겠음.
                        break;
                    case EEnterWaitingRoomResult.Fail:
                        //모종의 이유로 실패
                    case EEnterWaitingRoomResult.HostOut:
                        //나는 수락했으나, 호스트가 방 파기했음
                    case EEnterWaitingRoomResult.Timeout:
                        scene.customRoomManagement.ShowJoinResult(enterResult);
                        //제한시간 15초(임의) 내에 수락을 누르지 않았음.
                        break;
                }
                
            }
            return;
        }

        
        //2.본인이외의 사람이 브로드캐스팅 받아 nickname이 방에 들어왔음을 알게 된 경우
        if (joinResult == EJoinWaitingRoomResult.Ok)
        {
            scene.customRoomManagement.SetSlotState(seatIdx + 1, nickname, Define.CustomRoomSlotState.Ready);
            //해당 seatIdx자리에 nickname유저가 성공적으로 들어왔음.
            //EEnterWaitingRoomResult가 Success가 아니면 브로드캐스팅이 되지않으므로 신경 X
        }
        else if(joinResult == EJoinWaitingRoomResult.Reject)
        {
            scene.customRoomManagement.SetEmptySlotState(seatIdx + 1);
            //해당 seatIdx자리에 초대중이던 nickname유저를 삭제
        }
    }
}