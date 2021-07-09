using UnityEngine;
using UnityEditor;
using Facebook.Unity;

public class SignInSuccessPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("SignInSuccessPacket Unpack");
        if (GameController.instance)
        {
            Debug.Log("이 버그는 고쳐야 함");
            //로비로 이동할때 날아오는 패킷 보내주는 조건이 뭔지...?
            return;
        }
        DBManager.instance.ClearDB();

        int startIndex = PacketInfo.FromServerPacketSettingIndex;
        int nicknameLength = ByteConverter.ToInt(buffer, ref startIndex);
        string nickname = ByteConverter.ToString(buffer, ref startIndex, nicknameLength);
        int battery = ByteConverter.ToInt(buffer, ref startIndex);
        int gold = ByteConverter.ToInt(buffer, ref startIndex);
        int diamond = ByteConverter.ToInt(buffer, ref startIndex);
        //int rankPoint = ByteConverter.ToInt(buffer, ref startIndex);

        //Debug.Log(nicknameLength);
        //Debug.Log("Nickname : " + nickname);
        //Debug.Log("Battery : " + battery);
        //Debug.Log("Gold : " + gold);
        //Debug.Log("Diamond : " + diamond);
        //Debug.Log("RankPoint : " + rankPoint);
        DBManager.instance.userData = new UserData(nickname, battery, gold, diamond);

        DBManager.instance.OnLoadedUserData();

        LoginScene_UI loginScene_UI = GameObject.FindObjectOfType<LoginScene_UI>();
        if (!loginScene_UI)
        {
            Debug.LogError("Login Scene UI 없음 로드 안한거 같음");
            return;
        }

        loginScene_UI.OnSuccessSignIn();
    }

}