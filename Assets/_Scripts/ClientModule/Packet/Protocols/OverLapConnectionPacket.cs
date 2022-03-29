using UnityEngine;
using UnityEditor;


public class OverLapConnection : Packet
{
    public override void UnPack(byte[] buffer)
    {
        Debug.Log("OverLapConnection Unpack");
    }

}