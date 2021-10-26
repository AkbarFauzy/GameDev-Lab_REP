using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelect : BattleState
{
    public SkillSelect(BattleManager battleManager) : base(battleManager)
    {
    }

    public override IEnumerator Start()
    {
        BattleManager.BU.ShowSkillPanel();
        yield break;
    }

    public override IEnumerator SubmitButton()
    {
        yield break;
    }

    public override IEnumerator CancelButton()
    {
        BattleManager.SetState(new ActionState(BattleManager));
        BattleManager.BU.HideSkillPanel();
        BattleManager.BU.commandPanel.SetActive(true);
        yield break;
    }
}
