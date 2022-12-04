using UnityEngine;

public class PackageShopInfoPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("PackageShopInfoPacket Unpack");
#if UNITY_IOS
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        int rowCount = ByteConverter.ToInt(buffer, ref startIndex);

        int id;
        int assetType;
        int price;

        for (int i = 0; i < rowCount; i++)
        {
            id = ByteConverter.ToInt(buffer, ref startIndex);
            assetType = ByteConverter.ToInt(buffer, ref startIndex);
            price = ByteConverter.ToInt(buffer, ref startIndex);

            DBManager.instance.packageShopInfos.Add(new InfoShop(id, assetType
                , price, 1));
        }
#else
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        int rowCount = ByteConverter.ToInt(buffer, ref startIndex);

        int id;
        int assetType;
        int price;

        for (int i = 0; i < rowCount; i++)
        {
            id = ByteConverter.ToInt(buffer, ref startIndex);
            assetType = ByteConverter.ToInt(buffer, ref startIndex);
            price = ByteConverter.ToInt(buffer, ref startIndex);

            Debug.Log($"package id : {id}");
            Debug.Log($"package id : {assetType}");
            Debug.Log($"package id : {price}");
            DBManager.instance.packageShopInfos.Add(new InfoShop(id, assetType, price, 1));
        }
#endif
        DBManager.instance.OnLoadedPackageShopInfo();
    }
}

