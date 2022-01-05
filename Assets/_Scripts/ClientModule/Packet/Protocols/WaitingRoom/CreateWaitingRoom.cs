using UnityEditor;
using UnityEngine;



//방만들기 하였을 때 방이 잘 만들어졌는지 응답 패킷
public enum ECreateWaitingRoomResult { Success = 1, Error }

public class CreateWaitingRoom : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        ECreateWaitingRoomResult result = (ECreateWaitingRoomResult)ByteConverter.ToInt(buffer, ref startIndex);
        int roomId = ByteConverter.ToInt(buffer, ref startIndex);

        Debug.Log(result);
        Debug.Log($"Created room ID : {roomId}");

        if (result == ECreateWaitingRoomResult.Success)
        {
            LobbyScene lobbyScene = Managers.Scene.CurrentScene as LobbyScene;
            lobbyScene.customRoomManagement.SelfCreateCustomRoomUI(roomId);
        }
        else
        {
            Managers.UI.ShowPopupUIAsync<CustomRoomCreateFail_Popup>();
        }

    }
}