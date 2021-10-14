using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingScreen_Popup : UI_Popup
{
    UILabel label;

    enum Buttons
    {
        MathchingCancelBtn
    }

    enum Labels
    {
        MatchWaitPlayersLabel,
    }
    enum Textures
    {
        UnScalingBG,
    }

    public override void Init()
    {
        base.Init();
        Bind<UIButton>(typeof(Buttons));
        Bind<UILabel>(typeof(Labels));
        Bind<UITexture>(typeof(Textures));


        GetButton((int)Buttons.MathchingCancelBtn).onClick.Add(new EventDelegate(() => { OnClickMatchingCancelBtn(); }));
        label = Get<UILabel>((int)Labels.MatchWaitPlayersLabel);

        Managers.Resource.LoadAsync<Texture2D>(Random.Range(0, 9).ToString(), (result) =>
        {
            Get<UITexture>((int)Textures.UnScalingBG).mainTexture = result.Result;
            //if (Managers.Scene.CurrentScene != null &&
            //Managers.Scene.CurrentScene.SceneType == Define.Scene.GameScene)
            //{
            //    GameScene gameScene = Managers.Scene.CurrentScene as GameScene;
            //    gameScene.OnLoadedMatchingBGTexture();
            //}
        });

    }
    void OnClickMatchingCancelBtn()
    {
        if (GameController.instance.CurrentPhase.type != Define.Phase.StartMatching) return;

        Get<UIButton>((int)Buttons.MathchingCancelBtn).gameObject.SetActive(false);
        CommunicationWaitQueue.Instance.ResetOrder();
        CommunicationInfo.IsBoardGamePlaying = false;
        Volt_PlayerData.instance.NeedReConnection = false;
        PacketTransmission.SendCancelSearchingEnemyPlayerPacket();
    }
    private void FixedUpdate()
    {
        StartMatching phase = GameController.instance.CurrentPhase as StartMatching;
        if (phase == null) return;
        if (!isInit) return;

        if (PlayerPrefs.GetInt("Volt_TrainingMode") == 0)
            label.text = phase.connectedPlayerCount + "/4";
        else
            label.text = "1/1";
    }
}
