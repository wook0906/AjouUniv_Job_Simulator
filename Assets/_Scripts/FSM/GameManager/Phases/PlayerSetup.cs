using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : PhaseBase
{
    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter PlayerSetup");
        type = Define.Phase.PlayerSetup;

        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        Destroy(gameObject.GetComponent<PhaseBase>());
    }

    public override IEnumerator Action(GameData game)
    {
        yield return new WaitUntil(() => Volt_PlayerManager.S.GetPlayers().Count == game.MaxPlayer);

        List<Volt_PlayerInfo> players = Volt_PlayerManager.S.GetPlayers();

        for (int i = 0; i < players.Count; i++)
        {
            players[i].playerCam = players[i].GetComponentInChildren<Camera>();
            players[i].playerCamRoot = players[i].GetComponentInChildren<CameraMovement>();
            players[i].raycaster = players[i].GetComponent<Volt_ScreenRaycaster>();
            if (players[i] != Volt_PlayerManager.S.I)
            {
                players[i].playerCam.gameObject.SetActive(false);
                players[i].playerCamRoot.enabled = false;
                players[i].raycaster.enabled = false;
            }
        }

        foreach (var item in Volt_PlayerManager.S.GetPlayers())
        {
            List<int> tileIdxList = Volt_ArenaSetter.S.playerStartTileIdxData.GetTileIdxData(item.playerNumber);

            foreach (var idx in tileIdxList)
            {
                item.startingTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(idx));
            }

            switch (item.playerNumber)
            {
                case 2:
                    item.playerCamRoot.rotor.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                    break;
                case 3:
                    item.playerCamRoot.rotor.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    break;
                case 4:
                    item.playerCamRoot.rotor.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                    break;
                default:
                    break;
            }
        }
        Volt_PlayerManager.S.I.playerCamRoot.Init();

        yield return new WaitUntil(() => Volt_PlayerManager.S.GetPlayers().Count == game.MaxPlayer);

        foreach (var item in Volt_PlayerManager.S.GetPlayers())
        {
            item.playerCam.enabled = true;
        }
        //Volt_GMUI.S.matchingScreenPanel.SetActive(false);
        Managers.UI.CloseAllPopupUI();

        ModuleType moduleType = Volt_ModuleDeck.S.GetRandomModuleType();
        Volt_ModuleCardBase cardBase = Volt_ModuleDeck.S.DrawRandomCard(moduleType);

        if (PlayerPrefs.GetInt("Volt_TutorialDone") == 1 && PlayerPrefs.GetInt("Volt_TrainingMode") != 1)
            PacketTransmission.SendFieldReadyCompletionPacket(cardBase.card);
        else if(PlayerPrefs.GetInt("Volt_TutorialDone") == 0 && PlayerPrefs.GetInt("Volt_TrainingMode") != 1)
            GameController.instance.ChangePhase<TutorialItemSetup>();
        else if(PlayerPrefs.GetInt("Volt_TrainingMode") == 1)
        {
            GameController.instance.gameData.drawedCard = Card.NONE;
            GameController.instance.gameData.vpIdx = 0;
            GameController.instance.gameData.repairIdx = 0;
            GameController.instance.gameData.moduleIdx = 0;
            GameController.instance.ChangePhase<ItemSetup>();
        }
        phaseDone = true;
    }
}
