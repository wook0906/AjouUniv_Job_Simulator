using UnityEditor;
using UnityEngine;



//FriendRequestList를 송신했을 때의 응답값.
//나에게 들어온 친구요청목록을 받습니다.

public class FriendRequestList : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        int count = ByteConverter.ToInt(buffer, ref startIndex);

        Volt_PlayerData.instance.friendsRequestList.Clear();
        
        for(int i = 0; i<count; i++)
        {
            Define.ProfileData data = new Define.ProfileData();

            int timeLengh = ByteConverter.ToInt(buffer, ref startIndex);
            string time = ByteConverter.ToString(buffer, ref startIndex, timeLengh);
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

            Volt_PlayerData.instance.friendsRequestList.Add(nickname, data);

            Debug.Log($"{time} : {nickname}");

            Volt_PlayerData.instance.friendsRequestList.Add(data.nickname, data);
            //각 행 별 처리

            //친구 요청을 리스트업 하는 UI를 만든 후, 로비 진입할때 친구요청목록을 서버에 요청, 이 패킷을 받아 UI에 리스트업.
        }
        FriendsRequestList_Popup popup = Managers.UI.GetPopupUI<FriendsRequestList_Popup>();
        if (popup)
            popup.RenewFriendsInfo();
    }
}