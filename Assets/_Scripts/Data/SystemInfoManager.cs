using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemInfoManager : MonoBehaviour
{
    public static SystemInfoManager instance;
    public Dictionary<int, InfoShop> shopInfos = new Dictionary<int, InfoShop>();
    public Dictionary<int, InfoACHCondition> achConditionInfos = new Dictionary<int, InfoACHCondition>();

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    
    public void ClearSystemInfo()
    {
        shopInfos.Clear();
        achConditionInfos.Clear();
    }
    public bool InitSystemInfo(List<InfoShop> packageShop, List<InfoShop> batteyShop, List<InfoShop> diamondShop, List<InfoShop> goldShop, List<InfoShop> frameDecoShop, List<InfoShop> robotSkinShop, List<InfoShop> emoticonShop, List<InfoACHCondition> daliy,
        List<InfoACHCondition> normal)
    {
        try
        {
            ClearSystemInfo();
            Debug.Log($"InitSystemInfo daily DailyAch:{daliy.Count}, NormalAch:{normal.Count}");
            foreach (var item in packageShop)
            {
                if (!shopInfos.ContainsKey(item.ID))
                    shopInfos.Add(item.ID, new InfoShop(item.ID, item.priceAssetType, item.price, item.count));
                else
                {
                    Debug.LogWarning($"[PackageShop] 이미 존재하는 키 key:{item.ID}");
                    shopInfos[item.ID] = new InfoShop(item.ID, item.priceAssetType, item.price, item.count);
                }
                Debug.Log($"PackageShop Info {item.ToString()}");
            }
            foreach (var item in batteyShop)
            {
                if (!shopInfos.ContainsKey(item.ID))
                    shopInfos.Add(item.ID, new InfoShop(item.ID, item.priceAssetType, item.price, item.count));
                else
                {
                    Debug.LogWarning($"[batteyShop] 이미 존재하는 키 key:{item.ID}");
                    shopInfos[item.ID] = new InfoShop(item.ID, item.priceAssetType, item.price, item.count);
                }
                //shopInfos.Add(item.ID, new InfoShop(item.ID, item.priceAssetType, item.price, item.count));
                //Debug.Log($"BatteryShop Info {item.ToString()}");
            }
            foreach (var item in diamondShop)
            {
                if (!shopInfos.ContainsKey(item.ID))
                    shopInfos.Add(item.ID, new InfoShop(item.ID, item.priceAssetType, item.price, item.count));
                else
                {
                    Debug.LogWarning($"[diamondShop] 이미 존재하는 키 key:{item.ID}");
                    shopInfos[item.ID] = new InfoShop(item.ID, item.priceAssetType, item.price, item.count);
                }
                //shopInfos.Add(item.ID, new InfoShop(item.ID, item.priceAssetType, item.price, item.count));

                //Debug.Log($"DiamondShop Info {item.ToString()}");
            }
            foreach (var item in goldShop)
            {
                if (!shopInfos.ContainsKey(item.ID))
                    shopInfos.Add(item.ID, new InfoShop(item.ID, item.priceAssetType, item.price, item.count));
                else
                {
                    Debug.LogWarning($"[goldShop] 이미 존재하는 키 key:{item.ID}");
                    shopInfos[item.ID] = new InfoShop(item.ID, item.priceAssetType, item.price, item.count);
                }
                //shopInfos.Add(item.ID, new InfoShop(item.ID, item.priceAssetType, item.price, item.count));

                //Debug.Log($"GoldShop Info {item.ToString()}");
            }
            foreach (var item in frameDecoShop)
            {
                if (!shopInfos.ContainsKey(item.ID))
                    shopInfos.Add(item.ID, new InfoShop(item.ID, item.priceAssetType, item.price, item.count));
                else
                {
                    Debug.LogWarning($"[frameDecoShop] 이미 존재하는 키 key:{item.ID}");
                    shopInfos[item.ID] = new InfoShop(item.ID, item.priceAssetType, item.price, item.count);
                }
                //shopInfos.Add(item.ID, new InfoShop(item.ID, item.priceAssetType, item.price,
                //   item.count));
                //Debug.Log($"FrameDecoShop Info {item.ToString()}");
            }
            foreach (var item in robotSkinShop)
            {
                if (!shopInfos.ContainsKey(item.ID))
                    shopInfos.Add(item.ID, new InfoShop(item.ID, item.priceAssetType, item.price, item.count));
                else
                {
                    Debug.LogWarning($"[robotSkinShop] 이미 존재하는 키 key:{item.ID}");
                    shopInfos[item.ID] = new InfoShop(item.ID, item.priceAssetType, item.price, item.count);
                }
                //shopInfos.Add(item.ID, new InfoShop(item.ID, item.priceAssetType, item.price,
                //   item.count));
                //Debug.Log($"RobotSkinShop Info {item.ToString()}");
            }
            foreach (var item in emoticonShop)
            {
                if (!shopInfos.ContainsKey(item.ID))
                    shopInfos.Add(item.ID, new InfoShop(item.ID, item.priceAssetType, item.price, item.count));
                else
                {
                    Debug.LogWarning($"[emoticonShop] 이미 존재하는 키 key:{item.ID}");
                    shopInfos[item.ID] = new InfoShop(item.ID, item.priceAssetType, item.price, item.count);
                }
                //shopInfos.Add(item.ID, new InfoShop(item.ID, item.priceAssetType, item.price,
                //   item.count));
                //Debug.Log($"EmoticonShop Info {item.ToString()}");
            }
            foreach (var item in daliy)
            {
                if(!achConditionInfos.ContainsKey(item.ID))
                    achConditionInfos.Add(item.ID, new InfoACHCondition(item.ID, item.conditionType, item.condition, item.reward));//item.rewardType, item.reward));
                else
                {
                    Debug.LogWarning($"[daliyACH] 이미 존재하는 키 key:{item.ID}");
                    achConditionInfos[item.ID] = new InfoACHCondition(item.ID, item.conditionType, item.condition, item.reward);
                }
                Debug.Log($"Daily ACH condtionInfo ID:{item.ID}, {item.ToString()}");
            }
            foreach (var item in normal)
            {
                if (!achConditionInfos.ContainsKey(item.ID))
                    achConditionInfos.Add(item.ID, new InfoACHCondition(item.ID, item.conditionType, item.condition, item.reward));//item.rewardType, item.reward));
                else
                {
                    Debug.LogWarning($"[normalACH] 이미 존재하는 키 key:{item.ID}");
                    achConditionInfos[item.ID] = new InfoACHCondition(item.ID, item.conditionType, item.condition, item.reward);
                }
                //achConditionInfos.Add(item.ID, new InfoACHCondition(item.ID, item.conditionType, item.condition, item.reward));
                Debug.Log($"Normal ACH ConditionInfo ID:{item.ID}, {item.ToString()}");
            }
        }
        catch(System.Exception ex)
        {
            Debug.LogError("InitSystemInfo Faile Error : " + ex.Message);
            return false;
        }
        return true;
    }
    public bool InitACHInfo(List<InfoACHCondition> daily, List<InfoACHCondition> normal)
    {
        try
        {
            achConditionInfos.Clear();
            foreach (var item in daily)
            {
                achConditionInfos.Add(item.ID, new InfoACHCondition(item.ID, item.conditionType, item.condition, item.reward));
                //Debug.Log($"Daily ACH condtionInfo {item.ToString()}");
            }
            foreach (var item in normal)
            {
                achConditionInfos.Add(item.ID, new InfoACHCondition(item.ID, item.conditionType, item.condition, item.reward));
                //Debug.Log($"Normal ACH ConditionInfo {item.ToString()}");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error : " + ex.Message);
            return false;
        }
        return true;

    }
    
}
