using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : GameState
{
    public MainMenuState(GameManager gameManager, PauseMenu pauseUI) : base(gameManager, pauseUI) { 
    }

    public override IEnumerator Start()
    {
        yield break;
    }

    public override IEnumerator UICancel()
    {
        GameManager.SetState(new PauseState(GameManager, PauseUI));
        yield break;
    }

    public override IEnumerator UIDown()
    {
        PauseUI.SelectDown();
        yield break;
    }

    public override IEnumerator UIUp()
    {
        PauseUI.SelectUp();
        yield break;
    }

}
