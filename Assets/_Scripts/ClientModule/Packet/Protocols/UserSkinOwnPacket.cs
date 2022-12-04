using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class UserSkinOwnPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("UserSkinOwnPacket Unpack");

        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        int columCount = ByteConverter.ToInt(buffer, ref startIndex);

        
        bool value;

        //List<UserACHCondition.InfoACH> achInfos = DBManager.instance.userNormalACHCondition.achInfos;
        int id = 5000001;
        for (int i = 0; i < (int)Define.RobotSkinCount.Volt_Max; i++)
        {
            value = ByteConverter.ToBool(buffer, ref startIndex);
            Debug.Log($"Volt Skin id:{id + i}, isHave:{value}");
            DBManager.instance.userSkinCondition.Add(id + i, value);
        }
        id = 5000011;
        for (int i = 0; i < (int)Define.RobotSkinCount.Mecury_Max; i++)
        {
            value = ByteConverter.ToBool(buffer, ref startIndex);
            Debug.Log($"Mercury Skin id:{id + i}, isHave:{value}");
            DBManager.instance.userSkinCondition.Add(id + i, value);
        }
        id = 5000021;
        for (int i = 0; i < (int)Define.RobotSkinCount.Hound_Max; i++)
        {
            value = ByteConverter.ToBool(buffer, ref startIndex);
            DBManager.instance.userSkinCondition.Add(id + i, value);
        }
        id = 5000031;
        for (int i = 0; i < (int)Define.RobotSkinCount.Reaper_Max; i++)
        {
            value = ByteConverter.ToBool(buffer, ref startIndex);
            DBManager.instance.userSkinCondition.Add(id + i, value);
        }
        DBManager.instance.OnLoadedUserSkinCondition();
    }
}
