using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : PhaseBase
{

    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter GameOver");
        type = Define.Phase.GameOver;

        game.gameData.isOnSuddenDeath = false;
        game.gameData.isGameOverWaiting = true;

        Volt_GMUI.S.guidePanel.HideGuideText();
        foreach (var item in Volt_GMUI.S.playerPanels)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var item in Volt_ArenaSetter.S.robotsInArena)
        {
            item.panel.gameObject.SetActive(false);
        }

        CommunicationWaitQueue.Instance.ResetOrder();
        CommunicationInfo.IsBoardGamePlaying = false;
        Volt_PlayerData.instance.NeedReConnection = false;

        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        Destroy(gameObject.GetComponent<PhaseBase>());
    }

    public override IEnumerator Action(GameData data)
    {
        //yield return new WaitUntil(() => data.isLocalSimulationDone);
        yield return new WaitUntil(() => !Volt_PlayerManager.S.I.playerCam.GetComponent<Volt_CameraEffect>().isShakePlaying);
        yield return new WaitUntil(() => !Volt_PlayerManager.S.I.playerCamRoot.isMoving);

        Volt_Robot winnerRobot = null;
        if (Volt_PlayerManager.S.GetPlayerByPlayerNumber(data.winner).playerRobot)
        {
            winnerRobot = Volt_PlayerManager.S.GetPlayerByPlayerNumber(data.winner).playerRobot.GetComponent<Volt_Robot>();
        }

        if (winnerRobot != null)
        {
            foreach (var item in Volt_ArenaSetter.S.robotsInArena)
            {
                if (item)
                {
                    item.playerInfo.playerCamRoot.GameOver(item.gameObject);
                    if (item == winnerRobot.GetComponent<Volt_Robot>())
                    {
                        item.fsm.Animator.CrossFade("Win", 0.1f);
                    }
                    else
                    {
                        item.fsm.Animator.CrossFade("Lose", 0.1f);
                    }
                }
            }
        }

        //noticeText.text = "Game Over!";

        if (data.winner == Volt_PlayerManager.S.I.playerNumber)
        {
            //DB 승리 카운트 증가
            Volt_GamePlayData.S.Rank = 1;
            Volt_GMUI.S.guidePanel.ShowSpriteAnimationMSG(GuideMSGType.Victory, false);
            Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/VOLT_SE/01Victory.wav",
                (result) =>
                {
                    Volt_SoundManager.S.RequestSoundPlay(result.Result, false);
                });

            if (Volt_PlayerManager.S.I.playerRobot)
            {
                Volt_Robot robot = Volt_PlayerManager.S.I.playerRobot.GetComponent<Volt_Robot>();
                Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/SFx_200703/RobotVictory.wav",
                    (result) =>
                    {
                        Volt_SoundManager.S.RequestSoundPlay(result.Result, false);
                    });
            }
        }
        else
        {
            Volt_GamePlayData.S.Rank = 2;
            Volt_GMUI.S.guidePanel.ShowSpriteAnimationMSG(GuideMSGType.Defeat, false);
            Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/VOLT_SE/02Lose.wav",
                (result) =>
                {
                    Volt_SoundManager.S.RequestSoundPlay(result.Result, false);
                });

            if (Volt_PlayerManager.S.I.playerRobot)
            {
                Volt_Robot robot = Volt_PlayerManager.S.I.playerRobot.GetComponent<Volt_Robot>();
                Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/SFx_200703/RobotLose.wav",
                    (result) =>
                    {
                        Volt_SoundManager.S.RequestSoundPlay(result.Result, false);
                    });
            }
        }

        yield return new WaitForSeconds(6f);

        Managers.Scene.LoadSceneAsync(Define.Scene.ResultScene);
        //Volt_LoadingSceneManager.S.RequestLoadScene("ResultScene 1");

    }
}
