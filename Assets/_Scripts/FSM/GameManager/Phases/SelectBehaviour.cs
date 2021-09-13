using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBehaviour : PhaseBase
{
    bool myBehaviourSelectDone = false;
    Volt_RobotBehavior behaviour;

    public override void OnEnterPhase(GameController game)
    {
        
        Debug.Log("Enter SelectBehaviour");

        type = Define.Phase.SelectBehaviour;
        
        foreach (var item in Volt_ArenaSetter.S.robotsInArena)
        {
            item.panel.IndicateControl(true);
        }
        behaviour = new Volt_RobotBehavior();

        Volt_GMUI.S.IsTickOn = true;
        Volt_GMUI.S.TickTimer = game.gameData.selectBehaviourTime;

        Volt_PlayerUI.S.BehaviourSelectOn(BehaviourType.Both);
        Volt_PlayerUI.S.ShowModuleButton(true);

        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        Volt_PlayerUI.S.BehaviourSelectOff();
        Volt_PlayerUI.S.ShowModuleButton(false);
        
        Destroy(gameObject.GetComponent<PhaseBase>());
    }

    public override IEnumerator Action(GameData game)
    {
        yield return new WaitUntil(() => myBehaviourSelectDone || Volt_GMUI.S.TickTimer == 0);

        Volt_PlayerUI.S.BehaviourSelectOff();

        if (myBehaviourSelectDone)
        {
            phaseDone = true;
            GameController.instance.ChangePhase<SelectRange>();
            //SelectRangeStart();
        }
        else //내가 잠수를 타버린 경우.
        {
            Volt_PlayerInfo pInfo = Volt_PlayerManager.S.I;
            behaviour.SetBehaivor(0, BehaviourType.None, Volt_PlayerManager.S.I.playerNumber);
            Volt_PlayerManager.S.SendBehaviorOrderPacket(pInfo.playerNumber, behaviour);


            if (Volt_PlayerManager.S.I.PlayerType == PlayerType.HOSTPLAYER)
            {
                foreach (var item in Volt_PlayerManager.S.GetPlayers())
                {
                    if (item.PlayerType == PlayerType.PLAYER && !item.IsMobileActivated)
                    {
                        Volt_PlayerManager.S.SendBehaviorOrderPacket(item.playerNumber, behaviour);
                    }
                }
                DoAllKillbotsDetection();
            }

            foreach (var item in Volt_ArenaSetter.S.robotsInArena)
            {
                item.panel.IndicateControl(false);
            }

            yield return new WaitUntil(() => IsInputBehaviourAllPlayer());
            PacketTransmission.SendBehaviorOrderCompletionPacket();

            foreach (var item in Volt_PlayerManager.S.I.startingTiles)
            {
                item.BlinkOn = false;
            }
            yield return new WaitUntil(() => Volt_ArenaSetter.S.IsAllTileBlinkOff());

            phaseDone = true;
        }
    }
    void DoAllKillbotsDetection()
    {
        foreach (Volt_Robot robot in Volt_ArenaSetter.S.robotsInArena)
        {
            if (robot.playerInfo.PlayerType == PlayerType.AI)
            {
                Volt_Robot killbot = robot.gameObject.GetComponent<Volt_Robot>();
                killbot.DetectRobot();
            }
        }
    }
    bool IsInputBehaviourAllPlayer()
    {
        int curNumOfBehaviour = GameController.instance.gameData.behaviours.Count;

        if (curNumOfBehaviour >= Volt_PlayerManager.S.GetCurrentNumberOfActive())
        {
            return true;
        }
        else
        {
            Debug.Log(curNumOfBehaviour + ", " + Volt_PlayerManager.S.GetCurrentNumberOfActive());
        }
        return false;
    }

    public void SelectBehaviourDoneCallback(GameObject btnType) // 0 = move , 1 = atk
    {
        if (btnType.name == "Move")
        {
            behaviour.SetBehaivor(0, BehaviourType.Move, Volt_PlayerManager.S.I.playerNumber);
        }
        else if (btnType.name == "Attack")
        {
            behaviour.SetBehaivor(0, BehaviourType.Attack, Volt_PlayerManager.S.I.playerNumber);
        }
        else
        {
            behaviour.SetBehaivor(0, BehaviourType.None, Volt_PlayerManager.S.I.playerNumber);
        }
        myBehaviourSelectDone = true;
    }
    public void SelectBehaviourDoneCallbackForModule(BehaviourType behaviourType) // 0 = move , 1 = atk
    {
        //if (IsTutorialMode)
        //{
        //    if (Volt_TutorialManager.S && RoundNumber == 1)
        //    {
        //        Volt_TutorialManager.S.FindContentsByName("WaitSelectBehaviour").gameObject.SetActive(false);
        //        //Debug.Log("WaitSelectBehaviour 끝");
        //    }
        //}
        if (behaviourType == BehaviourType.Move)
        {
            behaviour.SetBehaivor(0, BehaviourType.Move, Volt_PlayerManager.S.I.playerNumber);
        }
        else if (behaviourType == BehaviourType.Attack)
        {
            behaviour.SetBehaivor(0, BehaviourType.Attack, Volt_PlayerManager.S.I.playerNumber);
        }
        else
        {
            behaviour.SetBehaivor(0, BehaviourType.None, Volt_PlayerManager.S.I.playerNumber);
        }
        if(!GameController.instance.gameData.behaviours.ContainsKey(Volt_PlayerManager.S.I.playerNumber))
        {
            GameController.instance.gameData.behaviours.Add(Volt_PlayerManager.S.I.playerNumber, behaviour);
        }

        Volt_PlayerUI.S.BehaviourSelectOff();
        myBehaviourSelectDone = true;
    }
}
