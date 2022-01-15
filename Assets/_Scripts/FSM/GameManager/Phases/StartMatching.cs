using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class StartMatching : PhaseBase
{
    MatchingScreen_Popup matchingScreen;
    public int connectedPlayerCount = 0;

    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter StartMatching");

        type = Define.Phase.StartMatching;

        RobotType selectedRobotType = (RobotType)PlayerPrefs.GetInt("SELECTED_ROBOT");

        //Volt_PlayerManager.S.GetMyPlayerNumberFromServer(1);
        //Volt_PlayerManager.S.SetupPlayersInfo(PlayerType.HOSTPLAYER, selectedRobotType, Volt_PlayerData.instance.selectdRobotSkins[selectedRobotType].SkinType, Volt_PlayerData.instance.NickName,
        //                                    PlayerType.AI, randomKillbotType2, (SkinType)Random.Range(0, 7), Volt_Utils.GetRandomKillbotName(randomKillbotType2),
        //                                    PlayerType.AI, randomKillbotType3, (SkinType)Random.Range(0, 7), Volt_Utils.GetRandomKillbotName(randomKillbotType3),
        //                                    PlayerType.AI,  randomKillbotType4, (SkinType)Random.Range(0, 7), Volt_Utils.GetRandomKillbotName(randomKillbotType4));

        if (PlayerPrefs.GetInt("Volt_TrainingMode")==1)
        {
            Volt_PlayerManager.S.GetMyPlayerNumberFromServer(1);
            RobotType randomRobotType1 = (RobotType)UnityEngine.Random.Range((int)RobotType.Volt, (int)RobotType.Max);
            RobotType randomRobotType2 = (RobotType)UnityEngine.Random.Range((int)RobotType.Volt, (int)RobotType.Max);
            RobotType randomRobotType3 = (RobotType)UnityEngine.Random.Range((int)RobotType.Volt, (int)RobotType.Max);
            SkinType randomSkinType1;
            do
            {
                randomSkinType1 = (SkinType)UnityEngine.Random.Range((int)SkinType.Origin, (int)SkinType.Max);
            }
            while (randomSkinType1 == SkinType.Christmas);
            SkinType randomSkinType2;
            do
            {
                randomSkinType2 = (SkinType)UnityEngine.Random.Range((int)SkinType.Origin, (int)SkinType.Max);
            }
            while (randomSkinType2 == SkinType.Christmas);
            SkinType randomSkinType3;
            do
            {
                randomSkinType3 = (SkinType)UnityEngine.Random.Range((int)SkinType.Origin, (int)SkinType.Max);
            }
            while (randomSkinType3 == SkinType.Christmas);
            Volt_PlayerManager.S.SetupPlayersInfo(PlayerType.HOSTPLAYER, (RobotType)PlayerPrefs.GetInt("SELECTED_ROBOT"), (SkinType)PlayerPrefs.GetInt($"{(RobotType)PlayerPrefs.GetInt("SELECTED_ROBOT")}_skin"), Volt_PlayerData.instance.NickName,
                                            PlayerType.AI, randomRobotType1, randomSkinType1, Volt_Utils.GetRandomKillbotName(randomRobotType1),
                                            PlayerType.AI, randomRobotType2, randomSkinType2, Volt_Utils.GetRandomKillbotName(randomRobotType2),
                                            PlayerType.AI, randomRobotType3, randomSkinType3, Volt_Utils.GetRandomKillbotName(randomRobotType3));

            //Volt_GameManager.S.ArenaSetupStart(mapType);


            GameController.instance.gameData.mapType = (Define.MapType)PlayerPrefs.GetInt("SELECTED_MAP");
            GameController.instance.ChangePhase<ArenaSetup>();
        }
        else if (PlayerPrefs.GetInt("CustomRoomID") != -1)
        {
            PacketTransmission.SendReadyToWaitingRoom(PlayerPrefs.GetInt("CustomRoomID"), PlayerPrefs.GetInt("CustomRoomMySlotNumber"));
            PlayerPrefs.SetInt("CustomRoomID", -1);
            PlayerPrefs.SetInt("CustomRoomMySlotNumber", -1);
            //TODO 나중에 뭐... Result씬에서 필요할 수도 있는데... 일단은
        }
        else
            PacketTransmission.SendMatchingRequestPacket((int)selectedRobotType, (int)Volt_PlayerData.instance.selectdRobotSkins[selectedRobotType].SkinType);


        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        Debug.Log("Exit StartMatching");
        Destroy(gameObject.GetComponent<PhaseBase>());
    }

    public override IEnumerator Action(GameData game)
    {
        AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<MatchingScreen_Popup>();
        yield return new WaitUntil(() => handle.IsDone);
        matchingScreen = handle.Result.GetComponent<MatchingScreen_Popup>();

        phaseDone = true;
        yield break;
    }
    public void RenewWaitingPlayerCount(int waitingPlayerCount)
    {
        connectedPlayerCount = waitingPlayerCount;
    }

}
