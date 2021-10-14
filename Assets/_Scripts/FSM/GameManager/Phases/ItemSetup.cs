using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSetup : PhaseBase
{
    bool isVpSetupDone = false;
    bool isRepairkitSetupDone = false;
    bool isModuleSetupDone = false;
    bool isVoltageSpaceSetupDone = false;

    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter ItemSetup");
        type = Define.Phase.ItemSetup;

        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        Destroy(gameObject.GetComponent<PhaseBase>());
    }
    public override IEnumerator Action(GameData game)
    {
        //Debug.Log("ItemSetupStart");

        Volt_PlayerUI.S.MyModuleSetupStateInit();
        Volt_GMUI.S.RoundNumber++;

        //noticeText.text = "Item Setting...";

        if (Volt_ArenaSetter.S.numOfVPSetupTile == 0)
            game.remainRoundCountToVpSetup--;

        yield return StartCoroutine(ItemSetupCoroutine(game));

        phaseDone = true;

        GameController.instance.ChangePhase<PlaceRobot>();
    }
    IEnumerator ItemSetupCoroutine(GameData data)
    {
        if (data.vpIdx != 0)
        {
            if (GameController.instance.gameData.remainRoundCountToVpSetup == 0)
                SetVPInTile(Volt_ArenaSetter.S.GetTileByIdx(data.vpIdx));
            else
                isVpSetupDone = true;
        }
        else
            isVpSetupDone = true;
        //Debug.Log("VP Setup");
        yield return new WaitUntil(() => isVpSetupDone);
        //Debug.Log("VP Setup Done");

        if (data.repairIdx != 0)
        {
            int totalKit = 0;
            foreach (var tile in Volt_ArenaSetter.S.GetTileArray())
            {
                if (tile.isHaveRepairKit)
                    totalKit += 1;
            }
            if (totalKit < 3)
                SetRepairKitInTile(Volt_ArenaSetter.S.GetTileByIdx(data.repairIdx));
            else
                isRepairkitSetupDone = true;
        }
        else
            isRepairkitSetupDone = true;
        //Debug.Log("kit Setup");
        yield return new WaitUntil(() => isRepairkitSetupDone);


        if (data.moduleIdx != 0)
        {
            if (Volt_ArenaSetter.S.numOfModule < 6)
                SetModuleInTile(Volt_ArenaSetter.S.GetTileByIdx(data.moduleIdx), data.drawedCard);
            else
                isModuleSetupDone = true;
        }
        else
            isModuleSetupDone = true;
        //Debug.Log("module Setup");
        yield return new WaitUntil(() => isModuleSetupDone);
        //Debug.Log("module Setup done");


        if (data.mapType == Define.MapType.Ruhrgebiet)
        {
            if (Volt_ArenaSetter.S.numOfSetOnVoltageSpace < 6)
            {
                int[] voltageSpaces = { 21, 23, 29, 33, 47, 51, 57, 59 };
                SetVoltageSpace(Volt_ArenaSetter.S.GetTileByIdx(voltageSpaces[((data.moduleIdx + data.vpIdx + data.repairIdx) % 7)]));
            }
            else
                isVoltageSpaceSetupDone = true;

            yield return new WaitUntil(() => isVoltageSpaceSetupDone);
        }

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
    void SetVoltageSpace(Volt_Tile tile)
    {
        if (!tile.IsOnVoltage)
            tile.SetVoltage();
        isVoltageSpaceSetupDone = true;
    }
}
