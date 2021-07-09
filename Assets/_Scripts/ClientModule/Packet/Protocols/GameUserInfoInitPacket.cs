using UnityEngine;

public class GameUserInfoInitPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("GameUserInfoInitPacket Unpack");
        int startindex = PacketInfo.FromServerPacketSettingIndex;
        


    }
}