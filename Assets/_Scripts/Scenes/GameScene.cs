using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameScene : BaseScene
{
    enum Loads
    {
        None = 0,
        PostProcessing = 1 << 0,
        GamePlayData = 1 << 1,
        ModuleDeck = 1 << 2,
        ParticleManager = 1 << 3,
        GameScene_UI = 1 << 4,
        SimulationObserver = 1 << 5,
        //PlayerUI = 1 << 6,
        //GameManager = 1 << 7,
        GameController = 1 << 6,
        PrefabFactory = 1 << 7,
        MatchingBGTexutre = 1 << 8,
        
        All = PostProcessing | GamePlayData | ModuleDeck |
            ParticleManager | SimulationObserver | GameScene_UI |
            GameController | MatchingBGTexutre | PrefabFactory
    }

    private Loads load;
    public override float Progress
    {
        get
        {
            Array loads = typeof(Loads).GetEnumValues();
            int max = loads.Length - 2;
            int count = 0;
            for(int i = 1; i <= max; ++i)
            {
                if ((load & (Loads)loads.GetValue(i)) != 0)
                    count++;
            }
            return (float)count / max;
        }
    }

    public override bool IsDone
    {
        get { return load == Loads.All; }
    }

    
    AsyncOperationHandle<GameObject>[] preloadHandles;
    AsyncOperationHandle<GameObject> gameMgrHandle;
    List<GameObject> coreGOs = new List<GameObject>();
    //Volt_GameManager gameMgr;
    GameController gameController;
    private IEnumerator Start()
    {
        SceneType = Define.Scene.GameScene;

        AsyncOperationHandle<GameObject> fadePopupHandle = Managers.UI.ShowPopupUIAsync<Fade_Popup>();
        yield return new WaitUntil(() => { return fadePopupHandle.IsDone; });
        Fade_Popup fadePopup = fadePopupHandle.Result.GetComponent<Fade_Popup>();
        fadePopup.FadeIn(.5f, float.MaxValue);

        AsyncOperationHandle<GameObject> loadingPopupHandle = Managers.UI.ShowPopupUIAsync<Loading_Popup>();
        yield return new WaitUntil(() => { return loadingPopupHandle.IsDone; });
        Loading_Popup loadingPopup = loadingPopupHandle.Result.GetComponent<Loading_Popup>();
        yield return new WaitUntil(() => { return loadingPopup.IsInit; });

        

        string[] preloadEnumNames = typeof(Loads).GetEnumNames();
        int max = preloadEnumNames.Length - 3;
        preloadHandles = new AsyncOperationHandle<GameObject>[max];
        for (int i = 1; i <= max; ++i)
        {
            preloadHandles[i - 1] = Managers.Resource.InstantiateAsync($"Game/Core/{preloadEnumNames[i]}.prefab");
            preloadHandles[i - 1].Completed += (result) =>
              {
                  if (result.Result.name.Contains("(Clone)"))
                  {
                      result.Result.name = result.Result.name.Split('(')[0];
                      load |= (Loads)Enum.Parse(typeof(Loads), result.Result.name);
                  }
                  coreGOs.Add(result.Result);
                  //if (!result.Result.GetComponent<Volt_GameManager>())
                  //    return;
                  //gameMgr = result.Result.GetComponent<Volt_GameManager>();
                  if (!result.Result.GetComponent<GameController>())
                      return;
                  gameController = result.Result.GetComponent<GameController>();
              };
        }

        yield return new WaitUntil(() =>
        {
            return preloadHandles.Length == coreGOs.Count;
        });

        foreach (var coreGO in coreGOs)
        {
            coreGO.SendMessage("Init", SendMessageOptions.DontRequireReceiver);
        }
        yield return new WaitUntil(() => { return IsDone; });
        
        Managers.UI.CloseAllPopupUI();

        IntroVideo_Popup introVideoUI = null;
        if (PlayerPrefs.GetInt("Volt_TutorialDone") == 0)
        {
            Managers.Resource.InstantiateAsync($"Game/Core/TutorialData.prefab");

            Managers.UI.ShowPopupUIAsync<IntroVideo_Popup>();
            yield return new WaitUntil(() =>
            {
                introVideoUI = FindObjectOfType<IntroVideo_Popup>();
                return introVideoUI != null;
            });
            Debug.Log("Intro video create");

            introVideoUI.Play();

            yield return new WaitUntil(() => !introVideoUI.IsPlaying());
            Debug.Log("Intro video End");
        }
        else
        {
            MatchingScreen_Popup matchingPopup = null;
            Managers.UI.ShowPopupUIAsync<MatchingScreen_Popup>();
            yield return new WaitUntil(() =>
            {
                matchingPopup = FindObjectOfType<MatchingScreen_Popup>();
                return matchingPopup != null;
            });
        }

        Managers.UI.CloseAllPopupUI();
        //gameMgr.StartMatching();

        if (PlayerPrefs.GetInt("Volt_TutorialDone") == 1)
            gameController.ChangePhase(new StartMatching());
        else
            gameController.ChangePhase(new TutorialMatchSetup());
    }

    public void OnLoadedMatchingBGTexture()
    {
        load |= Loads.MatchingBGTexutre;
    }
    public override void Clear()
    {
        Managers.Clear();
        Camera.main.enabled = false;
    }
}
