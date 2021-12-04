using UnityEditor;
using UnityEngine;



//초대 받고 수락하면 방에 들어가게되고, 방에 있는 다른이에게 이 패킷이 전송된다.
//본인의 경우 이 패킷을 받으면. InfoWaitingPacket을 받는다.


public class JoinWaitingRoom : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        int length = ByteConverter.ToInt(buffer, startIndex);
        //지금 방에 들어온 친구
        string nickname = ByteConverter.ToString(buffer, startIndex, length);

    }
}