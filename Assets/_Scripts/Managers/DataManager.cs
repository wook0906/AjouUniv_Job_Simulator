using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static Define;

public class DataManager
{
    public Dictionary<RobotType, RobotSkinObject> SkinDatas = new Dictionary<RobotType, RobotSkinObject>();

    //private AsyncOperationHandle skinDatasLoadHandle;
    public EBenefitType CurrentGetBenefitType { private set; get; }
    public int GetBenefitResult { private set; get; }
    public InfoShop CurrentProductInfoShop { private set; get; }
    public EShopPurchase CurrentProductType { private set; get; }
    public PurchaseProductResult PurchaseProductResult { private set; get; }

    public VFXPoolData VFXPoolData { private set; get; } = null;
    public ObjectPoolData ObjectData { private set; get; } = null;

    public Dictionary<ECouponResult, string /*string key*/> CouponUseErrorStringDict { private set; get; } = new Dictionary<ECouponResult, string>();
    public Dictionary<string, string /*coupon code, string key*/> CouponResultStringDcit { private set; get; } = new Dictionary<string, string>();
    public List<string /*coupon code*/> CouponCodes { private set; get; } = new List<string>();
    private object _lockObj = new object();
    public void Init()
    {
        Debug.Log("Init DataManager");
        int maxRobotType = (int)RobotType.Max;
        for(int i = 0; i < maxRobotType; ++i)
        {
            RobotType type = (RobotType)i;
            Addressables.LoadAssetAsync<RobotSkinObject>($"Assets/SkinDatas/{type}_SkinData.asset").Completed += 
                (result) =>
                {
                    lock(_lockObj)
                    {
                        Debug.Log($"Load Robot SkinData {result.Result.robotType}");
                        SkinDatas.Add(result.Result.robotType, result.Result);
                        Volt_PlayerData.instance.LoadUserSkinData(result.Result.robotType);
                    }
                };
        }
        //skinDatasLoadHandle = Addressables.LoadAssetsAsync<RobotSkinObject>("SkinDatas",
        //    (result) =>
        //    {
        //        SkinDatas.Add(result.robotType, result);
        //    });

        //skinDatasLoadHandle.Completed += (result) =>
        //{
        //    Volt_PlayerData.instance.LoadUserSkinData();
        //};

        Addressables.LoadAssetAsync<VFXPoolData>("VFXPoolData").Completed +=
            (result) =>
            {
                VFXPoolData = result.Result;
                VFXPoolData.Init();
            };

        Addressables.LoadAssetAsync<ObjectPoolData>("ObjectPoolData").Completed +=
            (result) =>
            {
                ObjectData = result.Result;
                ObjectData.Init();
            };

        Addressables.LoadAssetAsync<CouponCodeData>("Assets/_ScriptableObjects/CouponCodeData.asset").Completed +=
            (result) =>
            {
                CouponCodeData data = result.Result;
                CouponCodes = data.couponCodes;
            };

        Addressables.LoadAssetAsync<CouponUseErrorString>("Assets/_ScriptableObjects/CouponUseErrorStringData.asset").Completed +=
            (result) =>
            {
                CouponUseErrorString data = result.Result;
                int startIdx = (int)ECouponResult.NotExistCoupon;
                int count = (int)ECouponResult.Max;
                for (int i = startIdx; i < count; ++i)
                {
                    ECouponResult type = (ECouponResult)i;
                    string key = data.GetStringKey(type);
                    if (string.IsNullOrEmpty(key))
                    {
                        Debug.LogError($"Doesn't exist Coupon Result ErrorCode [type:{type}]");
                        continue;
                    }
                    CouponUseErrorStringDict.Add(type, data.GetStringKey(type));
                }
            };

        Addressables.LoadAssetAsync<CouponUseResultString>("Assets/_ScriptableObjects/CouponUseResultStringData.asset").Completed +=
            (result) =>
            {
                CouponUseResultString stringData = result.Result;
                List<CouponUseResultData> couponUseResultDataList = null;

                // 유저의 시스템 언어에따라 쿠폰 코드가 달라질 수 있기 때문에...
                switch (Application.systemLanguage)
                {
                    case SystemLanguage.English:
                        couponUseResultDataList = stringData.EnglishDatas;
                        break;
                    case SystemLanguage.German:
                        couponUseResultDataList = stringData.GermanDatas;
                        break;
                    case SystemLanguage.Korean:
                        couponUseResultDataList = stringData.KoreanDatas;
                        break;
                    default:
                        Debug.LogError("Error not support language");
                        break;
                }

                if (couponUseResultDataList == null)
                    return;

                // 전 국가 공통 내용들 추가
                couponUseResultDataList.AddRange(stringData.CommonDatas);
                foreach (var couponUseResultData in couponUseResultDataList)
                {
                    CouponResultStringDcit.Add(couponUseResultData.couponCode, couponUseResultData.stringKey);
                }
            };
    }

    public void SetBenefitInfo(EBenefitType benefitType, int result)
    {
        CurrentGetBenefitType = benefitType;
        GetBenefitResult = result;
    }

    public void SetPurchaseProductInfo(InfoShop info, EShopPurchase type)
    {
        this.CurrentProductInfoShop = info;
        this.CurrentProductType = type;
    }

    public void SetPurchaseProductResult(EShopPurchase type, bool isSuccess)
    {
        SetPurchaseProductResult(new PurchaseProductResult(type, isSuccess));
    }

    public void SetPurchaseProductResult(PurchaseProductResult result)
    {
        PurchaseProductResult = result;
    }
}
