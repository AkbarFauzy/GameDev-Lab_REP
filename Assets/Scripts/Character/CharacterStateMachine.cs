using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    protected CharacterState State;
    protected IEnumerator MainCoroutine;

    public void SetState(CharacterState state) {
        State = state;
        StartCoroutine(State.Start());
    
    }

}
