using UnityEngine;
using UnityEditor;

public class WaitingPlayerCountPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketSettingIndex;
        int waitingPlayerCount = ByteConverter.ToInt(buffer, ref startIndex);
        //Debug.Log("waitingPlayerCount : " + waitingPlayerCount);
        //Volt_GMUI.S.RenewWaitingPlayerCount(waitingPlayerCount);

        StartMatching phase = GameController.instance.CurrentPhase as StartMatching;

        phase.RenewWaitingPlayerCount(waitingPlayerCount);
    }
}