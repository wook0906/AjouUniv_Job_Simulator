using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMatch_Popup : UI_Popup
{
    
    enum Buttons
    {
        Exit_Btn,
        MatchForAI_Btn,
        MatchForNormal_Btn,
    }


    public override void Init()
    {
        base.Init();

        Bind<UIButton>(typeof(Buttons));

        //Managers.UI.PushToUILayerStack(this);

        Get<UIButton>((int)Buttons.Exit_Btn).onClick.Add(new EventDelegate(() =>
        {
            OnClose();
        }));
        Get<UIButton>((int)Buttons.MatchForNormal_Btn).onClick.Add(new EventDelegate(() =>
        {
            OnClickStartGame();
            OnClose();
        }));


        //TODO 정보세팅

    }
    public override void OnClose()
    {
        base.OnClose();
        //ClosePopupUI();
        GetComponent<UIPanel>().alpha = 0f;
    }

    public void OnClickStartGame()
    {
        Debug.Log("StartGame!");
        //Get<UIWidget>((int)Widgets.InputBlock).gameObject.SetActive(true);
        FindObjectOfType<LobbyScene_AssetUI>().SetOnInputBlock();
        //if (PlayerPrefs.GetInt("Volt_TutorialDone") == 0) //게임 플레이하고 왔는지 분기 나눠야할듯...
        //{
        //    Volt_TutorialManager.S.FindContentsByName("WaitClickGameStartBtn").gameObject.SetActive(false);
        //}
        StartCoroutine(CoStartGame());
    }

    IEnumerator CoStartGame()
    {
        Volt_LobbyRobotViewSection.S.PlayLobbyAnimation();
        yield return new WaitForSeconds(2f);
        Managers.Scene.LoadSceneAsync(Define.Scene.GameScene);
    }
}
