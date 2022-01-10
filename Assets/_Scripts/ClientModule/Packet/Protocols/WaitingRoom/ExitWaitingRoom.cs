using UnityEditor;
using UnityEngine;



//이 사람이 방에서 나갔다는 패킷.
//(나간 본인도 받을 수 있음)

public class ExitWaitingRoom : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        int length = ByteConverter.ToInt(buffer,ref startIndex);
        //지금 방에서 나간 친구
        string nickname = ByteConverter.ToString(buffer,ref startIndex, length);
        int seatIdx = ByteConverter.ToInt(buffer,ref startIndex);

        LobbyScene scene = Managers.Scene.CurrentScene as LobbyScene;

        if (nickname == Volt_PlayerData.instance.NickName)
            scene.customRoomManagement.CloseRoom();
        else
            scene.customRoomManagement.SetEmptySlotState(seatIdx + 1);
    }
}