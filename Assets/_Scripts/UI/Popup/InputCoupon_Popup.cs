using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCoupon_Popup : UI_Popup
{
    enum InputFields
    {
        Coupon_InputField,
    }

    enum Labels
    {
        Input_Label,
        Placeholder_Label,
    }

    enum Buttons
    {
        Ok_Btn,
        ClosePopup_Btn,
    }

    enum Sprites
    {
        Block_BG,
        Loading_img
    }

    public override void Init()
    {
        base.Init();

        Bind<UIInput>(typeof(InputFields));
        Bind<UIButton>(typeof(Buttons));
        Bind<UILabel>(typeof(Labels));
        Bind<UISprite>(typeof(Sprites));

        Get<UIInput>((int)InputFields.Coupon_InputField).onChange.Add(new EventDelegate(() =>
        {
            UILabel inputLabel = Get<UILabel>((int)Labels.Input_Label);
            UILabel placeholder = Get<UILabel>((int)Labels.Placeholder_Label);
            if (inputLabel.text.Length > 0)
                placeholder.gameObject.SetActive(false);
            else
                placeholder.gameObject.SetActive(true);
        }));
        Get<UIInput>((int)InputFields.Coupon_InputField).onSubmit.Add(new EventDelegate(OnClickOkButton));
        Get<UIButton>((int)Buttons.Ok_Btn).onClick.Add(new EventDelegate(OnClickOkButton));
        Get<UIButton>((int)Buttons.ClosePopup_Btn).onClick.Add(new EventDelegate(ClosePopupUI));

        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        Get<UISprite>((int)Sprites.Block_BG).width = screenWidth;
        Get<UISprite>((int)Sprites.Block_BG).height = screenHeight;

        int loadingImgWidth = (int)(screenWidth * 0.1f);
        Get<UISprite>((int)Sprites.Loading_img).width = loadingImgWidth;
        Get<UISprite>((int)Sprites.Loading_img).height = loadingImgWidth;
        Get<UISprite>((int)Sprites.Block_BG).gameObject.SetActive(false);
    }

    private void OnClickOkButton()
    {
        Debug.Log("Submit Coupon code");
        string couponCode = Get<UILabel>((int)Labels.Input_Label).text;
        if (Managers.Data.CouponCodes.Count > 0)
        {
            // 쿠폰이 유효한 쿠폰인지 먼저 확인.
            System.Func<string, bool> isValidCoupon = (userCouponCode) =>
            {
                foreach (string code in Managers.Data.CouponCodes)
                {
                    if (code == userCouponCode)
                        return true;
                }
                return false;
            };

            if (!isValidCoupon(couponCode))
            {
                Debug.Log("Invalid Coupon Code");
                Managers.UI.ShowPopupUIAsync<Alarm_Popup>(() =>
                {
                    Alarm_Popup alarmPopup = GameObject.FindObjectOfType<Alarm_Popup>();
                    alarmPopup.AddOkButtonClickEvent(Define.ClickAction.ClosePopup);
                    alarmPopup.SetMessage(Managers.Data.CouponUseErrorStringDict[ECouponResult.NotExistCoupon]);
                });
                return;
            }
        }
        else
        {
            Debug.LogWarning("데이터 매니저에 쿠폰 데이터가 없다... 이상하다...");
        }
        PacketTransmission.SendCouponUse(couponCode);
        Get<UISprite>((int)Sprites.Block_BG).gameObject.SetActive(true);
        StartCoroutine(AnimateLoading());
    }

    private IEnumerator AnimateLoading()
    {
        Debug.Log("Animate Loading");
        GameObject go = Get<UISprite>((int)Sprites.Loading_img).gameObject;
        while (true)
        {
            go.transform.Rotate(Vector3.forward, 720f * Time.deltaTime);
            yield return null;
        }
    }

    public override void ClosePopupUI()
    {
        StopAllCoroutines();
        Get<UILabel>((int)Labels.Input_Label).text = "";
        Get<UILabel>((int)Labels.Placeholder_Label).gameObject.SetActive(true);
        base.ClosePopupUI();
    }

    public void OnRecvCouponUsePacket(ECouponResult result, string cont)
    {
        Debug.Log("On Receive CouponUserPacket");
        StopAllCoroutines();
        Get<UISprite>((int)Sprites.Block_BG).gameObject.SetActive(false);

        if (result == ECouponResult.OK)
        {
            Debug.Log("Coupon result is OK");
            if (cont != "NONE")
            {
                //주의 ::: 상품이 여러개일 수 있음.
                string[] contents = cont.Split(',');
                foreach (string item in contents)
                {
                    //Gold, Diamond, Battery, Skin, Emoticon
                    string[] arr = item.Split(':');
                    int value = 0;
                    System.Int32.TryParse(arr[1], out value);
                    switch (arr[0])
                    {
                        case "G":
                            //돈을 value만큼 받았음.
                            Debug.Log($"Add {value} Golds");
                            Volt_PlayerData.instance.AddGold(value);
                            break;
                        case "D":
                            //다이아몬드를 value만큼 받았음.
                            Debug.Log($"Add {value} Diamonds");
                            Volt_PlayerData.instance.AddDiamond(value);
                            break;
                        case "B":
                            //배터리를 value만큼 받았음.
                            Debug.Log($"Add {value} Batteries");
                            Volt_PlayerData.instance.AddBattery(value);
                            break;
                        case "S":
                            //Skin id:value 인 것을 받았음.
                            {
                                Debug.Log($"Add {value} Skin");
                                //Volt_PlayerData.instance.RenewRobotSkinData(value);
                                //ShopScene_UI shopSceneUI = GameObject.FindObjectOfType<ShopScene_UI>();
                                //if (shopSceneUI == null)
                                //    break;
                                //shopSceneUI.OnPurchasedRobotSkin(value);
                            }
                            break;
                        case "E":
                            //이모티콘 id:value인 것을 받았음.
                            {
                                Debug.Log($"Add {value} Emo");
                                Volt_PlayerData.instance.RenewEmoticonData(value);
                                ShopScene_UI shopSceneUI = GameObject.FindObjectOfType<ShopScene_UI>();
                                if (shopSceneUI == null)
                                    break;
                                shopSceneUI.OnPurchasedEmoticon(value);
                            }
                            break;
                    }
                }

                UILabel inputLabel = GetLabel((int)Labels.Input_Label);
                Managers.UI.ShowPopupUIAsync<Alarm_Popup>(() =>
                {
                    Alarm_Popup alarmPopup = FindObjectOfType<Alarm_Popup>();
                    alarmPopup.AddOkButtonClickEvent(Define.ClickAction.CloseAllPopup);
                    alarmPopup.SetMessage(Managers.Data.CouponResultStringDcit[inputLabel.text]);
                });
            }
        }
        else
        {
            Debug.Log($"Failed to use Coupon [ErrorCode:{result}]");
            Managers.UI.ShowPopupUIAsync<Alarm_Popup>(() =>
            {
                Alarm_Popup alarmPopup = GameObject.FindObjectOfType<Alarm_Popup>();
                alarmPopup.AddOkButtonClickEvent(Define.ClickAction.ClosePopup);
                alarmPopup.SetMessage(Managers.Data.CouponUseErrorStringDict[result]);
            });
        } 
    }
}
