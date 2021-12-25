using UnityEditor;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;



//친구 추가 요청을 받고 그 수락or거부 했을 때 패킷이 잘 날아갔는지에 대한 응답값.
public enum EConfirmFriendAddResult { Success=1, NotExist, DBError }

public class ConfirmFriendAdd : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;

        EConfirmFriendAddResult result = (EConfirmFriendAddResult)ByteConverter.ToInt(buffer, ref startIndex);

        int nicknameLength = ByteConverter.ToInt(buffer, ref startIndex);
        string nickname = ByteConverter.ToString(buffer, ref startIndex, nicknameLength);

        
        Debug.Log($"친구 요청에대한 답변의 결과 : {result}");

        Managers.UI.GetPopupUI<Community_Popup>().ShowRequestConfirmPopup(result,nickname);

    }
}