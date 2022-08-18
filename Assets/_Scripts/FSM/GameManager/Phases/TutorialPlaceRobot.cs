using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TutorialPlaceRobot : PhaseBase
{
    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter TutorialPlaceRobot");
        type = Define.Phase.PlaceRobot;

        

        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        Destroy(gameObject.GetComponent<PhaseBase>());
    }
    public override void OnTouchTileEnd(Volt_Tile tile)
    {
        if (!tile.BlinkOn)
            return;
        if (Volt_PlayerManager.S.I.startingTiles.Contains(tile) && Volt_PlayerManager.S.I.playerRobot == null)
        {
            //Volt_PlayerManager.S.SendCharacterPositionPacket(Volt_PlayerManager.S.I.playerNumber, tile.tilePosInArray.x, tile.tilePosInArray.y);
            RobotPlaceData data;
            data.playerNumber = 1;
            data.x = tile.tilePosInArray.x;
            data.y = tile.tilePosInArray.y;
            GameController.instance.gameData.placeRobotRequestDatas.Add(data);

            foreach (var item in Volt_PlayerManager.S.I.startingTiles)
            {
                //item.responseParticle.Stop();
                //item.responseParticle.gameObject.SetActive(false);
                item.SetDefaultBlinkOption();
                item.BlinkOn = false;
            }
        }
    }

    public override IEnumerator Action(GameData game)
    {
        if (game.round == 1)
        {
            while (TutorialData.S.curTutorialIdx <= 0)
            {
                AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<TutorialExplaination_Popup>(null,true,true);
                yield return new WaitUntil(() => handle.IsDone);
                TutorialExplaination_Popup popUp = handle.Result.GetComponent<TutorialExplaination_Popup>();
                popUp.SetWindow(FindObjectOfType<TutorialData>().datas[TutorialData.S.curTutorialIdx]);
                yield return new WaitUntil(() => TutorialData.S.isOnTutorialPopup == false);
            }
        }
        
        if (Volt_ArenaSetter.S.robotsInArena.Count != game.MaxPlayer)
        {
            if (!Volt_PlayerManager.S.I.GetRobot())
                Volt_GMUI.S.guidePanel.ShowGuideTextMSG(GuideMSGType.RobotSetup);
            else
                Volt_GMUI.S.guidePanel.ShowGuideTextMSG(GuideMSGType.WaitPlaceOtherPlayerRobot);
        }

        yield return StartCoroutine(WaitPlaceRobot(game));

        if (game.round == 1)
        {
            Card exclusiveCardType = Card.NONE;
            ModuleType exclusiveModuleType = ModuleType.Max;
            RobotType exclusiveRobotType = RobotType.Max;
            switch (game.mapType)
            {
                case Define.MapType.TwinCity:
                    exclusiveCardType = Card.SAWBLADE;
                    exclusiveModuleType = ModuleType.Attack;
                    exclusiveRobotType = RobotType.Hound;
                    break;
                case Define.MapType.Rome:
                    exclusiveCardType = Card.DODGE;
                    exclusiveModuleType = ModuleType.Movement;
                    exclusiveRobotType = RobotType.Mercury;
                    break;
                case Define.MapType.Ruhrgebiet:
                    exclusiveCardType = Card.SHIELD;
                    exclusiveModuleType = ModuleType.Tactic;
                    exclusiveRobotType = RobotType.Reaper;
                    break;
                case Define.MapType.Tokyo:
                    exclusiveCardType = Card.PERNERATE;
                    exclusiveModuleType = ModuleType.Attack;
                    exclusiveRobotType = RobotType.Volt;
                    break;
                default:
                    break;
            }

            foreach (var item in Volt_ArenaSetter.S.robotsInArena)
            {
                if (item.playerInfo == Volt_PlayerManager.S.I) continue;
                if (item.playerInfo.RobotType != exclusiveRobotType) continue;
                Volt_ModuleCardBase exclusiveModule = Instantiate(Volt_ModuleDeck.S.GetModulePrefab(exclusiveModuleType, exclusiveCardType));
                exclusiveModule.isNeedReturnToDeck = false;
                item.OnPickupNewModuleCard(exclusiveModule);
            }
        }
        phaseDone = true;

        GameController.instance.ChangePhase<TutorialSelectBehaviour>();
    }


    IEnumerator WaitPlaceRobot(GameData data)
    {
        if (!Volt_PlayerManager.S.I.playerRobot)
        {
            Volt_PlayerManager.S.I.playerCamRoot.SetSaturationDown(false);
            Volt_PlayerManager.S.I.playerCamRoot.CamInit();

            Volt_PlayerManager.S.I.startingTiles[2].SetDefaultBlinkOption();
            Volt_PlayerManager.S.I.startingTiles[2].BlinkOn = true;
        }


        foreach (var item in Volt_PlayerManager.S.GetPlayers())
        {
            if (item.playerRobot == null && item.PlayerType == PlayerType.AI)
            {
                AutoRobotSetup(item.playerNumber);
            }
        }

        StartCoroutine(FlushPlaceRobotRequest(data));

        yield return new WaitUntil(() => IsAllUserHasRobot() && SimulationObserver.Instance.IsAllRobotIdleState());
    }


    public void AutoRobotSetup(int playerNumber)
    {
        Volt_PlayerInfo player = Volt_PlayerManager.S.GetPlayerByPlayerNumber(playerNumber);

        Volt_Tile targetTile = Volt_ArenaSetter.S.GetTileByIdx(player.startingTiles[2].tileIndex);

        RobotPlaceData data;
        data.playerNumber = playerNumber;
        data.x = targetTile.tilePosInArray.x;
        data.y = targetTile.tilePosInArray.y;

        GameController.instance.gameData.placeRobotRequestDatas.Add(data);
    }
    public bool IsAllUserHasRobot()
    {
        foreach (var item in Volt_PlayerManager.S.GetPlayers())
        {
            if (item.playerRobot == null)
                return false;
        }
        return true;
    }
    IEnumerator FlushPlaceRobotRequest(GameData data)
    {
        yield return new WaitUntil(() => data.placeRobotRequestDatas.Count + Volt_ArenaSetter.S.robotsInArena.Count >= data.MaxPlayer);

        for (int i = 0; i < data.placeRobotRequestDatas.Count; i++)
        {
            RobotPlaceData placeData =  data.placeRobotRequestDatas[i];
            Volt_PlayerInfo player = Volt_PlayerManager.S.GetPlayerByPlayerNumber(placeData.playerNumber);
            if (player.playerRobot == null)
            {
                player.skinType = Volt_PrefabFactory.S.SkinTypeDecisionByRobotType(player.RobotType, player.skinType);
                AsyncOperationHandle<GameObject> makeRobotHandle = Managers.Resource.InstantiateAsync($"Robots/{player.RobotType}/{player.RobotType}_{player.skinType}.prefab");

                yield return new WaitUntil(() => { return makeRobotHandle.IsDone; });
                player.playerRobot = makeRobotHandle.Result;

                //player.playerRobot = Volt_PrefabFactory.S.CreateRobot(player.playerNumber, player.RobotType, player.skinType).gameObject;

                Volt_Robot robot = player.playerRobot.GetOrAddComponent<Volt_Robot>();
                robot.Init(player, Volt_ArenaSetter.S.GetTile(placeData.x, placeData.y));

                if(player.PlayerType == PlayerType.AI)
                    robot.HitCount = 1;
            }
        }

        
        data.placeRobotRequestDatas.Clear();
    }
}
