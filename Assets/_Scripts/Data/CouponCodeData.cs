using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StringDatas/CouponCodeData")]
public class CouponCodeData : ScriptableObject
{
    public List<string> couponCodes;
}
