using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class ArenaSetup : PhaseBase
{
    public override void OnEnterPhase(GameController game)
    {
        Debug.Log("Enter ArenaSetup");
        type = Define.Phase.ArenaSetup;

        Volt_ModuleDeck.S.SetModuleDeckSettingData(game.gameData.mapType);

        StartCoroutine(Action(game.gameData));
    }
    public override void OnExitPhase(GameController game)
    {
        //Destroy(this.gameObject);
        Destroy(gameObject.GetComponent<PhaseBase>());
    }

    public override IEnumerator Action(GameData game)
    {
        yield return new WaitUntil(() => Volt_PlayerManager.S.I != null);

        string sceneName = "";

        switch (game.mapType)
        {
                case Define.MapType.TwinCity:
                    sceneName = "TwinCity";
                    break;
                case Define.MapType.Rome:
                    sceneName = "Rome";
                    break;
                case Define.MapType.Ruhrgebiet:
                    sceneName = "Ruhrgebiet";
                    break;
                case Define.MapType.Tokyo:
                    sceneName = "Tokyo";
                    break;
                default:
                    break;
            }

        AsyncOperationHandle handle = Addressables.LoadSceneAsync($"Assets/_Scenes/{sceneName}.unity", LoadSceneMode.Additive, true);
        yield return new WaitUntil(() => { return handle.IsDone; });
        if (Volt_SoundManager.S != null)
            Volt_SoundManager.S.ChangeBGM(game.mapType);
        yield return new WaitForSeconds(1.5f);
        //noticeText.text = "Field Setup...";

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        yield return StartCoroutine(Volt_ArenaSetter.S.SetupField());
        phaseDone = true;
        GameController.instance.ChangePhase(new PlayerSetup());
    }



}
