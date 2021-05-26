using UnityEngine;
using System.Collections;

public abstract class BattleState
{
    protected BattleManager BattleManager;
    protected bool isCD;

    public BattleState(BattleManager battleManager)
    {
        BattleManager = battleManager;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }

    #region Control
    public virtual IEnumerator RightTopButton()
    {
        yield break;
    }

    public virtual IEnumerator LeftTopButton() {
        yield break;
    }

    public virtual IEnumerator SubmitButton() {
        yield break;
    }

    public virtual IEnumerator CancelButton() {
        yield break;
    }
    #endregion
}
