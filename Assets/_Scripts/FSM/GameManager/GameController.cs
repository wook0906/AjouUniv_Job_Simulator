using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameData gameData;

    public PhaseBase CurrentPhase
    {
        get { return phaseObject.GetComponent<PhaseBase>(); }
    }

    //public PhaseBase prevPhase;


    public bool isOnSuddenDeath = false;
    public bool IsOnSuddenDeath
    {
        get { return gameData.isOnSuddenDeath; }
        set { gameData.isOnSuddenDeath = value; }
    }
    public bool useCustomPostion = false;
    public PostProcessVolume postProcessVolume;
    Vignette vignette;
    public UILabel noticeText;
    [SerializeField]
    GameObject phaseObject;

    private void Awake()
    {
        instance = this;
        gameData = new GameData();
        gameData.Init();
        phaseObject = transform.Find("PhaseObject").gameObject;
    }
    public void Init()
    {
        postProcessVolume = FindObjectOfType<PostProcessVolume>();
        noticeText = GameObject.Find("GameScene_UI/TopUIs/Notice/Notice").GetComponent<UILabel>();
        noticeText.transform.parent.gameObject.SetActive(false);
        //screenBlockPanel = GameObject.Find("GMUI/ReconnectWaitBlock");
        //screenBlockPanel.SetActive(false);

        //Volt_GamePlayData.S.OnPlayGame(IsTutorialMode || IsTrainingMode);
        

        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            vignette.active = false;
        }
        //if (!IsTutorialMode)
        //    Destroy(Volt_TutorialManager.S);

        //if (!IsTrainingMode)
        //{
        //    Volt_GMUI.S.optionPanel.GetComponent<Volt_GameOptionPanel>().cheatActiveToggle.SetActive(false);
        //}
        Volt_PlayerUI.S.ShowModuleButton(false);
    }
    public void ChangePhase<T>(bool immediately = false) where T : PhaseBase
    {
        StartCoroutine(CorChangePhase<T>(immediately));
    }
    IEnumerator CorChangePhase<T>(bool immediately) where T : PhaseBase
    {
        if (!immediately && CurrentPhase != null)
            yield return new WaitUntil(() => CurrentPhase.phaseDone);

        if (CurrentPhase != null)
        {
            CurrentPhase.OnExitPhase(this);
        }
       

        phaseObject.AddComponent<T>();
        //ADD 할때까지 시간이 좀 걸리나보다?
        yield return new WaitUntil(() => CurrentPhase.phaseDone == false);

        CurrentPhase.OnEnterPhase(this);
    }
   
    

    public void CallFunctionDelayed(string methodName, float delayTime)
    {
        Invoke(methodName, delayTime);
    }
    public void SendAchievementProgressPacketBeforeGameOver(int winner)
    {
        //------------------------------------------승리시 업적 판단 winner가 본인이면 진행하면 될 듯.
        if (Volt_PlayerManager.S.I.playerNumber == winner)
        {
            foreach (var item in Volt_GamePlayData.S.usedModuleCardCounts[winner])
            {
                if (item.Key == Card.DUMMYGEAR &&
                    item.Value > 0)
                {
                    //DB 2000025 업적진행
                    PacketTransmission.SendAchievementProgressPacket(2000025, winner, true);
                }
            }
            if (Volt_GamePlayData.S.IsKillAllTheOtherRobotsInThisGame(winner))
            {
                //DB 2000015 업적진행
                PacketTransmission.SendAchievementProgressPacket(2000015, winner, true);
            }
            if (Volt_GamePlayData.S.IsRobotEverDied(winner))
            {
                //DB 2000014 업적진행
                PacketTransmission.SendAchievementProgressPacket(2000014, winner, true);
            }
            if (Volt_GamePlayData.S.IsRobotKillAnyone(winner))
            {
                //DB 2000013 업적진행
                PacketTransmission.SendAchievementProgressPacket(2000013, winner, true);
            }
        }
        //----------------------------------------------
    }
}

public class GameData
{
    public int KillbotBehaviourPoints
    {
        get 
        {
            if (PlayerPrefs.GetInt("Volt_TutorialDone") == 1)
                return UnityEngine.Random.Range(2, 4);
            else
                return 2;
        }
    }
    public bool isGameOverWaiting = false;
    public bool isOnSuddenDeath = false;
    public bool isEndlessGame = false;
    public bool isKillbotBehaviourOff = false;
    public bool useCameraEffect = true;
    public int winner = 0;
    //public bool isLocalSimulationDone = true;
    public int vpSetupInterval = 5;
    public int remainRoundCountToVpSetup = 0;
    public List<RobotPlaceData> placeRobotRequestDatas;
    public Dictionary<int, Volt_RobotBehavior> behaviours;
    public Queue<TileData> tileDatas;
    public Queue<RobotData> robotDatas;
    public Queue<PlayerData> playerDatas;

    int maxPlayer = 4;//몇인으로 게임을 시작할지를 결정.
    public int MaxPlayer
    {
        get { return maxPlayer; }
        set { maxPlayer = value; }
    }
    public int round;
    public List<int> randomOptionValues;
    public Card drawedCard;
    public int vpIdx;
    public int repairIdx;
    public int moduleIdx;
    //public int myPlayerNumber = 1;
    //public Volt_PlayerInfo I
    //{
    //    get { return playerDict[myPlayerNumber]; }
    //}
    public Define.MapType mapType;
    public Dictionary<int, Volt_PlayerInfo> playerDict;

    public int placeRobotTime = 10;
    public int selectBehaviourTime = 10;
    public int selectRangeTime = 15;

    public int AmargeddonCount = 0;
    public int AmargeddonPlayer = 0;   

    public void Init()
    {
        placeRobotRequestDatas = new List<RobotPlaceData>();
        randomOptionValues = new List<int>();
        behaviours = new Dictionary<int, Volt_RobotBehavior>();
        playerDict = new Dictionary<int, Volt_PlayerInfo>();
        tileDatas = new Queue<TileData>();
        robotDatas = new Queue<RobotData>();
        playerDatas = new Queue<PlayerData>();
        remainRoundCountToVpSetup = vpSetupInterval;
    }
}
