using UnityEditor;
using UnityEngine;



//친구 삭제 요청에 대한 결과값을 받습니다.
//잘 전송 되었는지에 대한 응답 값.
public enum EDeleteFriendResult { Success=1, NotExist, DBError }

public class DeleteFriend : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        EDeleteFriendResult result = (EDeleteFriendResult)ByteConverter.ToInt(buffer, ref startIndex);

        int nicknameLength = ByteConverter.ToInt(buffer, ref startIndex);
        string nickname = ByteConverter.ToString(buffer,ref startIndex, nicknameLength);

        if(result == EDeleteFriendResult.Success)
        {
            Volt_PlayerData.instance.friendsProfileDataDict.Remove(nickname);
            Managers.UI.GetPopupUI<Community_Popup>().RenewFriendsInfo();
        }
        else if(result == EDeleteFriendResult.NotExist)
        {
            //TODO 존재하지 않는 아이디임을 알림
        }
        else
        {
            //TODO 서버에러를 알림.
        }

        
        

    }
}