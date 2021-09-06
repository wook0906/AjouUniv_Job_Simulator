using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class TutorialMatchSetup : PhaseBase
{
    public RobotType SelectedRobot = RobotType.Max;
    bool robotSelectDone = false;

    public override void OnEnterPhase(GameController game)
    {
        type = Define.Phase.TutorialSetup;
        
        game.gameData.MaxPlayer = 3;
        GameController.instance.gameData.mapType = Define.MapType.Rome;
        Volt_PlayerManager.S.GetMyPlayerNumberFromServer(1);
        Volt_ModuleDeck.S.SetModuleDeckSettingData(game.gameData.mapType);


        StartCoroutine(Action(game.gameData));
        
    }
    public override void OnExitPhase(GameController game)
    {
        base.OnExitPhase(game);
    }
    public override IEnumerator Action(GameData data)
    {
        AsyncOperationHandle handle = Addressables.LoadSceneAsync($"Assets/_Scenes/Rome.unity", LoadSceneMode.Additive, true);
        yield return new WaitUntil(() => { return handle.IsDone; });
        if (Volt_SoundManager.S != null)
            Volt_SoundManager.S.ChangeBGM(data.mapType);
        //yield return new WaitForSeconds(1.5f);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Rome"));

        Managers.UI.ShowPopupUIAsync<TutorialCharacterSelect_Popup>();

        yield return new WaitUntil(() => robotSelectDone);

        Volt_PlayerManager.S.CreatePlayer(PlayerType.HOSTPLAYER, 1, SelectedRobot, SkinType.Origin, Volt_PlayerData.instance.NickName);
        Volt_PlayerManager.S.CreatePlayer(PlayerType.AI, 2, RobotType.Mercury, SkinType.Origin, Volt_Utils.GetRandomKillbotName(RobotType.Mercury));
        Volt_PlayerManager.S.CreatePlayer(PlayerType.AI, 4, RobotType.Reaper, SkinType.Origin, Volt_Utils.GetRandomKillbotName(RobotType.Reaper));
        
        yield return new WaitUntil(() => GameController.instance.gameData.playerDict.Count == 3);

        yield return StartCoroutine(Volt_ArenaSetter.S.SetupField());

        phaseDone = true;
        GameController.instance.ChangePhase<PlayerSetup>();

    }
    public void RobotSelectDone()
    {
        robotSelectDone = true;
    }

}
