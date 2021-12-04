using UnityEditor;
using UnityEngine;



//친구 추가 요청을 받고 그 수락or거부 했을 때 패킷이 잘 날아갔는지에 대한 응답값.
public enum EConfirmFriendAddResult { Success, NotExist, DBError }

public class ConfirmFriendAdd : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        EConfirmFriendAddResult result = (EConfirmFriendAddResult)ByteConverter.ToInt(buffer, ref startIndex);
       


    }
}