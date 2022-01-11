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
       
        
        for(int i = 0; i<count; i++)
        {
            int timeLengh = ByteConverter.ToInt(buffer, ref startIndex);
            string time = ByteConverter.ToString(buffer, ref startIndex, timeLengh);
            int nicknameLength = ByteConverter.ToInt(buffer, ref startIndex);
            string nickname = ByteConverter.ToString(buffer, ref startIndex, nicknameLength);

            Debug.Log($"{time} : {nickname}");

            Define.ProfileData data = new Define.ProfileData();
            data.killCnt = 0;
            data.defeatCnt = 0;
            data.deathCnt = 0;
            data.level = 1;
            data.nickname = nickname;
            data.StateMSG = "Empty";
            data.winCnt = 0;

            Volt_PlayerData.instance.friendsRequestList.Add(data.nickname, data);
            //각 행 별 처리

            //친구 요청을 리스트업 하는 UI를 만든 후, 로비 진입할때 친구요청목록을 서버에 요청, 이 패킷을 받아 UI에 리스트업.
        }

    }
}