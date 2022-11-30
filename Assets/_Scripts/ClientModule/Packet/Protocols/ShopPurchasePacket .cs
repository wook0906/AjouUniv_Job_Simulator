using UnityEngine;
using UnityEditor;

public class ShopPurchasePacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("ShopPurchasePacket Unpack");
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        EShopPurchase itemType = (EShopPurchase)ByteConverter.ToInt(buffer, ref startIndex);
        int itemID = ByteConverter.ToInt(buffer, ref startIndex);

        //Debug.Log($"Purchased item type : {itemType.ToString()} item Id : {itemID}");

        ShopScene shopScene = Managers.Scene.CurrentScene as ShopScene;

        if (shopScene)
        {
            Managers.Data.SetPurchaseProductResult(itemType, true);
            Managers.UI.ShowPopupUIAsync<PurchaseComplete_Popup>();
            //Volt_ShopUIManager.S.BoughtItemConfirmPopup(EShopPurchase.Skin, true);
            //구매관련업적반영
            shopScene.RenewShopItemState(itemType, itemID);
        }
        else
        {
            Volt_PlayerData.instance.RenewRobotSkinData(itemID);
        }
    }
}