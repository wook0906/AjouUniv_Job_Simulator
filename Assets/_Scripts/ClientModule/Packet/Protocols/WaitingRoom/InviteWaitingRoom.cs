using UnityEditor;
using UnityEngine;



//초대를 받은 사람이 받는 패킷.
//들어올것인지 물음.

public class InviteWaitingRoom : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        int length = ByteConverter.ToInt(buffer, startIndex);
        //초대한 친구
        string nickname = ByteConverter.ToString(buffer, startIndex, length);


    }
}