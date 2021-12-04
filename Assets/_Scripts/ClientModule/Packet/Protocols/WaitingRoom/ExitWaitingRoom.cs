using UnityEditor;
using UnityEngine;



//이 사람이 방에서 나갔다는 패킷.
//(나간 본인도 받을 수 있음)

public class ExitWaitingRoom : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        int length = ByteConverter.ToInt(buffer, startIndex);
        //지금 방에서 나간 친구
        string nickname = ByteConverter.ToString(buffer, startIndex, length);

    }
}