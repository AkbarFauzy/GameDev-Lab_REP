using UnityEngine;
using System.Collections;

public class BattleStateMachine : MonoBehaviour
{
    protected BattleState State;

    //Constructor
    public void SetState(BattleState state)
    {
        State = state;
        StartCoroutine(State.Start());
    }

}
