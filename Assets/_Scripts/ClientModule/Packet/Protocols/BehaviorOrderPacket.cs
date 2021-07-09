using UnityEngine;

public class BehaviorOrderPacket : Packet
{
    public override void UnPack(byte[] buffer)
    {
        int startIndex = 14;


        // 예시
        int playerNumber = ByteConverter.ToInt(buffer, ref startIndex);
        int orderNumber = ByteConverter.ToInt(buffer, ref startIndex);
        int robotState = ByteConverter.ToInt(buffer, ref startIndex);
        int behaviourPoints = ByteConverter.ToInt(buffer, ref startIndex);
        int directionStrLength = ByteConverter.ToInt(buffer, ref startIndex);
        Vector3 direction = Volt_Utils.StringToVector3(ByteConverter.ToString(buffer, ref startIndex, directionStrLength));
        BehaviourType behaviourType = (BehaviourType)ByteConverter.ToInt(buffer, ref startIndex);
        
        //Debug.Log("playerNumber "+playerNumber +" BehaviourInfo Arrived" );
        //Debug.Log("order Number :" + orderNumber);
        //Debug.Log("Robot state : "+robotState.ToString());
        //Debug.Log("points : "+ behaviourPoints);
        //Debug.Log("direction : " + direction);
        //Debug.Log("behaviourType : " + behaviourType.ToString());

        

        Volt_Robot robot = Volt_PlayerManager.S.GetPlayerByPlayerNumber(playerNumber).playerRobot.GetComponent<Volt_Robot>();

        Volt_Tile selectTile = Volt_ArenaSetter.S.GetTile(robot.transform.position, direction, behaviourPoints);
        
        Volt_RobotBehavior behaviour = new Volt_RobotBehavior(behaviourPoints, direction, behaviourType, playerNumber,selectTile);
        behaviour.OrderNumber = orderNumber;
        // 체크 부분 필요!!
        // if(체크 완료-모두 완료)
        // Send Completion

        Volt_RobotBehavior newBehaviour = null;
        if(GameController.instance.gameData.behaviours.TryGetValue(playerNumber, out newBehaviour))
        {
            GameController.instance.gameData.behaviours[playerNumber] = behaviour;
        }
        else
        {
            if (!GameController.instance.gameData.behaviours.ContainsKey(playerNumber))
            {
                GameController.instance.gameData.behaviours.Add(playerNumber, behaviour);
            }
        }
        //Volt_GameManager.S.RobotBehaviourInputToManager(behaviour);
    }
}