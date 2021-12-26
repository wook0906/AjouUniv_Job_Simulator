using UnityEditor;
using UnityEngine;



//방만들기 하였을 때 방이 잘 만들어졌는지 응답 패킷
public enum ECreateWaitingRoomResult { Success, Error }

public class CreateWaitingRoom : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        ECreateWaitingRoomResult result = (ECreateWaitingRoomResult)ByteConverter.ToInt(buffer, ref startIndex);

        if(result == ECreateWaitingRoomResult.Success)
        {
            LobbyScene lobbyScene = Managers.Scene.CurrentScene as LobbyScene;
            lobbyScene.customRoomManagement.CreateCustomRoomUI();
        }
        else
        {
            Managers.UI.ShowPopupUI<CustomRoomCreateFail_Popup>();
        }

    }
}