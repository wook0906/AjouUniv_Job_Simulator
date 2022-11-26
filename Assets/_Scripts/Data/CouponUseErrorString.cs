using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CouponUseErrorData
{
    public ECouponResult result;
    public string stringKey;
}

[CreateAssetMenu(menuName = "StringDatas/CouponUseErrorString")]
public class CouponUseErrorString : ScriptableObject
{
    [SerializeField]
    public List<CouponUseErrorData> datas;

    public string GetStringKey(ECouponResult type)
    {
        foreach (var item in datas)
        {
            if (item.result == type)
                return item.stringKey;
        }
        return string.Empty;
    }
}
