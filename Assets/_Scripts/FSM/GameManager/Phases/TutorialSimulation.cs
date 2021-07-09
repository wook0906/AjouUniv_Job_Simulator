using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSimulation : PhaseBase
{
    public Stack<Volt_RobotBehavior> behaviourStack;
    bool isPlayAmargeddon = false;
    int optionIdx = 0;

    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter TutorialSimulation");

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

        ShowRobotSimulationOrder();
        while (behaviourStack.Count > 0)
        {

            yield return new WaitUntil(() => Volt_GMUI.S.IsCheatPanelOn == false);

            foreach (var item in Volt_ArenaSetter.S.robotsInArena)
            {
                item.PushType = PushType.PushedCandidate;
            }
            Volt_RobotBehavior currentBehaviour = behaviourStack.Pop();
            int currentBehaviourPlayerNumber = currentBehaviour.PlayerNumber;
            yield return StartCoroutine(RobotDoBehaviour(currentBehaviour));

            yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff());
            yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotIdleState());

            yield return new WaitUntil(() => Volt_PlayerManager.S.I.playerCamRoot.isMoving == false);

            SimulationObserver.Instance.currentPusher = null;

            foreach (var item in Volt_ArenaSetter.S.robotsInArena)
            {
                item.fsm.attackInfo = null;
            }
        }
        //카메라 원래대로 돌려놔야함
        yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff());
        yield return new WaitUntil(() => Volt_ArenaSetter.S.IsAllTileBlinkOff());


        RobotOrderNumberInit(); //로봇 행동순서 UI 초기화

        yield return new WaitUntil(() => Volt_ArenaSetter.S.IsAllTileBlinkOff());

        foreach (var item in Volt_ArenaSetter.S.robotsInArena)
        {
            item.PushType = PushType.PushedCandidate;
        }

        yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff());

        data.behaviours.Clear();

        phaseDone = true;
        
        if(data.round >= 2)
            GameController.instance.ChangePhase(new TutorialSuddenDeath());
        else
            GameController.instance.ChangePhase(new TutorialResolutionTurn());
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

        Debug.Log("Cehck player vp");

        if (player.VictoryPoint >= 2)
        {
            phaseDone = true;
            GameController.instance.ChangePhase(new GameOver());
        }

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
