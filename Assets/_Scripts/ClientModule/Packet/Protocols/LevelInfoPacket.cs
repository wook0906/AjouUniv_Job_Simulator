using UnityEngine;
using UnityEditor;

public class LevelInfoPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        int rowCount = ByteConverter.ToInt(buffer, ref startIndex);

        int level;
        int exp;

        for (int i = 0; i < rowCount; i++)
        {
            level = ByteConverter.ToInt(buffer, ref startIndex);
            exp = ByteConverter.ToInt(buffer, ref startIndex);

            Debug.Log($"level : {level}, Need EXP : {exp}");

            DBManager.instance.infoLevelDict.Add(level, exp);
        }

        DBManager.instance.OnLoadedInfoLevel();
    }

}