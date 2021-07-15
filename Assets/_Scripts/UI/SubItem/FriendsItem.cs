using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsItem : UIBase
{
    string nickName;
    enum Labels
    {
        NickName_Label
    }
    enum Buttons
    {
        Delete_Btn,
        Profile_Btn,
    }
    public override void Init()
    {
        
        Bind<UILabel>(typeof(Labels));
        Bind<UIButton>(typeof(Buttons));
        //GetComponent<UIDragScrollView>().scrollView = transform.parent.parent.GetComponent<UIScrollView>();
    }
    public void SetInfo(string nickName)
    {
        this.nickName = nickName;
        Get<UILabel>((int)Labels.NickName_Label).text = nickName;
    }

    
}
