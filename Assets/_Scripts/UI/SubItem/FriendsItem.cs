using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class FriendsItem : UIBase
{
    string nickName;
    Define.ProfileData profileData;

    enum Labels
    {
        NickName_Label,
        //level_Label,
    }
    enum Buttons
    {
        Delete_Btn,
        Profile_Btn,
    }
    public override void Init()
    {
        transform.SetChildLayer(transform.root.gameObject.layer);

        Bind<UILabel>(typeof(Labels));
        Bind<UIButton>(typeof(Buttons));

        Get<UIButton>((int)Buttons.Profile_Btn).onClick.Add(new EventDelegate(OnClickProfileButton));
        Get<UIButton>((int)Buttons.Delete_Btn).onClick.Add(new EventDelegate(OnClickDeleteButton));
        
    }
    public void SetInfo(Define.ProfileData profileData)
    {
        this.nickName = profileData.nickname;
        Get<UILabel>((int)Labels.NickName_Label).text = profileData.nickname;
        this.profileData = profileData;
        //Get<UILabel>((int)Labels.level_Label).text = $"Lv.{profileData.level}";
       
    }
    void OnClickProfileButton()
    {
        StartCoroutine(CoSetUpProfile());
    }
    IEnumerator CoSetUpProfile()
    {
        AsyncOperationHandle<GameObject> handle = Managers.UI.ShowPopupUIAsync<OtherPlayerProfile_Popup>();
        yield return new WaitUntil(() => handle.IsDone);
        handle.Result.GetComponent<OtherPlayerProfile_Popup>().SetInfo(profileData);
    }
    void OnClickDeleteButton()
    {
        PacketTransmission.SendDeleteFriendPacket(nickName);
    }

    
}
