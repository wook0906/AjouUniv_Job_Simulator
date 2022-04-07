using UnityEngine;

public class PowerOwnPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("PowerOwnPacket Unpack");
        int startIndex = PacketInfo.FromServerPacketSettingIndex;

        bool skipAds = ByteConverter.ToBool(buffer, ref startIndex);

        for (int i = 8000001; i <= 8000006; i++)
        {
            DBManager.instance.userPackageCondition.Add(i, new Define.PackageProductState(false));
        }

        DBManager.instance.userPackageCondition[8000001] = new Define.PackageProductState(skipAds);
        DBManager.instance.OnLoadedUserPackageCondition();
    }
}

