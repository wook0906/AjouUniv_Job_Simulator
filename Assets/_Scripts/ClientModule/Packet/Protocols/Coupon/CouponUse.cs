using System;
using UnityEditor;
using UnityEngine;


public enum ECouponResult 
{ 
    OK = 1, //쿠폰을 올바르게 지급받았어여.
    NotExistCoupon, //존재하지 않는 쿠폰. 쿠폰 코드를 확인해주시길 바랍니다.
    OverPeriod, //쿠폰 기간이 지났음.
    AlreadySupply, //이미 썻어요ㅎㅎ
    NotHave, //본인에게 지급된 쿠폰이 아니예요.
    IncorrectProduct, //쿠폰테이블에 들어간 상품정보가 올바르지 않을때.
    NotRemain, //선착순 쿠폰이 모두 소진되었어용~
    Max
}

//쿠폰 사용후(SendCouponUse) 결과 값.
public class CouponUse : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("CouponUse Unpack");
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        ECouponResult result = (ECouponResult)ByteConverter.ToInt(buffer, ref startIndex);

        //내가 보냈던 코드
        int codeLen = ByteConverter.ToInt(buffer, ref startIndex);
        string code = ByteConverter.ToString(buffer,ref startIndex, codeLen);

        //받은 상품
        int contentLen = ByteConverter.ToInt(buffer, ref startIndex);
        string cont = ByteConverter.ToString(buffer, ref startIndex, contentLen);

        InputCoupon_Popup inputCouponPopup = GameObject.FindObjectOfType<InputCoupon_Popup>();
        if(inputCouponPopup == null)
        {
            Debug.LogError("inputCouponPopup is null");
            return;
        }
        inputCouponPopup.OnRecvCouponUsePacket(result, cont);
    }
}