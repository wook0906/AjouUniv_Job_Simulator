using UnityEditor;
using UnityEngine;



//FriendList를 송신했을 때의 응답값.
//전체 친구목록을 받습니다.

public class FriendList : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        int count = ByteConverter.ToInt(buffer, ref startIndex);
       
        
        for(int i = 0; i<count; i++)
        {
            int nicknameLength = ByteConverter.ToInt(buffer, ref startIndex);
            string nickname = ByteConverter.ToString(buffer, startIndex, nicknameLength);
            
            //각 행 별 처리
        }

    }
}