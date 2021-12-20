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
            string nickname = ByteConverter.ToString(buffer, startIndex, nicknameLength);

            Define.ProfileData friendProfile = new Define.ProfileData();
            friendProfile.nickname = nickname;
            friendProfile.killCnt = 0;
            friendProfile.level = 1;
            friendProfile.StateMSG = "Empty";
            friendProfile.totalPlayCnt = 2;
            friendProfile.winCnt = 3;

            Volt_PlayerData.instance.friendsProfileDataDict.Add(nickname, friendProfile);
            //각 행 별 처리
        }

    }
}