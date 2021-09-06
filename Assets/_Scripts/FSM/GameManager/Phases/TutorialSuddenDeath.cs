using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TutorialSuddenDeath : PhaseBase
{
    int randomBallistaLaunchPoint;

    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter TutorialSuddenDeath");
        type = Define.Phase.SuddenDeath;

        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        Destroy(gameObject.GetComponent<PhaseBase>());
    }
    public override IEnumerator Action(GameData data)
    {
        if (data.round == 3)
        {
            Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/VOLT_Soundsource_20200623/suddendeath_alram.mp3",
                (result) =>
                {
                    Volt_SoundManager.S.RequestSoundPlay(result.Result, false);
                });
            Volt_GMUI.S.guidePanel.ShowSpriteAnimationMSG(GuideMSGType.SuddenDeath, true);
            yield return new WaitForSeconds(2.5f);
            
            while (TutorialData.S.curTutorialIdx <= 12)
            {
                AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<TutorialExplaination_Popup>();
                yield return new WaitUntil(() => handle.IsDone);
                TutorialExplaination_Popup popUp = handle.Result.GetComponent<TutorialExplaination_Popup>();
                popUp.SetWindow(FindObjectOfType<TutorialData>().datas[TutorialData.S.curTutorialIdx]);
                yield return new WaitUntil(() => TutorialData.S.isOnTutorialPopup == false);
            }
        }

        

        switch (data.round)
        {
            case 3:
            case 4:
            case 5:
                randomBallistaLaunchPoint = 3;
                break;
            default:
                break;
        }
        //0~13의 int값
        //int randomBallistaLaunchPoint = data.randomOptionValues[0] % 13;
        //Debug.Log("randomBallistaLaunchPoint : "+ randomBallistaLaunchPoint);
        List<Volt_Tile> targetTiles = null;
        switch (randomBallistaLaunchPoint)
        {
            case 0:
                targetTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTiles(9, 17));
                targetTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(9));
                break;
            case 1:
                targetTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTiles(18, 26));
                targetTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(18));
                break;
            case 2:
                targetTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTiles(27, 35));
                targetTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(27));
                break;
            case 3:
                targetTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTiles(36, 44));
                targetTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(36));
                break;
            case 4:
                targetTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTiles(45, 53));
                targetTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(45));
                break;
            case 5:
                targetTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTiles(54, 62));
                targetTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(54));
                break;
            case 6:
                targetTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTiles(63, 71));
                targetTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(63));
                break;
            case 7:
                targetTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTiles(73, 1));
                targetTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(73));
                break;
            case 8:
                targetTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTiles(74, 2));
                targetTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(74));
                break;
            case 9:
                targetTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTiles(75, 3));
                targetTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(75));
                break;
            case 10:
                targetTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTiles(76, 4));
                targetTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(76));
                break;
            case 11:
                targetTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTiles(77, 5));
                targetTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(77));
                break;
            case 12:
                targetTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTiles(78, 6));
                targetTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(78));
                break;
            case 13:
                targetTiles = new List<Volt_Tile>(Volt_ArenaSetter.S.GetTiles(79, 7));
                targetTiles.Add(Volt_ArenaSetter.S.GetTileByIdx(79));
                break;
            default:
                //Debug.Log("Error");
                break;
        }

        foreach (var item in targetTiles)
        {
            item.SetBlinkOption(BlinkType.Attack, 0.5f);
            item.BlinkOn = true;
        }
        GameObject ballistaInstance = Volt_PrefabFactory.S.PopObject(Define.Objects.Ballista);
        if (ballistaInstance == null)
            yield break;

        ballistaInstance.transform.position = Volt_ArenaSetter.S.ballistaLaunchPoints[randomBallistaLaunchPoint].transform.position;
        ballistaInstance.transform.rotation = Quaternion.identity;
        Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/VOLT_Soundsource_20200623/suddendeath_Balista_Sound.wav",
            (result) =>
            {
                Volt_SoundManager.S.RequestSoundPlay(result.Result, false);
            });
        GameObject ballistaEffect = Volt_PrefabFactory.S.PopEffect(Define.Effects.VFX_BallistaSonicBoom);
        ballistaEffect.transform.rotation = Quaternion.identity;
        if (randomBallistaLaunchPoint <= 6)
        {
            ballistaInstance.GetComponent<Volt_Ballista>().Init(Vector3.right, targetTiles);
        }
        else
        {
            ballistaInstance.GetComponent<Volt_Ballista>().Init(Vector3.back, targetTiles);
            ballistaEffect.transform.Rotate(0f, 90f, 0f);
        }
        Vector3 effectPos = targetTiles[targetTiles.Count / 2].transform.position;
        effectPos.y += Volt_ArenaSetter.S.ballistaLaunchPoints[randomBallistaLaunchPoint].transform.position.y;
        ballistaEffect.transform.position = effectPos;
        yield return new WaitUntil(() => !ballistaInstance.activeSelf);

        yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff());
        yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotIdleState());

        foreach (var item in Volt_ArenaSetter.S.GetTileArray())
        {
            item.SetDefaultBlinkOption();
            item.BlinkOn = false;
        }

        foreach (var item in Volt_ArenaSetter.S.robotsInArena)
        {
            item.lastAttackPlayer = 0;
        }
        yield return new WaitUntil(() => Volt_ArenaSetter.S.IsAllTileBlinkOff());
        foreach (var item in Volt_ArenaSetter.S.robotsInArena)
        {
            item.PushType = PushType.PushedCandidate;
        }

        yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff());

        data.behaviours.Clear();
        phaseDone = true;

        GameController.instance.ChangePhase<TutorialResolutionTurn>();
    }
}
