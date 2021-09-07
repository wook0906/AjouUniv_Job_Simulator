using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItemSetup : PhaseBase
{
    int vpPlaceIdx = 0;
    int modulePlaceIdx = 0;
    int kitPlaceIdx = 0;


    bool isVpSetupDone = false;
    bool isRepairkitSetupDone = false;
    bool isModuleSetupDone = false;

    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter TutorialItemSetup");
        type = Define.Phase.ItemSetup;

        Volt_PlayerUI.S.MyModuleSetupStateInit();
        Volt_GMUI.S.RoundNumber++;

        switch (game.gameData.round)
        {
            case 1:
                modulePlaceIdx = 40;
                break;
            case 2:
                kitPlaceIdx = 49;
                break;
            case 5:
                vpPlaceIdx = 58;
                break;
            default:
                break;
        }

        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        Destroy(gameObject.GetComponent<PhaseBase>());
    }
    public override IEnumerator Action(GameData game)
    {
        yield return StartCoroutine(ItemSetupCoroutine(game));

        phaseDone = true;

        GameController.instance.ChangePhase<TutorialPlaceRobot>();
    }
    IEnumerator ItemSetupCoroutine(GameData data)
    {
        if (vpPlaceIdx != 0)
            SetVPInTile(Volt_ArenaSetter.S.GetTileByIdx(vpPlaceIdx));
        else
            isVpSetupDone = true;

        yield return new WaitUntil(() => isVpSetupDone);
        isVpSetupDone = false;

        if (kitPlaceIdx != 0)
            SetRepairKitInTile(Volt_ArenaSetter.S.GetTileByIdx(kitPlaceIdx));
        else
            isRepairkitSetupDone = true;

        yield return new WaitUntil(() => isRepairkitSetupDone);
        isRepairkitSetupDone = false;

        if (modulePlaceIdx != 0)
            SetModuleInTile(Volt_ArenaSetter.S.GetTileByIdx(modulePlaceIdx), Card.DOUBLEATTACK);
        else
            isModuleSetupDone = true;
        yield return new WaitUntil(() => isModuleSetupDone);
        isModuleSetupDone = false;

        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => !Volt_PlayerManager.S.I.playerCamRoot.isMoving);
    }

    void SetVPInTile(Volt_Tile tile)
    {
        tile.SetVictoryPoint();
        isVpSetupDone = true;
    }
    void SetRepairKitInTile(Volt_Tile tile)
    {
        tile.SetRepairKit();
        isRepairkitSetupDone = true;
    }
    void SetModuleInTile(Volt_Tile tile, Card cardType)
    {
        tile.SetModule(cardType);
        isModuleSetupDone = true;
    }

}
