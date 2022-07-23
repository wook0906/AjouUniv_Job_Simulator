using System.Collections;
using UnityEngine;

public class Volt_GMUI : UIBase
{
    public static Volt_GMUI S;
    //public GameObject matchingCancelBtn;
    //public GameObject matchingScreenPanel;
    //public GameObject scrrenBlockPanel;
    //public UILabel matchWaitingPlayerCountLabel;
    //public GameObject exitPanel;
    public GameObject SessionOverPanel;
    public GameObject cheatBtn;
    public GameObject noticeLabel;
    public GameObject timer;
    //public GameObject syncWaitblockPanel;
    //public GameObject cheatPanel;
    public GameObject optionPanel;
    public GameObject roundNumberGo;
    public GameObject exitGameButton;
    public Volt_PlayerPanel[] playerPanels;
    public Volt_2dUIMsgHandler msg2dHandler;
    public GameObject msg2dPanel;
    public Volt_GMUIGuidePanel guidePanel;
    public Volt_ModuleTooltip toolTipPrefab;
    public Cheat_Popup cheater;
    public UILabel newModuleNoticeLabel;
    public bool isGetNewModule = false;
    Volt_ModuleTooltip currnetShowtoolTip = null;

    public bool _3dObjectInteractable = true;


    bool isExitPanelOn = false;
    public bool IsExitPaenlOn
    {
        get { return isExitPanelOn; }
        set
        {
            isExitPanelOn = value;
        }
    }
    bool isOptionPanelOn = false;
    public bool IsOptionPanelOn
    {
        get { return isOptionPanelOn; }
        set
        {
            isOptionPanelOn = value;
        }
    }
    bool isCheatPanelOn = false;
    public bool IsCheatPanelOn
    {
        get { return isCheatPanelOn; }
        set
        {
            IsTickOn = !value;
            isCheatPanelOn = value;
        }
    }
    bool isCheatModeOn = false;
    public bool IsCheatModeOn
    {
        get { return isCheatModeOn; }
        set{
            
            isCheatModeOn = value;
            cheatBtn.SetActive(value);
        }
    }

    private bool isTickOn = true;
    public bool IsTickOn
    {
        get { return isTickOn; }
        set
        {
            isTickOn = value;
            if (!isTickOn)
            {
                timer.GetComponentInChildren<UILabel>().text = "-";
            }
        }
    }

    bool isHaveControl = true;
    [SerializeField]
    private int tickTimer = 0;
    public int TickTimer
    {
        get { return tickTimer; }
        set
        {
            timer.GetComponentInChildren<UILabel>().color = Color.red;
            tickTimer = value;
            if (tickTimer < 0)
                tickTimer = 0;
            timer.GetComponentInChildren<UILabel>().text = tickTimer.ToString();
            StartCoroutine(TimerTextFade());
        }
    }
    IEnumerator TimerTextFade()
    {
        float t = 0;
        while (t < 1f)
        {
            timer.GetComponentInChildren<UILabel>().color = Color.Lerp(timer.GetComponentInChildren<UILabel>().color, Color.white, Time.deltaTime * 5f);
            t += Time.deltaTime;
            yield return null;
        }
    }
    //public int roundNumber = 0;
    public int RoundNumber
    {
        get { return GameController.instance.gameData.round; }
        set
        {
            if (value >= 0)
            {
                //print("RoundNumber Up!");
                GameController.instance.gameData.round = value;
                roundNumberGo.GetComponentInChildren<UILabel>().text = GameController.instance.gameData.round.ToString();
                //if (value >= 10)
                    //GameController.instance.gameData.isOnSuddenDeath = true;
            }
        }
    }
    //private void Awake()
    //{
        
    //}

    //private void Start()
    //{
        
    //}

    public override void Init()
    {
        S = this;
        transform.SetParent(GameObject.Find("UI Root").transform);
        transform.localPosition = new Vector3(0f, 0f, 0f);
        transform.localScale = Vector3.one;

#if !UNITY_IOS
        exitGameButton.SetActive(false);
#endif
    }

    //public void Init()
    //{
    //    if (PlayerPrefs.GetInt("Volt_TutorialDone") == 0)
    //        matchingCancelBtn.SetActive(false);
    //    //if (Volt_GameManager.S.IsTrainingMode)
    //    //    cheatBtn.SetActive(true);
    //    else
    //        cheatBtn.SetActive(false);
    //}

    public void Synchronization(int roundNumber)
    {
        RoundNumber = roundNumber;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isHaveControl)
        {
            if (Volt_UILayerManager.instance.Empty())
            {
                IsExitPaenlOn = true;
                //OnPressPopupAppearWindowOnBtn(exitPanel);
                //Volt_UILayerManager.instance.Enqueue(exitPanel);
            }
            else
            {
                GameObject go = Volt_UILayerManager.instance.Dequeue();
                //if (go == cheatPanel)
                    //IsCheatPanelOn = false;
                go.GetComponent<UIRect>().alpha = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        if (!Volt_PlayerManager.S) return;
        if (!Volt_PlayerManager.S.I) return;
        if (!Volt_PlayerManager.S.I.raycaster) return;
        if (Volt_UILayerManager.instance.Empty())
            Volt_PlayerManager.S.I.raycaster.enabled = true;
        else
            Volt_PlayerManager.S.I.raycaster.enabled = false;

    }
    public void OnPressPopupAppearWindowOnBtn(GameObject self)
    {
        self.GetComponent<UIRect>().alpha = 1f;
        Volt_UILayerManager.instance.Enqueue(self.gameObject);
    }

    public void OnPressPopupDisappearButton(GameObject self)
    {
        self.GetComponent<UIRect>().alpha = 0f;
        Volt_UILayerManager.instance.RemoveUI(self.gameObject);
    }
    public void OnPressPopupWindowBtn(GameObject self)
    {
        self.SetActive(true);
        Volt_UILayerManager.instance.Enqueue(self.gameObject);
    }
    public void OnPressCloseWindowBtn(GameObject self)
    {
        self.SetActive(false);
        Volt_UILayerManager.instance.RemoveUI(self.gameObject);
    }
    public void ReceiveTick()
    {
        if (IsTickOn)
        {
            TickTimer--;
        }
    }
    public void OnToggleCheatMode()
    {
        if (IsCheatModeOn)
            IsCheatModeOn = false;
        else
            IsCheatModeOn = true;
    }
   
    public void OnClickCheatPanelBtn()
    {
        if (!IsCheatPanelOn)
        {
            IsCheatPanelOn = true;
            Managers.UI.ShowPopupUIAsync<Cheat_Popup>();
        }
        else
        {
            Volt_GMUI.S._3dObjectInteractable = false;
            cheater.ClosePopupUI();
            isCheatPanelOn = false;
        }
    }


   
    //public void OnClickBackToLobbyBtn(GameObject btn)
    //{
    //    isHaveControl = false;
    //    PacketTransmission.SendCancelSearchingEnemyPlayerPacket();
    //    btn.SetActive(false);
    //}
    public void OnClickOptionBtn()
    {
        if (IsOptionPanelOn)
        {
            IsOptionPanelOn = false;
        }
        else
        {
            IsOptionPanelOn = true;
        }
    }

    
    
    public void OnPressdownSessionOverPanel()
    {
        Application.Quit();
    }
    
    public void Create3DMsg(MSG3DEventType msgType, Volt_PlayerInfo player, int optionValue = 1)
    {
        if (!player.playerRobot) return;
        else
        {
            Volt_Robot robot = player.playerRobot.GetComponent<Volt_Robot>();
            Vector3 msgPos = Volt_ArenaSetter.S.GetTile(robot.transform.position).transform.position;
            msgPos.y += 6.5f;
            Volt_3dUIMsg msg = Volt_PrefabFactory.S.PopObject(Define.Objects.MSG3D).GetComponent<Volt_3dUIMsg>();
            if (msg == null)
                return;

            msg.SetMsg(msgType, optionValue);
            msg.transform.position = msgPos;
            msg.transform.rotation = Quaternion.identity;
            msg.StartMove();
        }
    }
    public void Create2DMsg(MSG2DEventType msgType, int playerNumber = 0)
    {
        //if (Volt_GameManager.S.pCurPhase == Phase.synchronization) return;
        if(GameController.instance.CurrentPhase.type == Define.Phase.Synchronization)
            return;
        Volt_2dUIMsg msg = Volt_PrefabFactory.S.PopObject(Define.Objects.MSG2D).GetComponent<Volt_2dUIMsg>();
        if (msg == null)
            return;

        msg.transform.SetParent(msg2dPanel.transform);
        msg.transform.localPosition = Vector3.zero;
        msg.transform.localScale = Vector3.one;
        msg.SetMsg(msgType, playerNumber);
    }
    
    public void OnClickPlayerExitBtn()
    {
        if (GameController.instance.CurrentPhase.type == Define.Phase.GameOver) return;
        GameController.instance.CurrentPhase.OnExitPhase(GameController.instance);
        isHaveControl = false;
        IsTickOn = false;
        if (PlayerPrefs.GetInt("Volt_TutorialDone") == 0)
        {
            Debug.Log("Applicatio Quit");
            Application.Quit();
        }
        else if(PlayerPrefs.GetInt("Volt_TrainingMode") == 1)
        {
            Managers.Scene.LoadSceneAsync(Define.Scene.Lobby);
        }
        else
            PacketTransmission.SendPlayerExitPacket();
        CommunicationWaitQueue.Instance.ResetOrder();
        CommunicationInfo.IsBoardGamePlaying = false;
        Volt_PlayerData.instance.NeedReConnection = false;
    }

    public void ShowModuleToolTip(GameObject parent)
    {
        if (parent.GetComponent<UISprite>().spriteName == "NoneFrame")
        {
            if(currnetShowtoolTip)
                currnetShowtoolTip.CloseTooltip();
            return;
        }
        if (currnetShowtoolTip != null)
        {
            if (currnetShowtoolTip)
                currnetShowtoolTip.CloseTooltip();
            return;
        }
        currnetShowtoolTip = Instantiate(toolTipPrefab, parent.transform);
        currnetShowtoolTip.transform.localPosition = new Vector3(0f, -330f, 0f);
        currnetShowtoolTip.ShowTooltip(parent.GetComponent<UISprite>().spriteName);
    }

    public void NoticeNewModule()
    {
        StartCoroutine(ShowNewModuleNoticeLabelBlink());
    }
    IEnumerator ShowNewModuleNoticeLabelBlink()
    {
        float timer = 0f;
        newModuleNoticeLabel.color = Color.white;
        while (timer <= 3f)
        {
            newModuleNoticeLabel.color = new Color(1f, 1f, 1f, Mathf.PingPong(Time.time * 3f, 1f));
            //Debug.LogError(moduleInfoGuideText.color.a);
            timer += Time.fixedDeltaTime;
            yield return null;
        }
        isGetNewModule = false;
        newModuleNoticeLabel.color = Color.clear;
    }

    public void OnClickExitGameButton()
    {
        Managers.UI.ShowPopupUIAsync<Exit_Popup>();
    }
}
