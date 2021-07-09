using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : PhaseBase
{
    public Stack<Volt_RobotBehavior> behaviourStack;
    bool isPlayAmargeddon = false;
    int optionIdx = 0;

    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter Simulation");

        type = Define.Phase.Simulation;

        behaviourStack = new Stack<Volt_RobotBehavior>();

        SortedRobotList(game.gameData);
        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        Destroy(gameObject.GetComponent<PhaseBase>());
    }

    public override IEnumerator Action(GameData data)
    {
        yield return new WaitUntil(() => !Volt_PlayerManager.S.I.playerCamRoot.isMoving);
        //if (isGameOverWaiting)
        //    yield break;
        //Debug.Log("시뮬레이션 시작");

        ShowRobotSimulationOrder();
        while (behaviourStack.Count > 0)
        {
            //if (isGameOverWaiting)
            //    break;

            yield return new WaitUntil(() => Volt_GMUI.S.IsCheatPanelOn == false);
 
            foreach (var item in Volt_ArenaSetter.S.robotsInArena)
            {
                item.PushType = PushType.PushedCandidate;
            }
            Volt_RobotBehavior currentBehaviour = behaviourStack.Pop();
            int currentBehaviourPlayerNumber = currentBehaviour.PlayerNumber;
            yield return StartCoroutine(RobotDoBehaviour(currentBehaviour));
            Volt_GamePlayData.S.ClearOtherRobotsAttackedByRobotsOnThisTurn();

            yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff());
            yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotIdleState());

            SimulationObserver.Instance.currentPusher = null;
            if (data.AmargeddonCount > 0 && data.AmargeddonPlayer != 0)
            {
                //Debug.Log("Simulate : " + option.ToString() + "," + option2.ToString() + "," + option3.ToString() + "," + option4.ToString());
                //Debug.Log(AmargeddonPlayer +" : " + AmargeddonCount + "번 남았으");
                yield return StartCoroutine(WaitAmargeddon(data));
                Volt_GamePlayData.S.ClearOtherRobotsAttackedByRobotsOnThisTurn();
            }

            yield return new WaitUntil(() => Volt_PlayerManager.S.I.playerCamRoot.isMoving == false);
            yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff());
            yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotIdleState());
            foreach (var item in Volt_ArenaSetter.S.robotsInArena)
            {
                item.fsm.attackInfo = null;
            }
        }
        //카메라 원래대로 돌려놔야함
        yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff());
        yield return new WaitUntil(() => Volt_ArenaSetter.S.IsAllTileBlinkOff());


        RobotOrderNumberInit(); //로봇 행동순서 UI 초기화

        if (!GameController.instance.gameData.isGameOverWaiting)
        {
            yield return StartCoroutine(WaitTimeBombCount()); //시한폭탄 처리 기다림
            yield return new WaitUntil(() => Volt_ArenaSetter.S.IsAllTileBlinkOff());

            Volt_GamePlayData.S.ClearOtherRobotsAttackedByRobotsOnThisTurn();

            if (data.isOnSuddenDeath)
            {
                GameController.instance.ChangePhase(new SuddenDeath());
                phaseDone = true;
                yield break;
            }

            foreach (var item in Volt_ArenaSetter.S.robotsInArena)
            {
                item.PushType = PushType.PushedCandidate;
            }

            yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff());

            Volt_GamePlayData.S.ClearOtherRobotsAttackedByRobotsOnThisTurn();
                PacketTransmission.SendSimulationCompletionPacket();
        }
        data.behaviours.Clear();
        //data.isLocalSimulationDone = true;
        phaseDone = true;
    }

    IEnumerator WaitTimeBombCount()
    {
        //foreach (var item in Volt_ArenaSetter.S.GetTileArray())
        //{
        //    if (item.TimeBombInstance)
        //        item.TimeBombInstance.CountDown();
        //}
        foreach (var item in FindObjectsOfType<Volt_TimeBomb>())
        {
            item.CountDown();
        }
        yield return new WaitUntil(() => SimulationObserver.Instance.IsAllTimeBombPlayEnd());
        yield return new WaitUntil(() =>SimulationObserver.Instance.IsAllRobotIdleState());
        foreach (var item in Volt_ArenaSetter.S.GetTileArray())
        {
            item.SetDefaultBlinkOption();
            item.BlinkOn = false;
        }
        foreach (var item in Volt_ArenaSetter.S.robotsInArena)
        {
            item.lastAttackPlayer = 0;
        }
    }

   
    IEnumerator RobotDoBehaviour(Volt_RobotBehavior behavior)
    {
        if (behavior.BehaviourType == BehaviourType.None ||
            behavior.BehaviorPoints == 0) yield break;

        //print("player : " + behavior.PlayerNumber + ", " + ", " + behavior.Direction + ", " + behavior.BehaviorPoints + ", " + behavior.BehaviourType);
        Volt_PlayerInfo player = Volt_PlayerManager.S.GetPlayerByPlayerNumber(behavior.PlayerNumber);
        if (player.playerRobot == null) yield break;


        SimulationObserver.Instance.OnBehaviorFlag(player.playerNumber);
        Volt_Robot robot = player.playerRobot.GetComponent<Volt_Robot>();
        // 로봇의 행동이 이동이면 해당 로봇을 Pusher로 설정한다.
        if (behavior.BehaviourType == BehaviourType.Move)
        {
            robot.PushType = PushType.Pusher;
        }
        SimulationObserver.Instance.currentPusher = robot;

        robot.fsm.behavior = behavior;

        yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff());

        //Debug.Log("Cehck player vp");
        
            //if (isEndlessGame)
            //    PacketTransmission.SendVictoryPointPacket(robot.playerInfo.playerNumber, 0);
            //else
            //  PacketTransmission.SendVictoryPointPacket(robot.playerInfo.playerNumber, robot.playerInfo.VictoryPoint);

        foreach (var item in Volt_ArenaSetter.S.robotsInArena)
        {
            if (robot != null)
                item.standingTile = Volt_ArenaSetter.S.GetTile(robot.transform.position);
        }
        foreach (var item in Volt_ArenaSetter.S.GetTileArray())
        {
            item.SetDefaultBlinkOption();
            item.BlinkOn = false;
        }

        foreach (var item in Volt_ArenaSetter.S.robotsInArena)
        {
            item.lastAttackPlayer = 0;
        }
    }

    IEnumerator WaitAmargeddon(GameData data)
    {
        Volt_Robot armageddonRobot = Volt_PlayerManager.S.GetPlayerByPlayerNumber(data.AmargeddonPlayer).playerRobot.GetComponent<Volt_Robot>();
        int[] randomTargetTileIdx = data.randomOptionValues.ToArray();

        --data.AmargeddonCount;
        isPlayAmargeddon = true;
        Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/SFx_200703/Amargeddon.wav",
            (result) =>
            {
                Volt_SoundManager.S.RequestSoundPlay(result.Result, false);
            });

        GameObject amargeddonGo = Volt_PrefabFactory.S.PopObject(Define.Objects.Armargeddon).gameObject;

        // = Instantiate(amargeddonPrefab);
        //Volt_Tile bombSite = Volt_ArenaSetter.S.SearchBombSite();

        //Debug.Log("WaitAmargeddon : " + options[optionIdx]);

        Volt_Tile targetTile = Volt_ArenaSetter.S.GetTileByIdx(randomTargetTileIdx[optionIdx]);
        optionIdx++;
        List<Volt_Tile> bombSites = new List<Volt_Tile>(targetTile.GetAdjecentTiles());
        bombSites.Add(targetTile);
        foreach (var bombSite in bombSites)
        {
            if (bombSite == null) continue;
            bombSite.SetBlinkOption(BlinkType.Attack, 0.3f);
            bombSite.BlinkOn = true;
        }

        if (targetTile == null)
        {
            //Debug.LogWarning("BombSite is null");
            List<Volt_Robot> robots = Volt_ArenaSetter.S.robotsInArena;
            int idx = Random.Range(0, robots.Count);

            List<Volt_Tile> tiles = new List<Volt_Tile>();
            tiles.Add(Volt_ArenaSetter.S.GetTile(robots[idx].transform.position));

            List<Volt_Tile> adjTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTile(robots[idx].transform.position).GetAdjecentTiles());
            for (int i = 0; i < adjTiles.Count; i++)
            {
                if (adjTiles[i] == null)
                    continue;
                tiles.Add(adjTiles[i]);
            }

            idx = Random.Range(0, tiles.Count);
            targetTile = tiles[idx];
        }
        amargeddonGo.GetComponent<Volt_Amargeddon>().Init(targetTile);

        yield return new WaitUntil(() => { return !isPlayAmargeddon; });
        yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff());
        foreach (var item in Volt_ArenaSetter.S.GetTileArray())
        {
            item.SetDefaultBlinkOption();
            item.BlinkOn = false;
        }
        if (data.AmargeddonCount == 0)
        {
            if (data.AmargeddonPlayer != 0)
                armageddonRobot.moduleCardExcutor.DestroyCard(Card.AMARGEDDON);
        }
        foreach (var item in Volt_ArenaSetter.S.robotsInArena)
        {
            item.lastAttackPlayer = 0;
        }
    }

    private void SortedRobotList(GameData data)
    {
        List<Volt_RobotBehavior> distinctBehaviours = new List<Volt_RobotBehavior>();
        List<int> behaviourKeys = new List<int>();

        foreach (var item in data.behaviours)
        {
            if (!behaviourKeys.Contains(item.Key))
            {
                behaviourKeys.Add(item.Key);
                distinctBehaviours.Add(item.Value);
            }
        }

        for (int i = 0; i < distinctBehaviours.Count; i++)
        {
            int idx = i;
            // Behaviour type과 BehaviourPoints를 기준으로 행동 우선순위가 가장 높은 킬봇의 행동정보를 저장한다.
            // Behaviour type의 우선 순위는 공격이 이동보다 낮고, 이동은 정지 보다 우선순위가 낮다.(Stay > Move > Attack)
            // BehaviourPoints는 값이 낮을수록 우선 순위가 높다.
            Volt_RobotBehavior behaviour1 = distinctBehaviours[idx];
            // 최우선순위 번호
            int topPriorityNumber = behaviour1.BehaviorPoints;
            // 만약 행동 타입이 공격일 경우 추가로 6를 더한다. 
            //topPriorityNumber += behaviour1.BehaviourType == BehaviourType.Move ? 0 : 6; //201102 일짜 기준으로 이동과 공격의 우선정도를 같음으로 설정함에따라 해당 행을 주석으로 처리함.

            for (int j = i; j < distinctBehaviours.Count; j++)
            {
                // 비교대상이 되는 킬봇의 행동정보
                Volt_RobotBehavior behaviour2 = distinctBehaviours[j];
                if (behaviour2.BehaviourType == BehaviourType.None)
                    idx = j;

                int priorityNumber = behaviour2.BehaviorPoints;
                //priorityNumber += behaviour2.BehaviourType == BehaviourType.Move ? 0 : 6; //201102 일짜 기준으로 이동과 공격의 우선정도를 같음으로 설정함에따라 해당 행을 주석으로 처리함.

                // 계산된 priorityNumber의 값이 낮을수록 우선순위가 높다.
                // 비교대상의 우선순위 번호가 더 클경우
                if (priorityNumber > topPriorityNumber)
                    continue;
                // 비교대상의 우선순위 번호가 더 작을경우
                else if (priorityNumber < topPriorityNumber)
                {
                    topPriorityNumber = priorityNumber;
                    idx = j;
                }
                // 비교대상의 우선순위 번호가 같은경우
                else
                {
                    // 비교대상의 순서번호가 더 작을경우
                    if (behaviour2.OrderNumber < behaviour1.OrderNumber)
                    {
                        idx = j;
                    }
                }
            }
            if (i == idx)
                continue;
            Volt_RobotBehavior tbehaviour = distinctBehaviours[i];
            distinctBehaviours[i] = distinctBehaviours[idx];
            distinctBehaviours[idx] = tbehaviour;
        }
        for (int i = distinctBehaviours.Count - 1; i >= 0; i--)
        {
            behaviourStack.Push(distinctBehaviours[i]);
        }
    }
    void ShowRobotSimulationOrder()
    {
        List<Volt_RobotBehavior> tmpRobotBehaviourList = new List<Volt_RobotBehavior>(behaviourStack);
        int orderIdx = 1;
        foreach (var item in tmpRobotBehaviourList)
        {
            Volt_PlayerInfo player = Volt_PlayerManager.S.GetPlayerByPlayerNumber(item.PlayerNumber);
            Volt_Robot robot = player.playerRobot.GetComponent<Volt_Robot>();
            if (item.BehaviourType == BehaviourType.None)
            {
                robot.panel.OrderNumberSet(-1);
            }
            else
            {
                robot.panel.OrderNumberSet(orderIdx);
                orderIdx++;
            }
        }
    }
    void RobotOrderNumberInit()
    {
        foreach (var item in Volt_PlayerManager.S.GetPlayers())
        {
            if (item.playerRobot)
                item.playerRobot.GetComponent<Volt_Robot>().panel.OrderNumberSet(-1);
        }
    }
}
