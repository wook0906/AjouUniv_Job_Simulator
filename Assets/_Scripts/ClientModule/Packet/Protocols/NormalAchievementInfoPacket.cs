using UnityEngine;
using UnityEditor;

public class NormalAchievementInfoPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("NormalAchievementInfoPacket Unpack");
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        int rowCount = ByteConverter.ToInt(buffer, ref startIndex);

        int id;
        int conditionType;
        int condition;
        int rewardType;
        int reward, reward2, reward3, reward4;

        for (int i = 0; i < rowCount; i++)
        {
            id = ByteConverter.ToInt(buffer, ref startIndex);
            conditionType = ByteConverter.ToInt(buffer, ref startIndex);
            condition = ByteConverter.ToInt(buffer, ref startIndex);
            rewardType = ByteConverter.ToInt(buffer, ref startIndex);
            reward = ByteConverter.ToInt(buffer, ref startIndex);//골드
            reward2 = ByteConverter.ToInt(buffer, ref startIndex);//다이아
            reward3 = ByteConverter.ToInt(buffer, ref startIndex);//배터리
            reward4 = ByteConverter.ToInt(buffer, ref startIndex);//스킨

            Define.ACHReward rewardInfo = new Define.ACHReward();
            rewardInfo.gold = reward;
            rewardInfo.diamond = reward2;
            rewardInfo.battery = reward3;
            rewardInfo.skinType = reward4;


            //DBManager.instance.normalACHConditionInfos.Add(new InfoACHCondition(id, conditionType, condition, rewardType, reward));
            DBManager.instance.normalACHConditionInfos.Add(new InfoACHCondition(id, conditionType, condition, rewardInfo));

            Debug.Log("ID : " + id);
            Debug.Log("Condition Type : " + conditionType.ToString());
            Debug.Log("Condition : " + condition);
            Debug.Log("Reward Type: " + rewardType.ToString());

            Debug.Log("RewardGold : " + rewardInfo.gold);
            Debug.Log("RewardDia : " + rewardInfo.diamond);
            Debug.Log("RewardBattery : " + rewardInfo.battery);
            Debug.Log("RewardSkin : " + rewardInfo.skinType);
        }


        DBManager.instance.OnLoadedNormalAchievementInfo();
    }

}