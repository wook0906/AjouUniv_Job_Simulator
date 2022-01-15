using UnityEditor;
using UnityEngine;



//친구를 내 방에 초대 했을 때, 초대가 잘 전송 되었는지

public enum EInviteMyWaitingRoomResult
{
    Success = 1,
    /// <summary>존재하지 않는 유저 </summary>
    NotExist,
    /// <summary>이미 다른 게임 플레이중인 유저 </summary>
    AlreadyPlayUser,
    /// <summary>게임을 종료한 플레이어 </summary>
    ExitGame,
    /// <summary>이미 초대를 전송했습니다. </summary>
    AlreadyInvite,
    /// <summary>이미 들어와 있습니다. </summary>
    AlreadyEnter
}


public class InviteMyWaitingRoom : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        EInviteMyWaitingRoomResult result = (EInviteMyWaitingRoomResult)ByteConverter.ToInt(buffer, ref startIndex);
        int nicknameLength = ByteConverter.ToInt(buffer, ref startIndex);
        string nickname = ByteConverter.ToString(buffer, ref startIndex, nicknameLength);
        int seatIdx = ByteConverter.ToInt(buffer, ref startIndex);

        Debug.Log(result);



        LobbyScene scene = Managers.Scene.CurrentScene as LobbyScene;
        scene.customRoomManagement.ShowInviteTryResult(result,nickname,seatIdx);

    }
}