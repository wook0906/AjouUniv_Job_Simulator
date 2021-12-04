using UnityEditor;
using UnityEngine;



//현재들어가 있는(을) 방의 정보입니다.
//State : 0: 호스트, 1: 정상적으로 들어가 있음, 2:초대 대기중인 사람.
public class InfoWaitingRoom : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        //들어가 있는 사람의 수.
        int count = ByteConverter.ToInt(buffer, ref startIndex);
        for(int i = 0; i<count; i++)
        {
            
            int nicknameLength = ByteConverter.ToInt(buffer, ref startIndex);
            string nickname = ByteConverter.ToString(buffer, startIndex, nicknameLength);
            int state = ByteConverter.ToInt(buffer, ref startIndex);
            //각 행 별 처리
        }

    }
}