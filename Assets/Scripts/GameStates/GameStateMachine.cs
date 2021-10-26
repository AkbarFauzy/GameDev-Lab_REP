using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    protected GameState State;
    protected Stack<GameState> PreviousState = new Stack<GameState>();

    public void SetState(GameState state, bool isSavePreviousState = false) {
        if (isSavePreviousState) {
            PreviousState.Push(State); 
        }
        State = state;
        StartCoroutine(State.Start());
    }
    public void SetToPreviousState() {

        Debug.Log("asw");
        Debug.Log(PreviousState.Peek() != null);
        if (PreviousState != null)
        {
            SetState(PreviousState.Peek());
            PreviousState.Pop();
            Debug.Log("POOPE");
        }
    }
}
