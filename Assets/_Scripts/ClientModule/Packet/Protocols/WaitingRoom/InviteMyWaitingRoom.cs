using UnityEditor;
using UnityEngine;



//친구를 내 방에 초대 했을 때, 초대가 잘 전송 되었는지

public enum EInviteMyWaitingRoomResult { Success,
    /// <summary>존재하지 않는 유저 </summary>
    NotExist,
    /// <summary>이미 다른 게임 플레이중인 유저 </summary>
    AlreadyPlayUser,
    /// <summary>게임을 종료한 플레이어 </summary>
    ExitGame,
    /// <summary>이미 초대를 전송했습니다. </summary>
    AlreadyInvite
}


public class InviteMyWaitingRoom : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        EInviteMyWaitingRoomResult result = (EInviteMyWaitingRoomResult)ByteConverter.ToInt(buffer, ref startIndex);

    }
}