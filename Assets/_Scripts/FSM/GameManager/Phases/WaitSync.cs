using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitSync : PhaseBase
{

    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter WaitSync");

        type = Define.Phase.WaitSync;

        GameController.instance.gameData.behaviours.Clear();
        //Volt_GameManager.S.screenBlockPanel.SetActive(true);
        //Volt_GMUI.S.scrrenBlockPanel.SetActive(true);
        Managers.UI.ShowPopupUIAsync<ReconnectWaitBlock_Popup>();


        Volt_GMUI.S.IsTickOn = false;
        Volt_GMUI.S.TickTimer = 99;

        StartCoroutine(Action(game.gameData));    
    }
    public override void OnExitPhase(GameController game)
    {
        Destroy(gameObject.GetComponent<PhaseBase>());
    }

    public override IEnumerator Action(GameData data)
    {
        yield break;
    }
}
