using UnityEngine;

public class BehaviorOrderCompletionPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        //Debug.Log("BehaviorOrderCompletionPacket Unpack");

        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        int armageddonTileIdx = ByteConverter.ToInt(buffer, ref startIndex);
        int armageddonTileIdx2 = ByteConverter.ToInt(buffer, ref startIndex);
        int armageddonTileIdx3 = ByteConverter.ToInt(buffer, ref startIndex);
        int armageddonTileIdx4 = ByteConverter.ToInt(buffer, ref startIndex);

        //Debug.Log(armageddonTileIdx+", "+armageddonTileIdx2 + ", " + armageddonTileIdx3 + ", " + armageddonTileIdx4);


        GameController.instance.gameData.randomOptionValues.Add(armageddonTileIdx);
        GameController.instance.gameData.randomOptionValues.Add(armageddonTileIdx2);
        GameController.instance.gameData.randomOptionValues.Add(armageddonTileIdx3);
        GameController.instance.gameData.randomOptionValues.Add(armageddonTileIdx4);
        //Volt_GameManager.S.SimulationStart(armageddonTileIdx,armageddonTileIdx2,armageddonTileIdx3,armageddonTileIdx4);

        
        GameController.instance.ChangePhase<Simulation>();

        Volt_PlayerUI.S.ShowModuleButton(false);
    }
}