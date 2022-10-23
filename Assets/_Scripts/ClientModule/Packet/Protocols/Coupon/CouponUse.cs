using System;
using UnityEditor;
using UnityEngine;


public enum ECouponResult { OK = 1, NotExistCoupon, OverPeriod, AlreadySupply, NotHave, IncorrectProduct, NotRemain }

//쿠폰 사용후(SendCouponUse) 결과 값.
public class CouponUse : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        ECouponResult result = (ECouponResult)ByteConverter.ToInt(buffer, ref startIndex);

        //내가 보냈던 코드
        int codeLen = ByteConverter.ToInt(buffer, ref startIndex);
        string code = ByteConverter.ToString(buffer,ref startIndex, codeLen);

        //받은 상품
        int contentLen = ByteConverter.ToInt(buffer, ref startIndex);
        string cont = ByteConverter.ToString(buffer, ref startIndex, contentLen);

        switch (result)
        {
            case ECouponResult.OK:
                //쿠폰을 올바르게 지급받았어여.
                //주의 ::: 상품이 여러개일 수 있음.
                if(cont != "NONE")
                {
                    string[] contents = cont.Split(',');
                    foreach (string item in contents)
                    {
                        //Gold, Diamond, Battery, Skin, Emoticon
                        string[] arr = item.Split(':');
                        int value = 0;
                        Int32.TryParse(arr[1], out value);
                        switch (arr[0])
                        {
                            case "G":
                                //돈을 value만큼 받았음.
                                Volt_PlayerData.instance.AddGold(value);
                                break;
                            case "D":
                                //다이아몬드를 value만큼 받았음.
                                Volt_PlayerData.instance.AddDiamond(value);
                                break;
                            case "B":
                                //배터리를 value만큼 받았음.
                                Volt_PlayerData.instance.AddBattery(value);
                                break;
                            case "S":
                                //Skin id:value 인 것을 받았음.
                                {
                                    Volt_PlayerData.instance.RenewRobotSkinData(value);
                                    ShopScene_UI shopSceneUI = GameObject.FindObjectOfType<ShopScene_UI>();
                                    if (shopSceneUI == null)
                                        break;
                                    shopSceneUI.OnPurchasedRobotSkin(value);
                                }
                                break;
                            case "E":
                                //이모티콘 id:value인 것을 받았음.
                                {
                                    Volt_PlayerData.instance.RenewEmoticonData(value);
                                    ShopScene_UI shopSceneUI = GameObject.FindObjectOfType<ShopScene_UI>();
                                    if (shopSceneUI == null)
                                        break;
                                    shopSceneUI.OnPurchasedEmoticon(value);
                                }
                                break;
                        }
                    }
                }
               
                break;
            case ECouponResult.AlreadySupply:
                //이미 썻어요ㅎㅎ
                break;
            case ECouponResult.OverPeriod:
                //쿠폰 기간이 지났음.
                break;
            case ECouponResult.NotExistCoupon:
                //존재하지 않는 쿠폰
                //쿠폰 코드를 확인해주시길 바랍니다.
                break;
            case ECouponResult.IncorrectProduct:
                //쿠폰테이블에 들어간 상품정보가 올바르지 않을때.
                //하나라도 틀린게 껴있으면 아무고토 지급되지 않아요.
                //관리자에게 문의바람
                break;
            case ECouponResult.NotHave:
                //본인에게 지급된 쿠폰이 아니예요.
                //(확장성 때문에 넣었움.)
                break;
            case ECouponResult.NotRemain:
                //선착순 쿠폰이 모두 소진되었어용~
                break;
        }





    }
}