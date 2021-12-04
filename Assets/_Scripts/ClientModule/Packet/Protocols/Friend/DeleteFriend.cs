using UnityEditor;
using UnityEngine;



//친구 삭제 요청에 대한 결과값을 받습니다.
//잘 전송 되었는지에 대한 응답 값.
public enum EDeleteFriendResult { Success, NotExist, DBError }

public class DeleteFriend : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        EDeleteFriendResult result = (EDeleteFriendResult)ByteConverter.ToInt(buffer, ref startIndex);
       


    }
}