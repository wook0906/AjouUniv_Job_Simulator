using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenDeath : PhaseBase
{
    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter SuddenDeath");
        type = Define.Phase.SuddenDeath;

        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        Destroy(gameObject.GetComponent<PhaseBase>());
    }
    public override IEnumerator Action(GameData data)
    {
        if (data.round == 10)
        {
            Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/VOLT_Soundsource_20200623/suddendeath_alram.mp3",
                (result) =>
                {
                    Volt_SoundManager.S.RequestSoundPlay(result.Result, false);
                });
            Volt_GMUI.S.guidePanel.ShowSpriteAnimationMSG(GuideMSGType.SuddenDeath, true);
            yield return new WaitForSeconds(2.5f);
        }
        switch (data.mapType)
        {
            case Define.MapType.TwinCity:
                List<Volt_Tile> suddenDeathTiles = Volt_ArenaSetter.S.GetSuddenDeathTile(data.randomOptionValues[0]);

                foreach (var tile in suddenDeathTiles)
                {
                    tile.pitMonster.GetComponent<Volt_PitMonster>().MonsterTargetSearchAnimationStart();
                }
                yield return new WaitUntil(() => !IsAnyMonsterActive(suddenDeathTiles));
                //스코프 애니메이션 끝날때까지 대기.
                foreach (var item in suddenDeathTiles)
                {
                    item.pitMonster.GetComponent<Volt_PitMonster>().DoAttack(item.GetRobotDirectionInRandomAdjecentTiles(data.randomOptionValues[0]));
                }
                yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff() && !IsAnyMonsterActive(suddenDeathTiles));
                break;
            case Define.MapType.Rome:

                //0~13의 int값
                int randomBallistaLaunchPoint = data.randomOptionValues[0] % 13;
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
                    break;

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

                break;
            case Define.MapType.Ruhrgebiet:
                if (data.isOnSuddenDeath)
                {
                    switch (data.round)
                    {
                        case 10:
                            Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/VOLT_Soundsource_20200623/suddendeath_factory.mp3",
                                (result) =>
                                {
                                    Volt_SoundManager.S.RequestSoundPlay(result.Result, false);
                                });

                            foreach (var item in Volt_ArenaSetter.S.fallTiles1)
                            {
                                item.Fall();
                            }
                            yield return new WaitUntil(() => Volt_ArenaSetter.S.IsAllTileAnimationDone(Volt_ArenaSetter.S.fallTiles1.ToArray()));

                            foreach (var item in Volt_PlayerManager.S.GetPlayers())
                            {
                                Volt_PlayerManager.S.RuhrgebietChangeStartingTiles(item.playerNumber, 1);
                            }


                            break;
                        case 13:
                            Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/VOLT_Soundsource_20200623/suddendeath_factory.mp3",
                                (result) =>
                                {
                                    Volt_SoundManager.S.RequestSoundPlay(result.Result, false);
                                });
                            foreach (var item in Volt_ArenaSetter.S.fallTiles2)
                            {
                                item.Fall();
                            }

                            yield return new WaitUntil(() => Volt_ArenaSetter.S.IsAllTileAnimationDone(Volt_ArenaSetter.S.fallTiles2.ToArray()));

                            foreach (var item in Volt_PlayerManager.S.GetPlayers())
                            {
                                Volt_PlayerManager.S.RuhrgebietChangeStartingTiles(item.playerNumber, 2);
                            }
                            break;
                        default:
                            break;
                    }
                }
                // 가장자리부터 타일이 한줄씩 사라질것임. 값 필요 x;
                break;
            case Define.MapType.Tokyo:

                int randomSatelitePoint = (data.randomOptionValues[0] % 4);
                Volt_Tile targetTile = null;
                switch (randomSatelitePoint)
                {
                    case 0:
                        targetTile = Volt_ArenaSetter.S.GetTileByIdx(data.randomOptionValues[0]);
                        break;
                    case 1:
                        targetTile = Volt_ArenaSetter.S.GetTileByIdx(data.randomOptionValues[1]);
                        break;
                    case 2:
                        targetTile = Volt_ArenaSetter.S.GetTileByIdx(data.randomOptionValues[2]);
                        break;
                    case 3:
                        targetTile = Volt_ArenaSetter.S.GetTileByIdx(data.randomOptionValues[3]);
                        break;
                    default:
                        break;
                }
                Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/VOLT_Soundsource_20200623/suddendeath_Satlite.wav",
                    (result) =>
                    {
                        Volt_SoundManager.S.RequestSoundPlay(result.Result, false);
                    });

                yield return new WaitForSeconds(1.9f);

                Vector3 pos = targetTile.transform.position;
                pos.y += 26f;
                GameObject satelitebeam = Volt_PrefabFactory.S.PopEffect(Define.Effects.VFX_Satelitebeam);
                satelitebeam.transform.position = pos;
                satelitebeam.GetComponent<ParticleSystem>().Play();
                List<Volt_Tile> sateliteTiles = new List<Volt_Tile>();
                sateliteTiles.Add(targetTile);
                foreach (var item in targetTile.GetAdjecentTiles())
                {
                    if (item == null) continue;
                    sateliteTiles.Add(item);
                }

                Volt_PlayerManager.S.I.playerCamRoot.SetShakeType(CameraShakeType.Satlite);
                Volt_PlayerManager.S.I.playerCamRoot.CameraShake();

                foreach (var item in sateliteTiles)
                {
                    item.SetBlinkOption(BlinkType.Attack, 0.5f);
                    item.BlinkOn = true;
                    if (item.GetRobotInTile())
                    {
                        Volt_Robot robot = item.GetRobotInTile();
                        robot.GetDamage(new AttackInfo(robot.playerInfo.playerNumber, 1, CameraShakeType.Satlite));
                        //robot.PlayDamagedEffect(robot.transform);
                    }
                }
                // 랜덤 위치에 폭격.
                yield return new WaitForSeconds(2.5f);
                Volt_PrefabFactory.S.PushEffect(satelitebeam.GetComponent<Poolable>());
                break;
            default:
                break;
        }
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
        foreach(var item in Volt_ArenaSetter.S.robotsInArena)
            {
            item.PushType = PushType.PushedCandidate;
        }

        yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff());

        Volt_GamePlayData.S.ClearOtherRobotsAttackedByRobotsOnThisTurn();
        if (PlayerPrefs.GetInt("Volt_TrainingMode") == 0)
            PacketTransmission.SendSimulationCompletionPacket();
        else
            GameController.instance.ChangePhase<ResolutionTurn>();
        

        data.behaviours.Clear();
        phaseDone = true;
    }

    bool IsAnyMonsterActive(List<Volt_Tile> monsterTile)
    {
        foreach (var item in monsterTile)
        {
            Volt_PitMonster monster = item.pitMonster.GetComponent<Volt_PitMonster>();
            if (monster.isMonsterActive || monster.isSearchActive)
                return true;
        }
        return false;
    }
}
