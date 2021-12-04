using UnityEditor;
using UnityEngine;



//호스트가 방을 나가서 방이 파기됨.
//호스트가 나가서 방이 닫혔다는 패킷을 모두에게 전송한 것.

public class CloseWaitingRoom : Packet
{
    public override void UnPack(byte[] buffer)
    {
        //Data 부분없음.

    }
}