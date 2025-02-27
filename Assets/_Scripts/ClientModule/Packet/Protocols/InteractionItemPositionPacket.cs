﻿using UnityEngine;

public class InteractionItemPositionPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        
        int startIndex = PacketInfo.FromServerPacketDataStartIndex;
        Card cardType = (Card)ByteConverter.ToInt(buffer, ref startIndex);
        int vpIdx = ByteConverter.ToInt(buffer, ref startIndex);
        int repairKitIdx = ByteConverter.ToInt(buffer, ref startIndex);
        int moduleIdx = ByteConverter.ToInt(buffer, ref startIndex);

        //Debug.Log("cardType : " + cardType);
        //Debug.Log("vpIdx : " + vpIdx);
        //Debug.Log("repairKit : " + repairKitIdx);
        //Debug.Log("moduleIdx : " + moduleIdx);

        //Debug.Log("moduleType : " + cardType.ToString());

        //if (GameController.instance.gameData.round == 1)
            //GameController.instance.gameData.drawedCard = Card.AMARGEDDON;//cardType;
        //else
            GameController.instance.gameData.drawedCard = cardType;
        GameController.instance.gameData.vpIdx = vpIdx;
        GameController.instance.gameData.repairIdx = repairKitIdx;
        GameController.instance.gameData.moduleIdx = moduleIdx;


        GameController.instance.ChangePhase<ItemSetup>();
        //Volt_GameManager.S.ItemSetupStart(vpIdx, repairKitIdx, moduleIdx, cardType);
        
        //필드설정 완료 후 기다리고있으면 날아오는패킷.
        //아이템 포지션 담겨서 날아올거임.
    }
}