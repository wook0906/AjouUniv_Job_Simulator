using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhaseBase : MonoBehaviour
{
    public bool phaseDone = false;
    public Define.Phase type = Define.Phase.None;

    public virtual void OnEnterPhase(GameController game)
    {

    }

    public virtual void OnExitPhase(GameController game)
    {
        Destroy(GetComponent<PhaseBase>());
    }

    public virtual void OnTouchTileBegin(Volt_Tile tile)
    {

    }
    public virtual void OnTouchTile(Volt_Tile tile)
    {

    }
    public virtual void OnTouchTileEnd(Volt_Tile tile)
    {

    }

    public abstract IEnumerator Action(GameData data);

}
