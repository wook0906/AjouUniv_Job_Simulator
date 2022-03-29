using UnityEngine;
using UnityEditor;


public class IAPPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        //Debug.Log("IAPPacket Unpack");
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        
    }
}