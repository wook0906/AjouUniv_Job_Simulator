using UnityEditor;
using UnityEngine;


//호스트가 특정인을 초대하였을 때!

//방에 있는 모든 세션에 브로드 캐스팅 해주었습니다.

//초대한 호스트 닉, 초대한 친구
//현재 방의 id, 자리 인덱스(0번부터 3번)

public class InviteWaitingRoom : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        //초대한 호스트 닉네임
        int length = ByteConverter.ToInt(buffer, startIndex);
        string hostNickname = ByteConverter.ToString(buffer, startIndex, length);

        //초대된 친구
        int inviteLength = ByteConverter.ToInt(buffer, startIndex);
        string InvitedNickname = ByteConverter.ToString(buffer, startIndex, inviteLength);

        int roomid = ByteConverter.ToInt(buffer, startIndex);
        int seatIdx = ByteConverter.ToInt(buffer, startIndex);

        //받은 사람이 본인 일경우
        if(InvitedNickname == "내닉네임")
        {
            //방에 드가시겠습니까? 
            //초대 다이얼로그를 띄운다.
        }
        //받은 사람이 호스트 일경우
        else if(hostNickname == "내닉네임")
        {
            //seatIdx 자리에 InvitedNickname을 초대 대기중인 상태로 채워넣는다.
            
            //(초대된 사람은 거부하거나 15초(임의)가 지나면 JoinWaitingRoom(result:거절)패킷이 날라온다.
            //(승락할 경우 JoinWaitingRoom(result:승락)패킷이 날라온다.

            //그러므로 일단 채워넣고 서버에서 JoinWaitingRoom 패킷을 기다린다.
        }
        //받은 사람이 방에 들어와 있는 다른 친구일경우
        else
        {
            //seatIdx 자리에 InvitedNickname을 초대 대기중인 상태로 채워넣는다.
            //호스트 일 때와 상동.
        }
    }
}