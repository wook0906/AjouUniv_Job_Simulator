using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionTurn : PhaseBase
{
    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter Resolution");

        type = Define.Phase.ResolutionTurn;

        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        Destroy(gameObject.GetComponent<PhaseBase>());
    }
    public override IEnumerator Action(GameData data)
    {
        foreach (var item in Volt_ArenaSetter.S.robotsInArena)
        {
            item.DoResolutionStart();
        }

        yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff() && IsAllResolutionEnd());
        foreach (var item in Volt_ArenaSetter.S.GetTileArray())
        {
            item.SetDefaultBlinkOption();
            item.BlinkOn = false;
        }
        yield return new WaitForSeconds(2.2f);
        yield return new WaitUntil(() => Volt_ArenaSetter.S.IsAllTileBlinkOff());


        //foreach (var item in Volt_PlayerManager.S.GetPlayers())
        //{
        //    if (item.VictoryPoint >= 3)
        //        yield break;
        //}


        Volt_GMUI.S.guidePanel.ShowSpriteAnimationMSG(GuideMSGType.RoundEnd, true);

        Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/etc/RoundOverSound.wav",
            (result) =>
            {
                Volt_SoundManager.S.RequestSoundPlay(result.Result, false);
            });


        yield return new WaitUntil(() => IsAllResolutionEnd() && SimulationObserver.Instance.IsAllRobotIdleState());

        if (data.mapType == Define.MapType.Tokyo &&
            data.round %2 == 0)
            Volt_ArenaSetter.S.AppearAllWalls();
        yield return new WaitForSeconds(1.5f);

        SendCurrentGameData();

        phaseDone = true;
    }

    bool IsAllResolutionEnd()
    {
        foreach (var item in Volt_ArenaSetter.S.robotsInArena)
        {
            if (!item.isResolutionDone)
            {
                //Debug.Log(item.name + "Resolution is Not Done");
                return false;
            }
        }
        return true;
    }

    void SendCurrentGameData()
    {
        if (GameController.instance.gameData.isGameOverWaiting) return;


        if (CommunicationInfo.IsMobileActive)
        {
            RenewNeedSyncTile();

            GameStateDataSet.SendTotalTurnOverGameDatas();
        }
        Volt_GMUI.S.guidePanel.ShowGuideTextMSG(GuideMSGType.Synchronization, false);
    }

    void RenewNeedSyncTile()
    {
        foreach (var item in Volt_ArenaSetter.S.GetTileArray())
        {
            if ((item.isHaveRepairKit ||
                item.IsHaveTimeBomb ||
                item.ModuleInstance ||
                item.VpCoinInstance ||
                item.IsOnVoltage) && !Volt_ArenaSetter.S.needSyncTiles.Contains(item))
            {
                Volt_ArenaSetter.S.needSyncTiles.Add(item);
            }
        }
    }
}
