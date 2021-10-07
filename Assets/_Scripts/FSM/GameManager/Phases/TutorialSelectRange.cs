using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSelectRange : PhaseBase
{
    bool isRangeSelectDone = false;

    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter TutorialSelectRange");

        type = Define.Phase.SelectRange;

        Volt_PlayerUI.S.BehaviourSelectOff();

        Volt_GMUI.S.guidePanel.ShowGuideTextMSG(GuideMSGType.BehaviourSelect);

        Volt_PlayerManager.S.I.playerCamRoot.enabled = false;
        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        Volt_PlayerManager.S.I.playerCamRoot.CamInit();
        Volt_PlayerManager.S.I.playerCamRoot.SaveLastInfo();
        foreach (var item in Volt_ArenaSetter.S.GetTileArray())
        {
            item.SetDefaultBlinkOption();
            item.BlinkOn = false;
        }

        foreach (var item in Volt_ArenaSetter.S.robotsInArena)
        {
            item.panel.IndicateControl(false);
        }

        Destroy(gameObject.GetComponent<PhaseBase>());
    }
    public override IEnumerator Action(GameData game)
    {
        Volt_PlayerManager.S.I.playerCamRoot.SaveLastInfo();

        StartCoroutine(WaitCameraAnimation(false));

        foreach (var item in Volt_ArenaSetter.S.GetTileArray())
        {
            if (item.VpCoinInstance)
                item.VpCoinInstance.GetComponent<FXV.FXVRotate>().RenewRotation(new Vector3(0f, 0f, 90f));
            else if (item.RepairKitInstance)
                item.RepairKitInstance.GetComponent<FXV.FXVRotate>().RenewRotation(new Vector3(90f, 0f, 0f));
        }

        Volt_Robot robot = Volt_PlayerManager.S.I.playerRobot.GetComponent<Volt_Robot>();
        Volt_RobotBehavior behaviour = game.behaviours[Volt_PlayerManager.S.I.playerNumber];

        Volt_Tile targetTile = null;
        switch (game.round)
        {
            case 1:
                targetTile = Volt_ArenaSetter.S.GetTileByIdx(40);
                break;
            case 2:
                targetTile = Volt_ArenaSetter.S.GetTileByIdx(41);
                break;
            case 3:
            case 5:
                targetTile = Volt_ArenaSetter.S.GetTileByIdx(49);
                break;
            case 4:
                targetTile = Volt_ArenaSetter.S.GetTileByIdx(39);
                break;
            default:
                break;
        }
        targetTile.SetDefaultBlinkOption();
        targetTile.BlinkOn = true;
        
        yield return StartCoroutine(WaitSelectRangeDone());

        yield return StartCoroutine(WaitCameraAnimation(true));
        phaseDone = true;

        GameController.instance.ChangePhase<TutorialSimulation>();
    }
    IEnumerator WaitCameraAnimation(bool rewind)
    {
        //Volt_PlayerManager.S.I.playerCamRoot. isRangeSelectCameraMoving = true;
        CameraMovement camRoot = Volt_PlayerManager.S.I.playerCamRoot;
        if (!rewind)
        {
            Volt_PlayerManager.S.I.playerCamRoot.isMoving = true;
            Vector3 startPos = camRoot.cam.transform.position;
            Vector3 endPos = new Vector3(Volt_ArenaSetter.S.GetCenterTransform().position.x, 33f, Volt_ArenaSetter.S.GetCenterTransform().position.z);
            Vector3 middlePos = (endPos - startPos);


            Vector3 rot1;
            Vector3 rot2 = Vector3.zero;

            float t = 0f;
            middlePos = new Vector3(startPos.x, endPos.y, startPos.z);
            middlePos.y += 10f;
            rot1 = camRoot.cam.transform.eulerAngles;
            switch (Volt_PlayerManager.S.I.playerNumber)
            {
                case 1:
                    break;
                case 2:
                    rot2.y = 90f;
                    break;
                case 3:
                    rot2.y = 180f;
                    break;
                case 4:
                    rot2.y = 270f;
                    break;
                default:
                    break;
            }
            rot2.x = 90f;

            while (t <= 1f)
            {
                if (!Volt_PlayerManager.S.I.IsMobileActivated)
                    break;
                t += Time.deltaTime * 2f;// 여기서 속도 조절
                Vector3 camPos = Vector3.Lerp(Vector3.Lerp(startPos, middlePos, t), Vector3.Lerp(middlePos, endPos, t), t);
                camRoot.cam.transform.position = camPos;

                camRoot.cam.transform.rotation = Quaternion.Euler(Vector3.Lerp(rot1, rot2, t));

                //if (curPhase == Phase.waitSync)
                //{
                //    camRoot.CamInit();
                //    camRoot.SaveLastInfo();
                //    yield break;
                //}
                yield return null;
            }
        }
        else
        {
            Vector3 startPos = camRoot.cam.transform.position;
            Vector3 endPos = camRoot.lastCamWorldPos;
            Vector3 middlePos = (endPos - startPos);


            Vector3 rot1;
            Vector3 rot2;

            float t = 0f;
            middlePos = new Vector3(startPos.x, endPos.y, startPos.z);
            middlePos.y += 10f;
            rot1 = camRoot.cam.transform.eulerAngles;
            rot2 = camRoot.tilter.transform.localRotation.eulerAngles;

            switch (Volt_PlayerManager.S.I.playerNumber)
            {
                case 1:
                    break;
                case 2:
                    rot2.y = 90f;
                    break;
                case 3:
                    rot2.y = 180f;
                    break;
                case 4:
                    rot2.y = 270f;
                    break;
                default:
                    break;
            }

            while (t <= 1f)
            {
                t += Time.deltaTime * 2f;// 여기서 속도 조절
                Vector3 camPos = Vector3.Lerp(Vector3.Lerp(startPos, middlePos, t), Vector3.Lerp(middlePos, endPos, t), t);
                camRoot.cam.transform.position = camPos;

                camRoot.cam.transform.rotation = Quaternion.Euler(Vector3.Lerp(rot1, rot2, t));

                yield return null;
            }
            Volt_PlayerManager.S.I.playerCamRoot.isMoving = false;
        }
    }

    IEnumerator WaitSelectRangeDone()
    {
        yield return new WaitUntil(() => isRangeSelectDone); 

        foreach (var item in Volt_ArenaSetter.S.GetTileArray())
        {
            item.SetDefaultBlinkOption();
            item.BlinkOn = false;
        }

        StartCoroutine(WaitCameraAnimation(true));
        foreach (var item in Volt_ArenaSetter.S.GetTileArray())
        {
            if (item.VpCoinInstance)
                item.VpCoinInstance.GetComponent<FXV.FXVRotate>().SetDefaultRotation();
            else if (item.RepairKitInstance)
                item.RepairKitInstance.GetComponent<FXV.FXVRotate>().SetDefaultRotation();
        }
        
        DoAllKillbotsDetection();

        yield return new WaitUntil(() => SimulationObserver.Instance.IsAllRobotBehaviourFlagOff());

    }

    void SelectRangeDoneCallback(Volt_Tile selectedTile)
    {
        Volt_PlayerManager.S.I.playerCamRoot.enabled = true;
        Volt_PlayerManager.S.I.playerCamRoot.RTSCameraInit();

        Volt_PlayerInfo I = Volt_PlayerManager.S.I;
        Volt_Robot robot = I.playerRobot.GetComponent<Volt_Robot>();
        Volt_Tile curStandingTile = Volt_ArenaSetter.S.GetTile(robot.transform.position);

        Vector3 direction = (selectedTile.transform.position - curStandingTile.transform.position).normalized;
        // Debug.Log($"[SelectRangeDonCallback] direction:{direction}");

        Volt_RobotBehavior myBehaviour = GameController.instance.gameData.behaviours[Volt_PlayerManager.S.I.playerNumber];

        myBehaviour.SetBehaivor(Volt_ArenaSetter.S.GetDistanceBetweenTiles(curStandingTile, selectedTile), direction, myBehaviour.BehaviourType, Volt_PlayerManager.S.I.playerNumber, selectedTile);

        Volt_RobotBehavior tmpBehaviour;

        GameController.instance.gameData.behaviours[1] = myBehaviour;

        isRangeSelectDone = true;
    }

    public void DoAllKillbotsDetection()
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

    public override void OnTouchTileBegin(Volt_Tile tile)
    {
        if (tile.BlinkOn)
        {
            foreach (var item in Volt_ArenaSetter.S.tileObjList)
            {
                item.blinkColor = Color.green;
            }
        }
    }
    public override void OnTouchTile(Volt_Tile tile)
    {
        Volt_Robot robot = Volt_PlayerManager.S.I.playerRobot.GetComponent<Volt_Robot>();
        if (tile.BlinkOn)
        {
            foreach (var item in Volt_ArenaSetter.S.tileObjList)
            {
                item.blinkColor = Color.green;
            }
            Volt_Tile[] tiles = Volt_ArenaSetter.S.GetTiles(Volt_ArenaSetter.S.GetTile(robot.transform.position), tile);
            foreach (var item in tiles)
            {
                //if (Volt_GameManager.S.behaviour.BehaviourType == BehaviourType.Move)
                if (GameController.instance.gameData.behaviours[Volt_PlayerManager.S.I.playerNumber].BehaviourType == BehaviourType.Move)
                    item.blinkColor = Color.blue;
                else
                    item.blinkColor = Color.red;
            }
        }
    }
    public override void OnTouchTileEnd(Volt_Tile tile)
    {
        if (!tile.BlinkOn)
        {
            foreach (var item in Volt_ArenaSetter.S.tileObjList)
            {
                item.blinkColor = Color.green;
            }
        }
        else
        {
            if (!Volt_PlayerManager.S.isCanGetPlayerKey(Volt_PlayerManager.S.I.playerNumber))
                return;
            SelectRangeDoneCallback(tile);
        }
    }
}
