using UnityEngine;

public class MatchingCompletionPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("MatchingCompletionPacket UnPack");

       
        CommunicationWaitQueue.Instance.ResetOrder();
        CommunicationInfo.IsBoardGamePlaying = true;
        CommunicationInfo.PlayCount = 0;
        CommunicationInfo.ModuleChoiceRequestOrder = 0;


        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        int mapType = ByteConverter.ToInt(buffer, ref startIndex);

        // 1???? ????????, 2???? ????????
        int player1Type = ByteConverter.ToInt(buffer, ref startIndex);
        int player2Type = ByteConverter.ToInt(buffer, ref startIndex);
        int player3Type = ByteConverter.ToInt(buffer, ref startIndex);
        int player4Type = ByteConverter.ToInt(buffer, ref startIndex);

        int character1Type = ByteConverter.ToInt(buffer, ref startIndex);
        int character2Type = ByteConverter.ToInt(buffer, ref startIndex);
        int character3Type = ByteConverter.ToInt(buffer, ref startIndex);
        int character4Type = ByteConverter.ToInt(buffer, ref startIndex);

        int skin1 = ByteConverter.ToInt(buffer, ref startIndex);
        int skin2 = ByteConverter.ToInt(buffer, ref startIndex);
        int skin3 = ByteConverter.ToInt(buffer, ref startIndex);
        int skin4 = ByteConverter.ToInt(buffer, ref startIndex);

        int nickName1Length = ByteConverter.ToInt(buffer, ref startIndex);
        string nickName1 = ByteConverter.ToString(buffer, ref startIndex, nickName1Length);
        int nickName2Length = ByteConverter.ToInt(buffer, ref startIndex);
        string nickName2 = ByteConverter.ToString(buffer, ref startIndex, nickName2Length);
        int nickName3Length = ByteConverter.ToInt(buffer, ref startIndex);
        string nickName3 = ByteConverter.ToString(buffer, ref startIndex, nickName3Length);
        int nickName4Length = ByteConverter.ToInt(buffer, ref startIndex);
        string nickName4 = ByteConverter.ToString(buffer, ref startIndex, nickName4Length);


        Volt_PlayerManager.S.SetupPlayersInfo((PlayerType)player1Type, (RobotType)character1Type, (SkinType)skin1, nickName1,
                                            (PlayerType)player2Type, (RobotType)character2Type, (SkinType)skin2, nickName2,
                                            (PlayerType)player3Type, (RobotType)character3Type, (SkinType)skin3, nickName3,
                                            (PlayerType)player4Type, (RobotType)character4Type, (SkinType)skin4, nickName4);

        //Volt_GameManager.S.ArenaSetupStart(mapType);


        GameController.instance.gameData.mapType = (Define.MapType)mapType;


        GameController.instance.ChangePhase(new ArenaSetup());
        Volt_PlayerData.instance.OnPlayGame();
        
    }
}