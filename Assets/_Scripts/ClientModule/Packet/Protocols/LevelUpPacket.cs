using UnityEngine;
using UnityEditor;

public class LevelUpPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        int prevLv = ByteConverter.ToInt(buffer, ref startIndex);
        int upLv = ByteConverter.ToInt(buffer, ref startIndex);
        //Rewards
        int coin = ByteConverter.ToInt(buffer, ref startIndex);
        int diamond = ByteConverter.ToInt(buffer, ref startIndex);
        int battery = ByteConverter.ToInt(buffer, ref startIndex);
       

        
    }

}