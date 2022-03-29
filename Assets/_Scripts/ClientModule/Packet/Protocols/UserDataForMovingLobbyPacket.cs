using UnityEngine;
using UnityEditor;


public class UserDataForMovingLobbyPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("UserDataForMovingLobbyPacket Unpack");
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        
    }
}