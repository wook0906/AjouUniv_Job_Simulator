// 임의로 Enum 설정
using UnityEngine;
using System.Text;
using System;

public static partial class PacketTransmission
{
   
    public static void SendCouponUse(string code)
    {
        Debug.Log("SendCouponUse Packet");
        try
        {
            byte[] buffer = IOBuffer.Dequeue();

            buffer[0] = PacketInfo.PacketStartNumber;
            int startIndex = 1;

            ByteConverter.FromInt((int)EPacketType.CouponUse, buffer, ref startIndex);

            ByteConverter.FromInt(0, buffer, ref startIndex);

            ByteConverter.FromInt(Encoding.UTF8.GetBytes(code).Length, buffer, ref startIndex);
            buffer[startIndex++] = PacketInfo.StringNumber;
            ByteConverter.FromString(code, buffer, ref startIndex);


            ClientSocketModule.Send(buffer, startIndex);

            IOBuffer.Enqueue(buffer);
        }
        catch(Exception ex)
        {
            Debug.LogError($"Faile to send [SendCouponUse]Packet errMsg[{ex.Message}]");
        }
    }
}