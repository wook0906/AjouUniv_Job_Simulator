using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat_Popup : UI_Popup
{
    Volt_PlayerInfo curSelectedPlayer;
    //public List<GameObject> playerBtns;
    public Dictionary<int, Dictionary<Card, bool>> playersHasModuleDict = new Dictionary<int, Dictionary<Card, bool>>()
    {
        {1, new Dictionary<Card, bool>()},
        {2, new Dictionary<Card, bool>()},
        {3, new Dictionary<Card, bool>()},
        {4, new Dictionary<Card, bool>()}
    };
    //public GameObject suddenDeathToggle;

    UIButton[] playerBtns = new UIButton[4];
    enum Buttons
    {
        Exit_Btn,
        Player1,
        Player2,
        Player3,
        Player4,
        Amargeddon,
        Anchor,
        Bomb,
        Crossfire,
        Dodge,
        DoubleAttack,
        DummyGear,
        EMP,
        Grenade,
        Pernerate,
        PowerBeam,
        RepulsionBlast,
        SawBlade,
        Shield,
        ShockWave,
        TimeBomb,
        SteeringNozzle,
        Hacking,
        Teleport,
        AddHP,
        SubtractHP,
        AddVP,
        SubVP,
        VPPlace_Btn1,
        VPPlace_Btn2,
        VPPlace_Btn3,
        VPPlace_Btn4,
        VPPlace_Btn5,
        VPPlace_Btn6,
        Endless_Button,
        SuddenDeath_Button,
    }

    enum Labels
    {
        Player1_Label,
        Player2_Label,
        Player3_Label,
        Player4Label,
        AddHPLabel,
        SubtractHPLabel,
        AddVPLabel,
        SubVPLabel, 
        Endless_Label,
        SuddenDeath_Label,
    }

    enum Sprites
    {
        BG,
        Exit_Icon,
        AmargeddonIcon,
        AnchorIcon,
        BombIcon ,
        CrossfireIcon ,
        DodgeIcon ,
        DoubleAttackIcon ,
        DummyIcon ,
        EMPIcon,
        GrenadeIcon,
        PernerateIcon,
        PowerBeamIcon,
        RepulsionBlastIcon,
        SawBladeIcon ,
        ShieldIcon,
        ShockPulseIcon,
        TimeBombIcon,
        SteeringNozzleIcon ,
        HackingIcon,
        TeleportIcon ,
        EndlessToggleOn,
        EndlessToggleOff,
        SuddenDeathModeOn,
        SuddenDeathModeOff,
    }


    public override void Init()
    {

        base.Init();

        Volt_GMUI.S.IsCheatPanelOn = true;
        Volt_GMUI.S.cheater = this;
        Volt_GMUI.S._3dObjectInteractable = false;

        Bind<UIButton>(typeof(Buttons));
        Bind<UILabel>(typeof(Labels));   
        Bind<UISprite>(typeof(Sprites));

        Get<UIButton>((int)Buttons.Exit_Btn).onClick.Add(new EventDelegate(() =>
        {
            Volt_GMUI.S.IsCheatPanelOn = false;
            Volt_GMUI.S._3dObjectInteractable = true;
            ClosePopupUI();
        }));
        playerBtns[0] = Get<UIButton>((int)Buttons.Player1);
        playerBtns[0].onClick.Add(new EventDelegate(() =>
        {
            OnClickPlayerSelectBtn(1);
        }));
        playerBtns[1] = Get<UIButton>((int)Buttons.Player2);
        playerBtns[1].onClick.Add(new EventDelegate(() =>
        {
            OnClickPlayerSelectBtn(2);
        }));
        playerBtns[2] = Get<UIButton>((int)Buttons.Player3);
        playerBtns[2].onClick.Add(new EventDelegate(() =>
        {
            OnClickPlayerSelectBtn(3);
        }));
        playerBtns[3] = Get<UIButton>((int)Buttons.Player4);
        playerBtns[3].onClick.Add(new EventDelegate(() =>
        {
            OnClickPlayerSelectBtn(4);
        }));

        GetButtonByCardType(Card.AMARGEDDON).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.AMARGEDDON);
        }));
        GetButtonByCardType(Card.ANCHOR).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.ANCHOR);
        }));
        GetButtonByCardType(Card.BOMB).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.BOMB);
        }));
        GetButtonByCardType(Card.CROSSFIRE).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.CROSSFIRE);
        }));
        GetButtonByCardType(Card.DODGE).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.DODGE);
        }));
        GetButtonByCardType(Card.DOUBLEATTACK).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.DOUBLEATTACK);
        }));
        GetButtonByCardType(Card.DUMMYGEAR).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.DUMMYGEAR);
        }));
        GetButtonByCardType(Card.EMP).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.EMP);
        }));
        GetButtonByCardType(Card.GRENADES).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.GRENADES);
        }));
        GetButtonByCardType(Card.HACKING).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.HACKING);
        }));
        GetButtonByCardType(Card.PERNERATE).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.PERNERATE);
        }));
        GetButtonByCardType(Card.POWERBEAM).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.POWERBEAM);
        }));
        GetButtonByCardType(Card.REPULSIONBLAST).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.REPULSIONBLAST);
        }));
        GetButtonByCardType(Card.SAWBLADE).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.SAWBLADE);
        }));
        GetButtonByCardType(Card.SHIELD).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.SHIELD);
        }));
        GetButtonByCardType(Card.SHOCKWAVE).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.SHOCKWAVE);
        }));
        GetButtonByCardType(Card.STEERINGNOZZLE).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.STEERINGNOZZLE);
        }));
        GetButtonByCardType(Card.TELEPORT).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.TELEPORT);
        }));
        GetButtonByCardType(Card.TIMEBOMB).onClick.Add(new EventDelegate(() =>
        {
            OnClickModuleBtn(Card.TIMEBOMB);
        }));

        PlayerHasModuleDictInit();
        Get<UIButton>((int)Buttons.AddHP).onClick.Add(new EventDelegate(() =>
        {
            OnPressdownHpBtn(true);
        }));
        Get<UIButton>((int)Buttons.SubtractHP).onClick.Add(new EventDelegate(() =>
        {
            OnPressdownHpBtn(false);
        }));
        Get<UIButton>((int)Buttons.AddVP).onClick.Add(new EventDelegate(() =>
        {
            OnPressdownVpBtn(true);
        }));
        Get<UIButton>((int)Buttons.SubVP).onClick.Add(new EventDelegate(() =>
        {
            OnPressdownVpBtn(false);
        }));
        UIButton endlessButton = Get<UIButton>((int)Buttons.Endless_Button);
        if (GameController.instance.gameData.isEndlessGame)
            GetLabel((int)Labels.Endless_Label).text = "Endless : ON";
        else
            GetLabel((int)Labels.Endless_Label).text = "Endless : OFF";
        endlessButton.onClick.Add(new EventDelegate(() =>
        {
            OnClickEndless();
        }));
        UIButton suddenDeathButton = Get<UIButton>((int)Buttons.SuddenDeath_Button);
        if (GameController.instance.gameData.isOnSuddenDeath)
            GetLabel((int)Labels.SuddenDeath_Label).text = "SuddenDeath : ON";
        else
            GetLabel((int)Labels.SuddenDeath_Label).text = "SuddenDeath : OFF";
        suddenDeathButton.onClick.Add(new EventDelegate(() =>
        {
            OnClickSuddenDeath();
        }));
        Get<UIButton>((int)Buttons.VPPlace_Btn1).onClick.Add(new EventDelegate(() =>
        {
            OnClickPlaceVPButton(1);
        }));
        Get<UIButton>((int)Buttons.VPPlace_Btn2).onClick.Add(new EventDelegate(() =>
        {
            OnClickPlaceVPButton(2);
        }));
        Get<UIButton>((int)Buttons.VPPlace_Btn3).onClick.Add(new EventDelegate(() =>
        {
            OnClickPlaceVPButton(3);
        }));
        Get<UIButton>((int)Buttons.VPPlace_Btn4).onClick.Add(new EventDelegate(() =>
        {
            OnClickPlaceVPButton(4);
        }));
        Get<UIButton>((int)Buttons.VPPlace_Btn5).onClick.Add(new EventDelegate(() =>
        {
            OnClickPlaceVPButton(5);
        }));
        Get<UIButton>((int)Buttons.VPPlace_Btn6).onClick.Add(new EventDelegate(() =>
        {
            OnClickPlaceVPButton(6);
        }));
        OnClickPlayerSelectBtn(1);
        Debug.Log("Cheater Init!");
    }

    void PlayerHasModuleDictInit()
    {
        for(int i = 1; i<=4; i++)
        {
            Volt_Robot robot = Volt_PlayerManager.S.GetPlayerByPlayerNumber(i).GetRobot();
            if (robot == null)
            {
                for (Card card = Card.REPULSIONBLAST; card < Card.MAX; card++)
                {
                    playersHasModuleDict[i].Add(card, false);
                }
            }
            else
            {
                for (Card card = Card.REPULSIONBLAST; card < Card.MAX; card++)
                {
                    if (robot.moduleCardExcutor.IsHaveModuleCard(card))
                    {
                        playersHasModuleDict[i].Add(card, true);
                    }
                    else
                    {
                        playersHasModuleDict[i].Add(card, false);
                    }
                }
            }
        }
     

    }


    UIButton GetButtonByCardType(Card cardType)
    {
        switch (cardType)
        {
            case Card.REPULSIONBLAST:
                return Get<UIButton>((int)Buttons.RepulsionBlast);
            case Card.STEERINGNOZZLE:
                return Get<UIButton>((int)Buttons.SteeringNozzle);
            case Card.TELEPORT:
                return Get<UIButton>((int)Buttons.Teleport);
            case Card.CROSSFIRE:
                return Get<UIButton>((int)Buttons.Crossfire);
            case Card.GRENADES:
                return Get<UIButton>((int)Buttons.Grenade);
            case Card.PERNERATE:
                return Get<UIButton>((int)Buttons.Pernerate);
            case Card.POWERBEAM:
                return Get<UIButton>((int)Buttons.PowerBeam);
            case Card.SAWBLADE:
                return Get<UIButton>((int)Buttons.SawBlade);
            case Card.SHOCKWAVE:
                return Get<UIButton>((int)Buttons.ShockWave);
            case Card.ANCHOR:
                return Get<UIButton>((int)Buttons.Anchor);
            case Card.BOMB:
                return Get<UIButton>((int)Buttons.Bomb);
            case Card.SHIELD:
                return Get<UIButton>((int)Buttons.Shield);
            case Card.DODGE:
                return Get<UIButton>((int)Buttons.Dodge);
            case Card.HACKING:
                return Get<UIButton>((int)Buttons.Hacking);
            case Card.TIMEBOMB:
                return Get<UIButton>((int)Buttons.TimeBomb);
            case Card.DOUBLEATTACK:
                return Get<UIButton>((int)Buttons.DoubleAttack);
            case Card.DUMMYGEAR:
                return Get<UIButton>((int)Buttons.DummyGear);
            case Card.AMARGEDDON:
                return Get<UIButton>((int)Buttons.Amargeddon);
            case Card.EMP:
                return Get<UIButton>((int)Buttons.EMP);
            default:
                break;
        }
        Debug.LogError("GetButtonByCardType Error");
        return null;
    }


    public void OnClickPlayerSelectBtn(int playerNumber)
    {
        curSelectedPlayer = Volt_PlayerManager.S.GetPlayerByPlayerNumber(playerNumber);
        for (int i = 0; i < playerBtns.Length; i++)
        {
            if (i == playerNumber - 1)
                playerBtns[i].GetComponent<UISprite>().color = Color.green;
            else
                playerBtns[i].GetComponent<UISprite>().color = Color.white;
        }

        if (curSelectedPlayer.PlayerType == PlayerType.AI)
        {
            foreach (var item in playersHasModuleDict[playerNumber])
            {
                if (item.Key == Card.DUMMYGEAR ||
                    item.Key == Card.HACKING ||
                    item.Key == Card.TELEPORT ||
                    item.Key == Card.STEERINGNOZZLE ||
                    item.Key == Card.SAWBLADE)
                {
                    UIButton btn = GetButtonByCardType(item.Key);
                    btn.isEnabled = false;
                }
            }
        }
        else
        {
            foreach (var item in playersHasModuleDict[playerNumber])
            {
                GetButtonByCardType(item.Key).isEnabled = true;
            }
        }
    }

    public void NoticeGetModuleCard(int playerNumber, Card card)
    {
        playersHasModuleDict[playerNumber][card] = true;
    }
    public void NoticeLostModuleCard(int playerNumber, Card card)
    {
        playersHasModuleDict[playerNumber][card] = true;
    }
    public void RobotDead(Volt_Robot robot)
    {
        for(Card i = Card.REPULSIONBLAST; i < Card.MAX; i++)
        {
            playersHasModuleDict[robot.playerInfo.playerNumber][i] = false;
        }
    }

    public void OnClickModuleBtn(Card cardType)
    {
        Volt_ModuleCardBase clickedModule;
        if (curSelectedPlayer == null)
            return;
        if (curSelectedPlayer.GetRobot() == null) return;
        Volt_Robot robot = curSelectedPlayer.GetRobot();

        if (IsHaveSameCard(cardType))
        {
            robot.moduleCardExcutor.DestroyCard(cardType);
            playersHasModuleDict[curSelectedPlayer.playerNumber][cardType] = false;
        }
        else
        {
            if (Volt_ModuleDeck.S.IsHaveModuleCard(cardType))
            {
                clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(cardType);
                if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
                {
                    robot.OnPickupNewModuleCard(clickedModule);
                    playersHasModuleDict[curSelectedPlayer.playerNumber][cardType] = true;
                }
            }
        }
    }
    //public void OnClickModuleBtn(GameObject moduleBtn)
    //{
    //    Volt_ModuleCardBase clickedModule;
    //    if (curSelectedPlayer == null)
    //        return;
    //    if (curSelectedPlayer.playerRobot == null) return;
    //    Volt_Robot robot = curSelectedPlayer.playerRobot.GetComponent<Volt_Robot>();
    //    switch (moduleBtn.name)
    //    {
    //        case "Amargeddon":
    //            //print("Amargeddon Btn Clicked");
    //            if (IsHaveSameCard(Card.AMARGEDDON))
    //            {
    //                //Volt_GameManager.S.AmargeddonCount = 0;
    //                GameController.instance.gameData.AmargeddonCount = 0;
    //                robot.moduleCardExcutor.DestroyCard(Card.AMARGEDDON);
    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.AMARGEDDON] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.AMARGEDDON))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.AMARGEDDON);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.AMARGEDDON] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "Anchor":
    //            //print("Anchor Btn Clicked");
    //            if (IsHaveSameCard(Card.ANCHOR))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.ANCHOR);
    //                robot.AddOnsMgr.IsHaveAnchor = false;
    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.ANCHOR] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.ANCHOR))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.ANCHOR);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.ANCHOR] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "Bomb":
    //            //print("Bomb Btn Clicked");
    //            if (IsHaveSameCard(Card.BOMB))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.BOMB);
    //                robot.AddOnsMgr.IsHaveBomb = false;
    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.BOMB] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.BOMB))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.BOMB);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.BOMB] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "Crossfire":
    //            //print("Crossfire Btn Clicked");
    //            if (IsHaveSameCard(Card.CROSSFIRE))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.CROSSFIRE);
    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.CROSSFIRE] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.CROSSFIRE))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.CROSSFIRE);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.CROSSFIRE] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "Dodge":
    //            //print("Dodge Btn Clicked");
    //            if (IsHaveSameCard(Card.DODGE))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.DODGE);
    //                robot.AddOnsMgr.IsDodgeOn = false;
    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.DODGE] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.DODGE))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.DODGE);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.DODGE] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "DoubleAttack":
    //            //print("DoubleAttack Btn Clicked");
    //            if (IsHaveSameCard(Card.DOUBLEATTACK))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.DOUBLEATTACK);

    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.DOUBLEATTACK] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.DOUBLEATTACK))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.DOUBLEATTACK);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.DOUBLEATTACK] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "DummyGear":
    //            //print("DummyGear Btn Clicked");
    //            if (IsHaveSameCard(Card.DUMMYGEAR))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.DUMMYGEAR);
    //                robot.AddOnsMgr.IsDummyGearOn = false;
    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.DUMMYGEAR] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.DUMMYGEAR))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.DUMMYGEAR);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.DUMMYGEAR] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "EMP":
    //            //print("EMP Btn Clicked");
    //            if (IsHaveSameCard(Card.EMP))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.EMP);

    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.EMP] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.EMP))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.EMP);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.EMP] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "Grenade":
    //            //print("Grenade Btn Clicked");
    //            if (IsHaveSameCard(Card.GRENADES))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.GRENADES);

    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.GRENADES] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.GRENADES))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.GRENADES);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.GRENADES] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "Hacking":
    //            //print("Hacking Btn Clicked");
    //            if (IsHaveSameCard(Card.HACKING))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.HACKING);
    //                robot.AddOnsMgr.IsHackingOn = false;
    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.HACKING] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.HACKING))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.HACKING);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.HACKING] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "Pernerate":
    //            //print("Pernerate Btn Clicked");
    //            if (IsHaveSameCard(Card.PERNERATE))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.PERNERATE);

    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.PERNERATE] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.PERNERATE))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.PERNERATE);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.PERNERATE] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "PowerBeam":
    //            //print("PowerBeam Btn Clicked");
    //            if (IsHaveSameCard(Card.POWERBEAM))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.POWERBEAM);

    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.POWERBEAM] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.POWERBEAM))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.POWERBEAM);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.POWERBEAM] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "RepulsionBlast":
    //            //print("RepulsionBlast Btn Clicked");
    //            if (IsHaveSameCard(Card.REPULSIONBLAST))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.REPULSIONBLAST);
    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.REPULSIONBLAST] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.REPULSIONBLAST))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.REPULSIONBLAST);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.REPULSIONBLAST] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "SawBlade":
    //            //print("SawBlade Btn Clicked");
    //            if (IsHaveSameCard(Card.SAWBLADE))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.SAWBLADE);
    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.SAWBLADE] = false;
    //                robot.AddOnsMgr.IsHaveSawBlade = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.SAWBLADE))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.SAWBLADE);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.SAWBLADE] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "Shield":
    //            //print("Shield Btn Clicked");
    //            if (IsHaveSameCard(Card.SHIELD))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.SHIELD);
    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.SHIELD] = false;
    //                robot.AddOnsMgr.ShieldPoints = 0;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.SHIELD))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.SHIELD);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.SHIELD] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "ShockWave":
    //            //print("ShockWave Btn Clicked");
    //            if (IsHaveSameCard(Card.SHOCKWAVE))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.SHOCKWAVE);
    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.SHOCKWAVE] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.SHOCKWAVE))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.SHOCKWAVE);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.SHOCKWAVE] = true;
    //                    }

    //                }
    //            }
    //            break;
    //        case "SteeringNozzle":
    //            //print("SteeringNozzle Btn Clicked");
    //            if (IsHaveSameCard(Card.STEERINGNOZZLE))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.STEERINGNOZZLE);
    //                robot.AddOnsMgr.IsSteeringNozzleOn = false;
    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.STEERINGNOZZLE] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.STEERINGNOZZLE))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.STEERINGNOZZLE);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.STEERINGNOZZLE] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "Teleport":
    //            //print("Teleport Btn Clicked");
    //            if (IsHaveSameCard(Card.TELEPORT))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.TELEPORT);

    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.TELEPORT] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.TELEPORT))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.TELEPORT);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.TELEPORT] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        case "TimeBomb":
    //            //print("TimeBomb Btn Clicked");
    //            if (IsHaveSameCard(Card.TIMEBOMB))
    //            {
    //                robot.moduleCardExcutor.DestroyCard(Card.TIMEBOMB);

    //                playersHasModuleDict[curSelectedPlayer.playerNumber][Card.TIMEBOMB] = false;
    //            }
    //            else
    //            {
    //                if (Volt_ModuleDeck.S.IsHaveModuleCard(Card.TIMEBOMB))
    //                {
    //                    clickedModule = Volt_ModuleDeck.S.GetModuleFromDeck(Card.TIMEBOMB);
    //                    if (robot.moduleCardExcutor.IsCanEquip(clickedModule))
    //                    {
    //                        robot.OnPickupNewModuleCard(clickedModule);
    //                        playersHasModuleDict[curSelectedPlayer.playerNumber][Card.TIMEBOMB] = true;
    //                    }
    //                }
    //            }
    //            break;
    //        default:
    //            break;
    //    }
    //}

    bool IsHaveSameCard(Card card)
    {
        foreach (var item in playersHasModuleDict[curSelectedPlayer.playerNumber])
        {
            if (item.Key == card)
                return item.Value;
        }
        Debug.LogError("IsHaveSameCard Error");
        return true;
    }

    bool IsHaveSameCard(int playerNumber, Card card)
    {
        foreach (var item in playersHasModuleDict[playerNumber - 1])
        {
            if (item.Key == card)
                return item.Value;
        }
        Debug.LogError("IsHaveSameCard2 Error");
        return false;
    }

    public void OnPressdownHpBtn(bool up)
    {
        Volt_Robot robot = curSelectedPlayer.playerRobot.GetComponent<Volt_Robot>();
        if (up)
        {
            if (!(robot.HitCount <= 0))
                robot.HitCount -= 1;
        }
        else
        {
            if (!(robot.HitCount >= 2))
                robot.HitCount += 1;
        }
    }
    public void OnPressdownVpBtn(bool up)
    {
        //TODO : 테스트를 위해서 서버에도 승점 패킷을 전송할지 선택해야함.
        if (up)
        {
            curSelectedPlayer.VictoryPoint++;
        }
        else
            curSelectedPlayer.VictoryPoint--;
    }

     void OnClickEndless()
    {
        if (GameController.instance.gameData.isEndlessGame)
        {
            GameController.instance.gameData.isEndlessGame = false;
            GetLabel((int)Labels.Endless_Label).text = "Endless : OFF";
        }
        else
        {
            GameController.instance.gameData.isEndlessGame = true;
            GetLabel((int)Labels.Endless_Label).text = "Endless : ON";
        }
    }
     void OnClickSuddenDeath()
    {
        if (GameController.instance.gameData.isOnSuddenDeath)
        {
            GameController.instance.gameData.isOnSuddenDeath = false;
            GetLabel((int)Labels.SuddenDeath_Label).text = "SuddenDeath : OFF";

        }
        else
        {
            GameController.instance.gameData.isOnSuddenDeath = true;
            GetLabel((int)Labels.SuddenDeath_Label).text = "SuddenDeath : ON";

        }
    }
    void OnClickPlaceVPButton(int number)
    {
        Volt_Tile vpTile = Volt_ArenaSetter.S.GetTileByIdx(Volt_Utils.GetVPTileNumberBySurfaceNumber(GameController.instance.gameData.mapType, number));
        if (vpTile.VpCoinInstance == null)
        {
            vpTile.SetVictoryPoint();
        }
        else
        {
            vpTile.CoinDestroy();
        }
    }
    
}
