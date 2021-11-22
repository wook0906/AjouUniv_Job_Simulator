using UnityEngine;

public class GameOverPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        int winner = ByteConverter.ToInt(buffer, ref startIndex);
        //Debug.Log("################################# GAMEOVER ###############################");
        Debug.Log("Winner : " + winner);

        int gold = ByteConverter.ToInt(buffer, ref startIndex);
        int exp = ByteConverter.ToInt(buffer, ref startIndex);
        Debug.Log($"gold : {gold}");
        Debug.Log($"exp : {exp}");


        Volt_PlayerData.instance.Exp += exp;

        GameController.instance.gameData.winner = winner;
        GameController.instance.gameData.isGameOverWaiting = true;
        GameController.instance.ChangePhase<GameOver>();
        

    }
}