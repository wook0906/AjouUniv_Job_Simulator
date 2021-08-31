using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlaceRobot : PhaseBase
{
    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter PlaceRobot");
        type = Define.Phase.PlaceRobot;

        Volt_GMUI.S._3dObjectInteractable = true;

        if (game.gameData.mapType == Define.MapType.Tokyo)
            Volt_ArenaSetter.S.DisappearWalls();

        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        Volt_GMUI.S._3dObjectInteractable = false;
        Destroy(gameObject.GetComponent<PhaseBase>());
    }
    public override void OnTouchTileEnd(Volt_Tile tile)
    {
        if (!tile.BlinkOn)
            return;
        if (Volt_PlayerManager.S.I.startingTiles.Contains(tile) && Volt_PlayerManager.S.I.playerRobot == null)
        {
            Volt_PlayerManager.S.SendCharacterPositionPacket(Volt_PlayerManager.S.I.playerNumber, tile.tilePosInArray.x, tile.tilePosInArray.y);

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
        Volt_GMUI.S.IsTickOn = true;
        Volt_GMUI.S.TickTimer = game.placeRobotTime;

        //noticeText.text = "Place your robot...!";
        if (Volt_ArenaSetter.S.robotsInArena.Count != game.MaxPlayer)
        {
            if (!Volt_PlayerManager.S.I.GetRobot())
                Volt_GMUI.S.guidePanel.ShowGuideTextMSG(GuideMSGType.RobotSetup, true, Application.systemLanguage);
            else
                Volt_GMUI.S.guidePanel.ShowGuideTextMSG(GuideMSGType.WaitPlaceOtherPlayerRobot, true, Application.systemLanguage);
        }

        yield return StartCoroutine(WaitPlaceRobot(game));



        phaseDone = true;
    }


    IEnumerator WaitPlaceRobot(GameData data)
    {

        if (!Volt_PlayerManager.S.I.playerRobot)
        {
            Volt_PlayerManager.S.I.playerCamRoot.SetSaturationDown(false);
            Volt_PlayerManager.S.I.playerCamRoot.CamInit();
            
            if (IsFullStartingTiles())
            {
                AutoRobotSetup(Volt_PlayerManager.S.I.playerNumber);
            }
            else
            {
                foreach (var item in Volt_PlayerManager.S.I.startingTiles)
                {
                    if (item.GetRobotInTile() == null)
                    {
                        item.SetDefaultBlinkOption();
                        item.BlinkOn = true;
                    }
                }
            }
        }

        if (Volt_PlayerManager.S.I.PlayerType == PlayerType.HOSTPLAYER && !IsAllUserHasRobot())
        {
            foreach (var item in Volt_PlayerManager.S.GetPlayers())
            {
                if (item.playerRobot == null)
                {
                    if (item.PlayerType == PlayerType.AI)
                    {
                        AutoRobotSetup(item.playerNumber);
                    }
                    else if (!item.IsMobileActivated)
                    {
                        AutoRobotSetup(item.playerNumber);
                    }
                }
            }
        }


        StartCoroutine(FlushPlaceRobotRequest(data));
        yield return new WaitUntil(() => IsAllUserHasRobot() || Volt_GMUI.S.TickTimer == 0);

        //로봇은 모두 배치되지 않았지만, 타이머가 모두 돌아서 코드가 진행된 경우.
        if (!IsAllUserHasRobot() && Volt_PlayerManager.S.I.PlayerType == PlayerType.HOSTPLAYER)
        {

            foreach (var item in Volt_PlayerManager.S.GetPlayers()) //나말고 다른애들이 로봇이 없으니까 자동배치 시켜주고.
            {
                if ((!item.playerRobot && !item.IsMobileActivated) || (!item.playerRobot && item.isNeedSynchronization))
                    //item.AutoRobotPlace();
                    AutoRobotSetup(item.playerNumber);
            }

            if (!Volt_PlayerManager.S.I.playerRobot) //내꺼 없으면 자동생성하고
            {
                //Volt_PlayerManager.S.I.AutoRobotPlace();
                AutoRobotSetup(Volt_PlayerManager.S.I.playerNumber);
            }
        }

        yield return new WaitUntil(() => IsAllUserHasRobot() && SimulationObserver.Instance.IsAllRobotIdleState());

        if (data.round == 1)
        {
            Card exclusiveCardType = Card.NONE;
            ModuleType exclusiveModuleType = ModuleType.Max;
            RobotType exclusiveRobotType = RobotType.Max;
            switch (data.mapType)
            {
                case Define.MapType.TwinCity:
                    exclusiveCardType = Card.SAWBLADE;
                    exclusiveModuleType = ModuleType.Attack;
                    exclusiveRobotType = RobotType.Hound;
                    break;
                case Define.MapType.Rome:
                    exclusiveCardType = Card.ANCHOR;
                    exclusiveModuleType = ModuleType.Tactic;
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
                if (item.playerInfo.RobotType != exclusiveRobotType) continue;
                Volt_ModuleCardBase exclusiveModule = Instantiate(Volt_ModuleDeck.S.GetModulePrefab(exclusiveModuleType, exclusiveCardType));
                exclusiveModule.isNeedReturnToDeck = false;
                item.OnPickupNewModuleCard(exclusiveModule);
            }
        }

        Volt_GMUI.S.IsTickOn = false;

        PacketTransmission.SendCharacterPositionCompletionPacket();
    }

    bool IsFullStartingTiles()
    {
        foreach (var item in Volt_PlayerManager.S.I.startingTiles)
        {
            if (!item.GetRobotInTile())
                return false;
        }
        return true;
    }

    public void AutoRobotSetup(int playerNumber)
    {
        Volt_PlayerInfo player = Volt_PlayerManager.S.GetPlayerByPlayerNumber(playerNumber);

        Volt_Tile targetTile = Volt_ArenaSetter.S.GetRandomTileToPlace(player.startingTiles);

        Volt_PlayerManager.S.SendCharacterPositionPacket(playerNumber, targetTile.tilePosInArray.x, targetTile.tilePosInArray.y);

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
            }
        }

        
        data.placeRobotRequestDatas.Clear();
    }
}
