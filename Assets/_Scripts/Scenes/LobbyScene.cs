using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LobbyScene : BaseScene
{
    private Exit_Popup exitPopup;

    enum Loads
    {
        None,
        LobbyScene_UI = 1 << 0,
        AchScene_UI = 1 << 1,
        HangarScene_UI = 1 << 2,
        ModuleScene_UI = 1 << 3,
        RobotViewSection = 1 << 4,
        LobbyScene_AssetUI = 1 << 5,
        EmoticonScene_UI = 1 << 6,
        Community_UI = 1 << 7,
        CustomRoom_UI = 1<<8,
        ProfilePictureSetup_UI = 1<<9,
        All = LobbyScene_UI | AchScene_UI |
            HangarScene_UI | ModuleScene_UI | RobotViewSection |
            LobbyScene_AssetUI | EmoticonScene_UI | Community_UI |
            CustomRoom_UI | ProfilePictureSetup_UI
    }
    private Loads loads;

    public override float Progress
    {
        get
        {
            string[] loadList = typeof(Loads).GetEnumNames();
            int max = loadList.Length - 2; // Except None, All
            int count = 0;
            for(int i = 1; i <= max; ++i)
            {
                if ((loads & (Loads)Enum.Parse(typeof(Loads), loadList[i])) != 0)
                    count++;
            }
            return (float)count / max;
        }
    }

    public override bool IsDone
    {
        get
        {
            return loads == Loads.All;
        }
    }

    public GameObject robotInLobbyCamera;
    public GameObject robotInHangarCamera;

    private IEnumerator Start()
    {
        PlayerPrefs.SetInt("Volt_TrainingMode", 0);

        SceneType = Define.Scene.Lobby;
        
        Managers.UI.ShowPopupUIAsync<Fade_Popup>();

        Fade_Popup fadeUI = null;
        yield return new WaitUntil(() =>
        {
            fadeUI = FindObjectOfType<Fade_Popup>();
            return fadeUI != null;
        });
        fadeUI.FadeIn(.3f, float.MaxValue);
        Managers.UI.ShowPopupUIAsync<Loading_Popup>("Loading_Popup", false);
        yield return new WaitUntil(() =>
        {
            Loading_Popup popup = FindObjectOfType<Loading_Popup>();
            return popup != null && popup.IsInit;
        });
        fadeUI.IsStartRightAway = true;

        Init();
        Managers.Resource.LoadAsync<AudioClip>("Assets/_SFX/BGMS/Lobby.wav",
            (result) =>
            {
                Volt_SoundManager.S.ChangeBGM(result.Result);
            });
        yield return new WaitUntil(() => { return loads == Loads.All; });

        yield return new WaitUntil(() => { return fadeUI.IsDone; });
        Managers.UI.CloseAllPopupUI();

        Managers.UI.ShowSceneUI<LobbyScene_UI>();

        if (!Volt_PlayerData.instance.IsGetBenefit(8000001) &&
            Volt_PlayerData.instance.IsHavePackage(8000001))
        {
            Managers.UI.ShowPopupUIAsync<BenefitAlarm_Popup>();
        }


        if (PlayerPrefs.GetInt("Volt_TutorialDone") == 0) //게임 플레이하고 왔는지 분기 나눠야할듯...
        {
            AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<TutorialExplaination_Popup>();
            yield return new WaitUntil(() => handle.IsDone);
            TutorialExplaination_Popup popUp = handle.Result.GetComponent<TutorialExplaination_Popup>();

            TutorialExplainPopupSetupData data = new TutorialExplainPopupSetupData();
            data.isNeedArrow = false;
            data.width = 900;
            data.height = 150;
            data.fontSize = 40;
            data.isButton = true;
            data.contents = "수고하셨습니다, 오퍼레이터님.\n 본선에서 승리하여 슈퍼스타가 되기를 기원하겠습니다.";
            popUp.SetWindow(data);
            PlayerPrefs.SetInt("Volt_TutorialDone", 1);
        }
    }

    protected override void Init()
    {
        Volt.Shop.ShopDataManager.instance.Init();

        float ratio = (float)Screen.width / (float)Screen.height;
        float value1 = 4f / 3f;
        float value2 = 16f / 9f;

        float differ1 = Mathf.Abs(ratio - value1);
        float differ2 = Mathf.Abs(ratio - value2);

        robotInHangarCamera.GetComponent<Camera>().orthographicSize = differ1 < differ2 ? 2.6f : 2.3f;
        robotInLobbyCamera.GetComponent<Camera>().orthographicSize = differ1 < differ2 ? 2.6f : 2.3f;

        Managers.UI.ShowSceneUIAsync<LobbyScene_UI>();
        Managers.UI.ShowSceneUIAsync<AchScene_UI>();
        Managers.UI.ShowSceneUIAsync<HangarScene_UI>();
        Managers.UI.ShowSceneUIAsync<ModuleScene_UI>();
        Managers.UI.ShowSceneUIAsync<EmoticonScene_UI>();
        Managers.UI.ShowSceneUIAsync<Community_UI>();
        Managers.UI.ShowSceneUIAsync<CustomRoom_UI>();
        Managers.UI.ShowSceneUIAsync<ProfilePictureSetup_UI>();

        FindObjectOfType<Volt_LobbyRobotViewSection>().Init();
        loads |= Loads.RobotViewSection;
    }

    public void ChangeToLobbyCamera()
    {
        if (Managers.UI.GetUILayerStack().Count != 0) return;
        robotInLobbyCamera.SetActive(true);
        robotInHangarCamera.SetActive(false);
    }

    public void ChangeToHangarCamera()
    {
        robotInHangarCamera.SetActive(true);
        robotInLobbyCamera.SetActive(false);
    }

    public void SetOffAllRobotCameras()
    {
        robotInHangarCamera.SetActive(false);
        robotInLobbyCamera.SetActive(false);
    }

    public void OnLoadedAchSceneUI()
    {
        loads |= Loads.AchScene_UI;
        Debug.Log("Load AchScene UI");
        FindObjectOfType<AchScene_UI>().gameObject.SetActive(false);
    }

    public void OnLoadedModuleSceneUI()
    {
        loads |= Loads.ModuleScene_UI;
        Debug.Log("Load ModuleScene UI");
        FindObjectOfType<ModuleScene_UI>().gameObject.SetActive(false);
    }

    public void OnLoadedHangarSceneUI()
    {
        loads |= Loads.HangarScene_UI;
        Debug.Log("Load Hangar UI");
        FindObjectOfType<HangarScene_UI>().gameObject.SetActive(false);
    }

    public void OnLoadedEmoticonSceneUI()
    {
        loads |= Loads.EmoticonScene_UI;
        Debug.Log("Load Emoticon UI");
        FindObjectOfType<EmoticonScene_UI>().gameObject.SetActive(false);
    }

    public void OnLoadedLobbySceneUI()
    {
        loads |= Loads.LobbyScene_UI;
        Debug.Log("Load LobbyScene UI");

        Managers.Resource.InstantiateAsync("UI/LobbyScene_AssetUI.prefab",
            (result) =>
            {
                GameObject go = result.Result;

                go.transform.SetParent(GameObject.Find("UI Root").transform);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
            });
    }

    public void OnLoadedAssetUI()
    {
        loads |= Loads.LobbyScene_AssetUI;
        Debug.Log("Load Asset UI");
    }
    public void OnLoadedCommunityUI()
    {
        loads |= Loads.Community_UI;
        Debug.Log("Load Community UI");
        FindObjectOfType<Community_UI>().gameObject.SetActive(false);
    }
    public void OnLoadedCustomRoomUI()
    {
        loads |= Loads.CustomRoom_UI;
        FindObjectOfType<CustomRoom_UI>().gameObject.SetActive(false);
    }
    public void OnLoadedProfilePictureSetupUI()
    {
        loads |= Loads.ProfilePictureSetup_UI;
        FindObjectOfType<ProfilePictureSetup_UI>().gameObject.SetActive(false);
    }

    public override void Clear()
    {
        Managers.Clear();
        FindObjectOfType<Volt_LobbyRobotViewSection>().Clear();
        Camera.main.enabled = false;
        robotInLobbyCamera.SetActive(false);
        robotInHangarCamera.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if (Managers.UI.GetPopupStack().Count == 0)
            if (Managers.UI.GetUILayerStack().Count == 0)
            {
                AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<Exit_Popup>();

                if (handle.IsValid())
                {
                    handle.Completed += (result) =>
                    {
                        exitPopup = result.Result.GetComponent<Exit_Popup>();
                    };
                }
            }
            else
            {
                Managers.UI.GetUILayerStack().Pop().OnClose();
            }
        }
    }
}
