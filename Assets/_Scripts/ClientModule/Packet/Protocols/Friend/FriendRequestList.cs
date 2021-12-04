using UnityEditor;
using UnityEngine;



//FriendRequestList를 송신했을 때의 응답값.
//나에게 들어온 친구요청목록을 받습니다.

public class FriendRequestList : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        int count = ByteConverter.ToInt(buffer, ref startIndex);
       
        
        for(int i = 0; i<count; i++)
        {
            int timeLengh = ByteConverter.ToInt(buffer, ref startIndex);
            string time = ByteConverter.ToString(buffer, startIndex, timeLengh);
            int nicknameLength = ByteConverter.ToInt(buffer, ref startIndex);
            string nickname = ByteConverter.ToString(buffer, startIndex, nicknameLength);
            
            //각 행 별 처리
        }

    }
}