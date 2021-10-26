using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class GameState
{
    protected GameManager GameManager;
    protected PauseMenu PauseUI;
    protected float cdCounter;

    public GameState(GameManager gameManager) {
        GameManager = gameManager;
    }

    public GameState(GameManager gameManager, PauseMenu pauseUI) {
        GameManager = gameManager;
        PauseUI = pauseUI;
        cdCounter = 0;
    }

    public virtual IEnumerator Start() {
        yield break;
    }

    public virtual IEnumerator UIPause() {
        yield break;
    }

    public virtual IEnumerator UISubmit() {
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        yield break;
    }
    public virtual IEnumerator UICancel() {
        GameManager.SetToPreviousState();
        yield break;
    }
    public virtual IEnumerator UIRight()
    {
        yield break;
    }
    public virtual IEnumerator UILeft() {
        yield break;
    }
    public virtual IEnumerator UIDown() {
        yield break;
    }
    public virtual IEnumerator UIUp()
    {
        yield break;
    }

}
