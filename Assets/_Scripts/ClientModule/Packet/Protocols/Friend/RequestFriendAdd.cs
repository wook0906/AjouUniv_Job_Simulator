using UnityEditor;
using UnityEngine;



//친구 추가 요청을 전송한 것에 대한 결과값을 받습니다.
//잘 전송 되었는지에 대한 응답 값.
public enum ERequestFriendAddResult { Success=1, NotExist, DBError }

public class RequestFriendAdd : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        ERequestFriendAddResult result = (ERequestFriendAddResult)ByteConverter.ToInt(buffer, ref startIndex);

        Debug.Log($"Friends add result : {result}");
        int nicknameLength = ByteConverter.ToInt(buffer, ref startIndex);
        string nickname = ByteConverter.ToString(buffer, ref startIndex, nicknameLength);

        Managers.UI.GetPopupUI<Community_Popup>().ShowRequestAddPopup(result,nickname);

    }
}