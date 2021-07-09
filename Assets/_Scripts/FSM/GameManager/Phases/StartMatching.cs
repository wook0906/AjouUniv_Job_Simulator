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
