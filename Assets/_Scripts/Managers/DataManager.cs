using System;
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

    private object _lockObj = new object();

    [System.Serializable]
    public class ShopPriceInfoWrapper : IDisposable
    {
        public List<Define.ShopPriceInfo> shopPriceInfo_iOS;

        public void Dispose()
        {
            shopPriceInfo_iOS.Clear();
            GC.SuppressFinalize(this);
        }
    }
    public Dictionary<int /*id*/, ShopPriceInfo> ShopPriceInfos { get; private set; }

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
                Debug.Log("Load Complete VFXPoolData");
                VFXPoolData = result.Result;
                VFXPoolData.Init();
            };

        Addressables.LoadAssetAsync<ObjectPoolData>("ObjectPoolData").Completed +=
            (result) =>
            {
                Debug.Log("Load Complete ObjectPoolData");
                ObjectData = result.Result;
                ObjectData.Init();
            };

        TextAsset jsonText = Resources.Load<TextAsset>("Data/ShopPriceInfo");
        using(ShopPriceInfoWrapper wrapper = JsonUtility.FromJson<ShopPriceInfoWrapper>(jsonText.text))
        {
            ShopPriceInfos = new Dictionary<int, ShopPriceInfo>();
            foreach(var shopPriceInfo in wrapper.shopPriceInfo_iOS)
            {
                ShopPriceInfos.Add(shopPriceInfo.id, shopPriceInfo);
            }
            Debug.Log("Load Complete ShopPriceInfo");
        }
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
