using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeroamState : GameState
{
    public FreeroamState(GameManager gameManager,PauseMenu pauseUI) : base(gameManager, pauseUI) { 
    }

    public override IEnumerator Start()
    {
        yield break;
    }

    public override IEnumerator UIPause()
    {
        GameManager.SetState(new PauseState(GameManager, PauseUI), true);
        GameManager.OpenPauseMenu();
        yield break;
    }


}
