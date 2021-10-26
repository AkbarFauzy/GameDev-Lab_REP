using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterSelectState : GameState
{
    public UICharacterSelectState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Start()
    {
        yield break;
    }

    public override IEnumerator UICancel()
    {
        yield break;
    }

    public override IEnumerator UIRight()
    {
        return base.UIDown();
    }

    public override IEnumerator UILeft()
    {
        return base.UILeft();
    }

}
