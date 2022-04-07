
using UnityEngine;

//ResultPurchasePacket : 패키지purchase 성공시에 패키지id값과 결과값 (0:성공 1실패)날라옵니다.
public class ResultPurchasePacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("ResultPurchasePacket Unpack");
        int startindex = PacketInfo.FromServerPacketSettingIndex;

        int id = ByteConverter.ToInt(buffer, ref startindex);//남은 초
        int result = ByteConverter.ToInt(buffer, ref startindex);


        if(result == 1)
        {
            Managers.UI.ShowPopupUIAsync<PurchaseFail_Popup>();
            //fail( because not enough money)
            return;
        }
        ShopScene_UI shopSceneUI = Managers.UI.GetSceneUI<ShopScene_UI>();
        if (id >= 8000001 && id <= 8000006)
        {
            Volt_PlayerData.instance.RenewPackageData(Managers.Data.CurrentProductInfoShop.ID);
            shopSceneUI.OnPurchasedPackage(Managers.Data.CurrentProductInfoShop.ID);
            Volt_PlayerData.instance.OnPurchasedPackage(id);
        }
        switch (id)
        {
            case 8000002:
                for (int i = 9000001; i <= 9000011; i++)
                {
                    //shopSceneUI.OnPurchasedEmoticon(i);
                    Volt_PlayerData.instance.OnPurchasedEmoticon(i);
                }
                for (int i = 9000101; i <= 9000111; i++)
                {
                    //shopSceneUI.OnPurchasedEmoticon(i);
                    Volt_PlayerData.instance.OnPurchasedEmoticon(i);
                }
                for (int i = 9000201; i <= 9000211; i++)
                {
                    //shopSceneUI.OnPurchasedEmoticon(i);
                    Volt_PlayerData.instance.OnPurchasedEmoticon(i);
                }
                for (int i = 9000301; i <= 9000311; i++)
                {
                    //shopSceneUI.OnPurchasedEmoticon(i);
                    Volt_PlayerData.instance.OnPurchasedEmoticon(i);
                }
                break;
            case 8000003://볼트
                for (int i = 5000030; i <= 5000034; i++)
                {
                    //shopSceneUI.OnPurchasedRobotSkin(i);
                    Volt_PlayerData.instance.OnPurchasedSkin(i);
                }
                break;
            case 8000004://머큐리
                for (int i = 5000001; i <= 5000005; i++)
                {
                    //shopSceneUI.OnPurchasedRobotSkin(i);
                    Volt_PlayerData.instance.OnPurchasedSkin(i);
                }
                break;
            case 8000005://리퍼
                for (int i = 5000010; i <= 5000014; i++)
                {
                    //shopSceneUI.OnPurchasedRobotSkin(i);
                    Volt_PlayerData.instance.OnPurchasedSkin(i);
                }
                break;
            case 8000006://하운드
                for (int i = 5000020; i <= 5000024; i++)
                {
                    //shopSceneUI.OnPurchasedRobotSkin(i);
                    Volt_PlayerData.instance.OnPurchasedSkin(i);
                }
                break;
            default:
                break;
        }
    }
}
