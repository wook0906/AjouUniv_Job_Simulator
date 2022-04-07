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
        for (int i = 0; i < 5; i++)
        {
            value = ByteConverter.ToBool(buffer, ref startIndex);
            DBManager.instance.userSkinCondition.Add(id + i, value);
        }
        id = 5000010;
        for (int i = 0; i < 5; i++)
        {
            value = ByteConverter.ToBool(buffer, ref startIndex);
            DBManager.instance.userSkinCondition.Add(id + i, value);
        }
        id = 5000020;
        for (int i = 0; i < 5; i++)
        {
            value = ByteConverter.ToBool(buffer, ref startIndex);
            DBManager.instance.userSkinCondition.Add(id + i, value);
        }
        id = 5000030;
        for (int i = 0; i < 5; i++)
        {
            value = ByteConverter.ToBool(buffer, ref startIndex);
            DBManager.instance.userSkinCondition.Add(id + i, value);
        }
        DBManager.instance.OnLoadedUserSkinCondition();
    }
}
