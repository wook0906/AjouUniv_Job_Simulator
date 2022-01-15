using UnityEditor;
using UnityEngine;





public class StartWaitingRoom : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        int roomid = ByteConverter.ToInt(buffer,ref startIndex);

        //방장이 게임시작하겠다는 메시지를 보냈고, 이를 브로드캐스팅해서 왔음.
        //GameScene으로 넘긴뒤, SendReadyToWaitingRoom 해주어야합니다.
    }
}