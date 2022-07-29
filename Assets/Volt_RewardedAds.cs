using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class Volt_RewardedAds : MonoBehaviour
{
    public static Volt_RewardedAds S;
    public RewardedAd rewardedVideoAd;

    //string adUnitId = "ca-app-pub-3940256099942544/5224354917"; //Test ID
    //스토어 공개시 위 아이디는 주석처리, 밑 아이디는 주석 풀고 빌드할것.
#if UNITY_IOS
    string adUnitId = "ca-app-pub-9868066161682340/2358399324";
#else
    string adUnitId = "ca-app-pub-9868066161682340/3890854330";
#endif
    private void Awake()
    {
        S = this;
    }

    public void CreateAd()
    {
        rewardedVideoAd = new RewardedAd(adUnitId);

        //AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).Build();
        //스토어 공개시 위 문장은 주석처리, 밑 문장은 주석 풀고 빌드할것.
        AdRequest request = new AdRequest.Builder().Build();
        rewardedVideoAd.LoadAd(request);
        rewardedVideoAd.OnAdClosed += HandleOnAdClosed;
        rewardedVideoAd.OnAdLoaded += HandleOnAdLoaded;
        rewardedVideoAd.OnAdOpening += HandleOnAdOpening;
        rewardedVideoAd.OnAdFailedToLoad += HandleOnAdFailedToLoaded;
        rewardedVideoAd.OnAdFailedToShow += HandleOnAdFailedToShow;
        //rewardedVideoAd.OnPaidEvent += HandleOnPaidEvent;
        rewardedVideoAd.OnUserEarnedReward += HandleOnUserEarnedReawrd;
    }

   
    public void ShowRewardBasedAd()
    {
        if (rewardedVideoAd.IsLoaded())
        {
            Debug.Log("ShowRewardBaseAd");
            rewardedVideoAd.Show();
        }
        else
        {
            print("Not loaded yet");
        }
    }
    public void HandleOnPaidEvent(object sender, EventArgs args)
    {
        print("On Paid Event");
    }
    public void HandleOnAdFailedToShow(object sender, EventArgs args)
    {
        print("On ad Fail to show");
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        print("On Ad Loaded");
    }
    public void HandleOnAdFailedToLoaded(object sender, AdFailedToLoadEventArgs args)
    {
        LoadAdError loadAdError = args.LoadAdError;

        // Gets the domain from which the error came.
        string domain = loadAdError.GetDomain();

        // Gets the error code. See
        // https://developers.google.com/android/reference/com/google/android/gms/ads/AdRequest
        // and https://developers.google.com/admob/ios/api/reference/Enums/GADErrorCode
        // for a list of possible codes.
        int code = loadAdError.GetCode();

        // Gets an error message.
        // For example "Account not approved yet". See
        // https://support.google.com/admob/answer/9905175 for explanations of
        // common errors.
        string message = loadAdError.GetMessage();

        // Gets the cause of the error, if available.
        AdError underlyingError = loadAdError.GetCause();

        // All of this information is available via the error's toString() method.
        Debug.Log("Load error string: " + loadAdError.ToString());

        // Get response information, which may include results of mediation requests.
        ResponseInfo responseInfo = loadAdError.GetResponseInfo();
        Debug.Log("Response info: " + responseInfo.ToString());
    }
    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        print("On Ad Opening");
    }
    
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        print("On Ad Closed");
        rewardedVideoAd = null;
        CreateAd();
    }

    public void HandleOnUserEarnedReawrd(object sender, EventArgs args)
    {
        print("On User Earned Reward");
        PacketTransmission.SendAdsWatch();
    }
}
