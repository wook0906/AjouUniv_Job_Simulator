using UnityEngine;

public class CharacterPositionSettingCompletionPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        //Debug.Log("CharacterPositionSettingCompletionPacket Unpack");
        //Volt_GameManager.S.SelectBehaviourTypeStart();
        GameController.instance.ChangePhase(new SelectBehaviour());
        
    }
}