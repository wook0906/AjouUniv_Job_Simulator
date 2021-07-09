using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameSetting/StartingTileSetData")]
public class StartingTileSetData : ScriptableObject
{
    public Define.MapType mapType;

    public List<int> player1;
    public List<int> player2;
    public List<int> player3;
    public List<int> player4;

    public List<int> GetTileIdxData(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                return player1;
            case 2:
                return player2;
            case 3:
                return player3;
            case 4:
                return player4;
            default:
                Debug.Log("GetTileIdxData Error");
                return null;
        }
    }
}
