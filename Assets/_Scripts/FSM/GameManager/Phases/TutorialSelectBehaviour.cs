using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TutorialSelectBehaviour : PhaseBase
{
    bool myBehaviourSelectDone = false;
    Volt_RobotBehavior behaviour;

    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter TutorialSelectBehaviour");

        type = Define.Phase.SelectBehaviour;
        
        foreach (var item in Volt_ArenaSetter.S.robotsInArena)
        {
            item.panel.IndicateControl(true);
        }
        behaviour = new Volt_RobotBehavior();

        if (game.gameData.round == 2)
            Volt_PlayerUI.S.BehaviourSelectOn(BehaviourType.Attack);
        Volt_PlayerUI.S.BehaviourSelectOn(BehaviourType.Move);
        Volt_PlayerUI.S.ShowModuleButton(true);

        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        Volt_PlayerUI.S.BehaviourSelectOff();
        Volt_PlayerUI.S.ShowModuleButton(false);
        Managers.UI.ClosePopupUI();
        Destroy(gameObject.GetComponent<PhaseBase>());
    }

    public override IEnumerator Action(GameData game)
    {
        

        switch (game.round)
        {
            case 1:
                while (TutorialData.S.curTutorialIdx <= 2)
                {
                    AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<TutorialExplaination_Popup>();
                    yield return new WaitUntil(() => handle.IsDone);
                    TutorialExplaination_Popup popUp = handle.Result.GetComponent<TutorialExplaination_Popup>();
                    popUp.SetWindow(FindObjectOfType<TutorialData>().datas[TutorialData.S.curTutorialIdx]);
                    yield return new WaitUntil(() => TutorialData.S.isOnTutorialPopup == false);
                }
                break;
            case 2:
                while (TutorialData.S.curTutorialIdx <= 7)
                {
                    AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<TutorialExplaination_Popup>();
                    yield return new WaitUntil(() => handle.IsDone);
                    TutorialExplaination_Popup popUp = handle.Result.GetComponent<TutorialExplaination_Popup>();
                    popUp.SetWindow(FindObjectOfType<TutorialData>().datas[TutorialData.S.curTutorialIdx]);
                    yield return new WaitUntil(() => TutorialData.S.isOnTutorialPopup == false);
                }
                break;
            case 3:
                while (TutorialData.S.curTutorialIdx <= 12)
                {
                    AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<TutorialExplaination_Popup>();
                    yield return new WaitUntil(() => handle.IsDone);
                    TutorialExplaination_Popup popUp = handle.Result.GetComponent<TutorialExplaination_Popup>();
                    popUp.SetWindow(FindObjectOfType<TutorialData>().datas[TutorialData.S.curTutorialIdx]);
                    yield return new WaitUntil(() => TutorialData.S.isOnTutorialPopup == false);
                }
                break;
            case 4:
                while (TutorialData.S.curTutorialIdx <= 18)
                {
                    AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<TutorialExplaination_Popup>();
                    yield return new WaitUntil(() => handle.IsDone);
                    TutorialExplaination_Popup popUp = handle.Result.GetComponent<TutorialExplaination_Popup>();
                    popUp.SetWindow(FindObjectOfType<TutorialData>().datas[TutorialData.S.curTutorialIdx]);
                    yield return new WaitUntil(() => TutorialData.S.isOnTutorialPopup == false);
                }
                break;
            default:
                break;
        }
        

        yield return new WaitUntil(() => myBehaviourSelectDone);

        Volt_PlayerUI.S.BehaviourSelectOff();

        phaseDone = true;
        GameController.instance.ChangePhase<TutorialSelectRange>();
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
    public void SelectBehaviourDoneCallbackForModule(BehaviourType behaviourType, int slotNumber)
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
        Volt_Robot robot = Volt_PlayerManager.S.I.playerRobot.GetComponent<Volt_Robot>();
        robot.moduleCardExcutor.SetOnActiveCard(robot.moduleCardExcutor.GetCurEquipCards()[slotNumber], slotNumber);


        TutorialData.S.curTutorialIdx++;
        TutorialData.S.isOnTutorialPopup = false;
        Volt_GMUI.S._3dObjectInteractable = true;
        
        Managers.UI.ClosePopupUI();

        Volt_PlayerUI.S.BehaviourSelectOff();
        myBehaviourSelectDone = true;
    }
}
