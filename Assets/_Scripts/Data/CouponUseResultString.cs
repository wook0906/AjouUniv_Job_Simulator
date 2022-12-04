using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CouponUseResultData
{
    public string couponCode;
    public string stringKey;
}

[CreateAssetMenu(menuName = "StringDatas/CouponUseResultString")]
public class CouponUseResultString : ScriptableObject
{
    [SerializeField]
    public List<CouponUseResultData> EnglishDatas;
    [SerializeField]
    public List<CouponUseResultData> KoreanDatas;
    [SerializeField]
    public List<CouponUseResultData> GermanDatas;
    [SerializeField]
    public List<CouponUseResultData> CommonDatas; // 전 국가 공통
}
