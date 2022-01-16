using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



//LoginFriendList를 송신했을 때의 응답값.
//현재 로그인중인 친구목록을 받습니다.

public class LoginFriendList : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        int count = ByteConverter.ToInt(buffer, ref startIndex);

        Dictionary<string, Define.ProfileData> dict = new Dictionary<string, Define.ProfileData>();

        for (int i = 0; i<count; i++)
        {
            Define.ProfileData data = new Define.ProfileData();
            
            int nicknameLength = ByteConverter.ToInt(buffer, ref startIndex);
            string nickname = ByteConverter.ToString(buffer, ref startIndex, nicknameLength);

            int level = ByteConverter.ToInt(buffer, ref startIndex);
            int totalPlayCnt = ByteConverter.ToInt(buffer, ref startIndex);
            int defeatCnt = ByteConverter.ToInt(buffer, ref startIndex);
            int winCnt = ByteConverter.ToInt(buffer, ref startIndex);
            int killCnt = ByteConverter.ToInt(buffer, ref startIndex);
            int deathCnt = ByteConverter.ToInt(buffer, ref startIndex);
            int msgLength = ByteConverter.ToInt(buffer, ref startIndex);
            string msg = ByteConverter.ToString(buffer, ref startIndex, msgLength);

            data.nickname = nickname;
            data.level = level;
            data.totalPlayCnt = totalPlayCnt;
            data.defeatCnt = defeatCnt;
            data.winCnt = winCnt;
            data.killCnt = killCnt;
            data.deathCnt = deathCnt;
            data.StateMSG = msg;

            dict.Add(nickname, data);
            //각 행 별 처리
        }

        Managers.UI.GetPopupUI<CustomRoom_Popup>().SetInviteInfo(dict);
    }
}