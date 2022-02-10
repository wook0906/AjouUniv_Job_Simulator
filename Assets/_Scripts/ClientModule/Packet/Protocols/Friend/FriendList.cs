using UnityEditor;
using UnityEngine;



//FriendList를 송신했을 때의 응답값.
//전체 친구목록을 받습니다.

public class FriendList : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("FriendList Unpack");
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        int count = ByteConverter.ToInt(buffer, ref startIndex);
        Debug.Log($"{count} 명의 친구!");

        for (int i = 0; i<count; i++)
        {
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

            Define.ProfileData friendProfile = new Define.ProfileData();
            friendProfile.nickname = nickname;
            friendProfile.killCnt = killCnt;
            friendProfile.level = level;
            friendProfile.StateMSG = msg;
            friendProfile.totalPlayCnt = totalPlayCnt;
            friendProfile.winCnt = winCnt;
            friendProfile.deathCnt = deathCnt;
            friendProfile.StateMSG = msg;
            friendProfile.defeatCnt = defeatCnt;

            Debug.Log(friendProfile.nickname);
            if (!Volt_PlayerData.instance.friendsProfileDataDict.ContainsKey(nickname))
            {
                Volt_PlayerData.instance.friendsProfileDataDict.Add(nickname, friendProfile);
            }
            //각 행 별 처리
        }
        Managers.UI.ShowPopupUIAsync<Community_Popup>();
    }
}