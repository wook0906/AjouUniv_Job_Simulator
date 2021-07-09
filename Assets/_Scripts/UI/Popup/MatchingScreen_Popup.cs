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
        MatchWaitPlayersLabel
    }

    public override void Init()
    {
        base.Init();
        Bind<UIButton>(typeof(Buttons));
        Bind<UILabel>(typeof(Labels));

        GetButton((int)Buttons.MathchingCancelBtn).onClick.Add(new EventDelegate(() => { OnClickMatchingCancelBtn(); }));
        label = Get<UILabel>((int)Labels.MatchWaitPlayersLabel);

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
        
        label.text = phase.connectedPlayerCount + "/4";
    }
}
