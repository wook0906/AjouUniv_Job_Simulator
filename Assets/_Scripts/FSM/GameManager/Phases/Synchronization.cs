using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synchronization : PhaseBase
{
    ModuleType moduleType;
    Volt_ModuleCardBase cardBase;

    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter Synchronization");

        type = Define.Phase.Synchronization;

        moduleType = Volt_ModuleDeck.S.GetRandomModuleType();
        cardBase = Volt_ModuleDeck.S.DrawRandomCard(moduleType);

        foreach (var item in Volt_ArenaSetter.S.GetTileArray())
        {
            item.DestroyModule();
            item.CoinDestroy();
            item.KitDestroy();
            //item.SetOffVoltage();
        }
        foreach (var item in Volt_ArenaSetter.S.robotsInArena)
        {
            item.moduleCardExcutor.DestroyCardAll();
            if (item.TimeBombInstance)
            {
                //Debug.Log($"{item.playerInfo.playerNumber} 기존 폭탄 존재");
                item.TimeBombInstance.GetComponent<Volt_TimeBomb>().Destroy();
            }
        }

        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {

        Destroy(gameObject.GetComponent<PhaseBase>());
    }

    public override IEnumerator Action(GameData data)
    {
        

        yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotIdleState() && SimulationObserver.Instance.IsAllRobotBehaviourFlagOff());
        yield return new WaitUntil(() => IsReceivedAllData());

        for (int i = 0; i < Volt_ArenaSetter.S.GetTileArray().Length; i++)
        {
            TileData tileData = data.tileDatas.Dequeue();
            Volt_Tile syncTile = Volt_ArenaSetter.S.GetTileByIdx(tileData.tileIdx);
            syncTile.Synchronization(tileData);
        }

        for (int i = 0; i < Volt_PlayerManager.S.GetPlayers().Count; i++)
        {
            PlayerData playerData = data.playerDatas.Dequeue();
            RobotData robotData = data.robotDatas.Dequeue();
            Volt_PlayerInfo player = Volt_PlayerManager.S.GetPlayerByPlayerNumber(playerData.playerNumber);
            player.Synchronization(playerData);

            if (playerData.isRobotAlive)
            {
                player.playerCamRoot.SetSaturationDown(false);
                if (player.GetRobot())
                {
                    player.GetRobot().SynchronizationStart(robotData);
                }
                else
                {
                    //TODO: Managers.Resources를 통해 로봇 생성
                    Managers.Resource.InstantiateAsync($"Robots/{player.RobotType}/{player.RobotType}_{player.skinType}.prefab",
                        (result) =>
                        {
                            player.playerRobot = result.Result;
                            Volt_Robot robot = player.playerRobot.GetOrAddComponent<Volt_Robot>();
                            robot.Init(player, null);
                            //Volt_PrefabFactory.S.CreateRobot(playerData.playerNumber, player.RobotType, robotData.skinType).gameObject;
                            player.GetRobot().SynchronizationStart(robotData);
                        });
                }
            }
            else
            {
                if (player.GetRobot())
                    player.GetRobot().ForcedKillRobot();
            }
            if (player.IsMobileActivated)
                player.isNeedSynchronization = false;
            else
                player.isNeedSynchronization = true;
        }

        Volt_GMUI.S.Synchronization(data.round);


        if(data.round >= 10 ) data.isOnSuddenDeath = true;
        if (data.mapType == Define.MapType.Ruhrgebiet)
        {
            //SuddenDeathOn 시켜야함.
            if (data.round >= 10 && data.round < 13)
            {
                foreach (var item in Volt_ArenaSetter.S.fallTiles1)
                {
                    item.Fall();
                }
                foreach (var item in Volt_PlayerManager.S.GetPlayers())
                {
                    Volt_PlayerManager.S.RuhrgebietChangeStartingTiles(item.playerNumber, 1);
                }
            }
            else if (data.round >= 13)
            {
                foreach (var item in Volt_ArenaSetter.S.fallTiles2)
                {
                    item.Fall();
                }

                foreach (var item in Volt_PlayerManager.S.GetPlayers())
                {
                    Volt_PlayerManager.S.RuhrgebietChangeStartingTiles(item.playerNumber, 2);
                }
            }
        }

        //yield return StartCoroutine(Volt_SynchronizationHandler.S.DelayedSync(armageddonCount, armageddonPlayer));
        //yield return new WaitUntil(() => Volt_SynchronizationHandler.S.isSyncDone);
        //Volt_SynchronizationHandler.S.isSyncDone = false;

        //Debug.Log("PostProcess Sync done");

        if (!CommunicationInfo.IsMobileActive)
        {
            CommunicationInfo.IsMobileActive = true;
            PacketTransmission.SendTotalTurnOverPacket(null, null, null);
        }
        CommunicationInfo.IsMobileActive = true;
        Volt_PlayerManager.S.I.isNeedSynchronization = false;
        //Volt_GMUI.S.syncWaitblockPanel.SetActive(false);
        //Managers.UI.ShowPopupUIAsync<ReconnectWaitBlock_Popup>();

        Volt_ArenaSetter.S.needSyncTiles.Clear();
        

        //Debug.Log("All Sync Done");
        for (int i = 0; i < 4; i++)
        {
            Volt_PlayerManager.S.ReturnPlayerKey(i + 1);
        }
        PacketTransmission.SendFieldReadyCompletionPacket(cardBase.card);
        phaseDone = true;
    }
    bool IsReceivedAllData()
    {
        if (GameController.instance.gameData.tileDatas.Count == 81 && GameController.instance.gameData.robotDatas.Count == 4 && GameController.instance.gameData.playerDatas.Count == 4)
            return true;
        return false;
    }
}
