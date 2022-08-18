using UnityEngine;
using UnityEditor;


public class OverLapConnection : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("OverLapConnection Unpack");
        Managers.UI.ShowPopupUIAsync<Notice_Popup>();
    }

}