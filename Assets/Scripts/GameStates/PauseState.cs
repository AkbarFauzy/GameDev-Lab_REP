using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : GameState
{
    public PauseState(GameManager gameManager, PauseMenu pauseUI):base(gameManager, pauseUI) { 
    }
    public override IEnumerator Start()
    {
        //GameManager.PauseUi.mainMenuAnim.SetTrigger("Enter");
        yield break;
    }

    public override IEnumerator UICancel()
    {
        // return to previous State;
        //PauseUI.mainMenuAnim.SetTrigger("Exit");
        yield break;
    }

    public override IEnumerator UIPause()
    {
        PauseUI.gameObject.SetActive(false);
        GameManager.SetToPreviousState();
        
        yield break;
    }

    public override IEnumerator UIRight()
    {
        PauseUI.SelectRight();
        yield break;
    }

    public override IEnumerator UILeft()
    {
        PauseUI.SelectLeft();
        yield break;
    }

}
