using UnityEngine;
using UnityEditor;


public class SignUpSuccessPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("SignUpSuccessPacket Unpack");
        Debug.Log("계정 생성에 성공하였음");
        
        DBManager.instance.ClearDB();
        {
            // 몇 가지 로컬 데이터 초기화

            // 새로 가입한 계정이니 튜토리얼할 수 있게 무조건 초기화
            if (PlayerPrefs.HasKey("Volt_TutorialDone"))
                PlayerPrefs.DeleteKey("Volt_TutorialDone");

            // 아래 스킨과 같은 이유로 초기화
            if (PlayerPrefs.HasKey("SELECTED_ROBOT"))
                PlayerPrefs.DeleteKey("SELECTED_ROBOT");

            // 로컬 기기에 스킨 착용정보가 저장되다보니
            // 로컬 데이터가 남으면 새 계정에 스킨이 적용되는 현상이 있음.
            for (int i = (int)RobotType.Volt; i < (int)RobotType.Max; ++i)
            {
                RobotType type = (RobotType)i;
                string key = $"{type}_skin";
                if (PlayerPrefs.HasKey(key))
                    PlayerPrefs.DeleteKey(key);
            }
        }

        int startIndex = PacketInfo.FromServerPacketSettingIndex;
        int nicknameLength = ByteConverter.ToInt(buffer, ref startIndex);
        string nickname = ByteConverter.ToString(buffer, ref startIndex, nicknameLength);
        int battery = ByteConverter.ToInt(buffer, ref startIndex);
        int gold = ByteConverter.ToInt(buffer, ref startIndex);
        int diamond = ByteConverter.ToInt(buffer, ref startIndex);
        int rankPoint = ByteConverter.ToInt(buffer, ref startIndex);
        int level = ByteConverter.ToInt(buffer, ref startIndex);
        int exp = ByteConverter.ToInt(buffer, ref startIndex);

        Debug.Log(nicknameLength);
        Debug.Log("Nickname : " + nickname);
        Debug.Log("Battery : " + battery);
        Debug.Log("Gold : " + gold);
        Debug.Log("Diamond : " + diamond);
        Debug.Log($"level : {level}");
        Debug.Log($"exp : {exp}");
        //Debug.Log("RankPoint : " + rankPoint);
        DBManager.instance.userData = new UserData(nickname, battery, gold, diamond, level, exp);

        DBManager.instance.OnLoadedUserData();

        LoginScene_UI loginSceneUI = GameObject.FindObjectOfType<LoginScene_UI>();
        if (!loginSceneUI)
        {
            Debug.LogError("Error LoginScene_UI 없음");
        }
        loginSceneUI.OnSuccessSignIn();
    }

}