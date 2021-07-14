using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRoomPlayerInfoItem : UIBase
{
    string nickName;
    string state;
    int level;

    enum Labels
    {
        Level_Label,
        NickName_Label,
        State_Label,
    }
    public override void Init()
    {
        Bind<UILabel>(typeof(Labels));

        GetComponent<UIDragScrollView>().scrollView = transform.parent.parent.GetComponent<UIScrollView>();
    }

    //패킷 날아오면 호출하는걸로.
    public void SetInfo(string nickName, int level)
    {
        this.nickName = nickName;
        this.level = level;
        Get<UILabel>((int)Labels.NickName_Label).text = nickName;
        Get<UILabel>((int)Labels.Level_Label).text = ($"Lv.{ level.ToString()}");
    }
    public void SetState(bool isReady)
    {
        if (isReady)
            Get<UILabel>((int)Labels.State_Label).text = "[FF0000]Ready![-]";
        if (isReady)
            Get<UILabel>((int)Labels.State_Label).text = "[FFFFFF]-[-]";
    }
}
