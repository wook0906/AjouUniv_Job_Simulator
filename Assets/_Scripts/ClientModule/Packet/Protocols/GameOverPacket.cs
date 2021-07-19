using UnityEngine;

public class GameOverPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int winner = ByteConverter.ToInt(buffer, 13);
        //Debug.Log("################################# GAMEOVER ###############################");
        //Debug.Log("Winner : " + winner);

        GameController.instance.gameData.winner = winner;
        GameController.instance.gameData.isGameOverWaiting = true;
        GameController.instance.ChangePhase<GameOver>();
        

    }
}