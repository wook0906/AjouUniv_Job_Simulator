// 임의로 Enum 설정
using UnityEngine;
using System.Text;
using System;

public enum EJoinWaitingRoomResult { Ok = 1, Reject }
public static partial class PacketTransmission
{
    public static void SendRequestFriendAddPacket(string nickname)
    {
        byte[] buffer = IOBuffer.Dequeue();

        buffer[0] = PacketInfo.PacketStartNumber;
        int startIndex = 1;

        ByteConverter.FromInt((int)EPacketType.RequestFriendAdd, buffer, ref startIndex);

        ByteConverter.FromInt(0, buffer, ref startIndex);

        ByteConverter.FromInt(nickname.Length, buffer, ref startIndex);
        buffer[startIndex++] = PacketInfo.StringNumber;
        ByteConverter.FromString(nickname, buffer, ref startIndex);


        ClientSocketModule.Send(buffer, startIndex);

        IOBuffer.Enqueue(buffer);
    }

    //1:수락 2:거절
    public static void SendConfirmFriendAddPacket(string nickname, int result)
    {
        byte[] buffer = IOBuffer.Dequeue();

        buffer[0] = PacketInfo.PacketStartNumber;
        int startIndex = 1;

        ByteConverter.FromInt((int)EPacketType.ConfirmFriendAdd, buffer, ref startIndex);

        ByteConverter.FromInt(0, buffer, ref startIndex);

        ByteConverter.FromInt(nickname.Length, buffer, ref startIndex);
        buffer[startIndex++] = PacketInfo.StringNumber;
        ByteConverter.FromString(nickname, buffer, ref startIndex);
        buffer[startIndex++] = PacketInfo.IntNumber;
        ByteConverter.FromInt(result, buffer, ref startIndex);

        ClientSocketModule.Send(buffer, startIndex);

        IOBuffer.Enqueue(buffer);
    }

    public static void SendDeleteFriendPacket(string nickname)
    {
        byte[] buffer = IOBuffer.Dequeue();

        buffer[0] = PacketInfo.PacketStartNumber;
        int startIndex = 1;

        ByteConverter.FromInt((int)EPacketType.DeleteFriend, buffer, ref startIndex);

        ByteConverter.FromInt(0, buffer, ref startIndex);

        ByteConverter.FromInt(nickname.Length, buffer, ref startIndex);
        buffer[startIndex++] = PacketInfo.StringNumber;
        ByteConverter.FromString(nickname, buffer, ref startIndex);


        ClientSocketModule.Send(buffer, startIndex);

        IOBuffer.Enqueue(buffer);
    }

    public static void SendFriendRequestListPacket()
    {
        byte[] buffer = IOBuffer.Dequeue();

        buffer[0] = PacketInfo.PacketStartNumber;
        int startIndex = 1;

        ByteConverter.FromInt((int)EPacketType.FriendRequestList, buffer, ref startIndex);

        ByteConverter.FromInt(0, buffer, ref startIndex);

        ClientSocketModule.Send(buffer, startIndex);

        IOBuffer.Enqueue(buffer);
    }

    public static void SendLoginFriendListPacket()
    {
        byte[] buffer = IOBuffer.Dequeue();

        buffer[0] = PacketInfo.PacketStartNumber;
        int startIndex = 1;

        ByteConverter.FromInt((int)EPacketType.LoginFriendList, buffer, ref startIndex);

        ByteConverter.FromInt(0, buffer, ref startIndex);

        ClientSocketModule.Send(buffer, startIndex);

        IOBuffer.Enqueue(buffer);
    }

    public static void SendFriendListPacket()
    {
        byte[] buffer = IOBuffer.Dequeue();

        buffer[0] = PacketInfo.PacketStartNumber;
        int startIndex = 1;

        ByteConverter.FromInt((int)EPacketType.FriendList, buffer, ref startIndex);

        ByteConverter.FromInt(0, buffer, ref startIndex);

        ClientSocketModule.Send(buffer, startIndex);

        IOBuffer.Enqueue(buffer);
    }

    public static void SendCreateWaitingRoomPacket(int characterNumber, int skinType)
    {
        byte[] buffer = IOBuffer.Dequeue();

        buffer[0] = PacketInfo.PacketStartNumber;
        int startIndex = 1;

        ByteConverter.FromInt((int)EPacketType.CreateWaitingRoom, buffer, ref startIndex);

        ByteConverter.FromInt(0, buffer, ref startIndex);
        ByteConverter.FromInt(characterNumber, buffer, ref startIndex); 
        buffer[startIndex++] = PacketInfo.IntNumber;
        ByteConverter.FromInt(skinType, buffer, ref startIndex);
        ClientSocketModule.Send(buffer, startIndex);

        IOBuffer.Enqueue(buffer);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nickname">초대할 아이디</param>
    /// <param name="seatIdx">자리번호 0에서 3</param>
    public static void SendInviteMyWaitingRoomPacket(string nickname, int seatIdx)
    {
        byte[] buffer = IOBuffer.Dequeue();

        buffer[0] = PacketInfo.PacketStartNumber;
        int startIndex = 1;

        ByteConverter.FromInt((int)EPacketType.InviteMyWaitingRoom, buffer, ref startIndex);

        ByteConverter.FromInt(0, buffer, ref startIndex);

        ByteConverter.FromInt(nickname.Length, buffer, ref startIndex);
        buffer[startIndex++] = PacketInfo.StringNumber;
        ByteConverter.FromString(nickname, buffer, ref startIndex);
        buffer[startIndex++] = PacketInfo.IntNumber;
        ByteConverter.FromInt(seatIdx, buffer, ref startIndex);

        ClientSocketModule.Send(buffer, startIndex);

        IOBuffer.Enqueue(buffer);
    }


    /// <summary>
    /// 초대 요청이 왔을 때, 승낙or거절을 보낸다.
    /// </summary>
    /// <param name="roomid"></param>
    /// <param name="result"></param>
    public static void SendJoinWaitingRoomPacket(int roomid,int seatIdx, EJoinWaitingRoomResult result, int characterNumber, int skinType)
    {
        byte[] buffer = IOBuffer.Dequeue();

        buffer[0] = PacketInfo.PacketStartNumber;
        int startIndex = 1;

        ByteConverter.FromInt((int)EPacketType.JoinWaitingRoom, buffer, ref startIndex);

        ByteConverter.FromInt(0, buffer, ref startIndex);

        ByteConverter.FromInt(roomid, buffer, ref startIndex); 
        buffer[startIndex++] = PacketInfo.IntNumber;
        ByteConverter.FromInt(seatIdx, buffer, ref startIndex);
        buffer[startIndex++] = PacketInfo.IntNumber;
        ByteConverter.FromInt((int)result, buffer, ref startIndex);
        buffer[startIndex++] = PacketInfo.IntNumber;
        ByteConverter.FromInt(characterNumber, buffer, ref startIndex);
        buffer[startIndex++] = PacketInfo.IntNumber;
        ByteConverter.FromInt(skinType, buffer, ref startIndex);
        ClientSocketModule.Send(buffer, startIndex);

        IOBuffer.Enqueue(buffer);
    }
    public static void SendExitWaitingRoomPacket(int roomid, int seatIdx)
    {
        byte[] buffer = IOBuffer.Dequeue();

        buffer[0] = PacketInfo.PacketStartNumber;
        int startIndex = 1;

        ByteConverter.FromInt((int)EPacketType.ExitWaitingRoom, buffer, ref startIndex);

        ByteConverter.FromInt(0, buffer, ref startIndex);


        ByteConverter.FromInt(roomid, buffer, ref startIndex);
        buffer[startIndex++] = PacketInfo.IntNumber;
        ByteConverter.FromInt(seatIdx, buffer, ref startIndex);

        buffer[startIndex++] = PacketInfo.IntNumber;
        ByteConverter.FromInt(Volt_PlayerData.instance.NickName.Length, buffer, ref startIndex);
        buffer[startIndex++] = PacketInfo.StringNumber;
        ByteConverter.FromString(Volt_PlayerData.instance.NickName, buffer, ref startIndex);

        ClientSocketModule.Send(buffer, startIndex);
    }

    public static void SendStartWaitingRoomPacket(int roomid)
    {
        byte[] buffer = IOBuffer.Dequeue();

        buffer[0] = PacketInfo.PacketStartNumber;
        int startIndex = 1;

        ByteConverter.FromInt((int)EPacketType.StartWaitingRoom, buffer, ref startIndex);

        ByteConverter.FromInt(0, buffer, ref startIndex);

        ByteConverter.FromInt(roomid, buffer, ref startIndex);
        ClientSocketModule.Send(buffer, startIndex);

        IOBuffer.Enqueue(buffer);
    }
}