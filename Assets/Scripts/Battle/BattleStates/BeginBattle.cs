using UnityEngine;
using System.Collections;

public class BeginBattle : BattleState
{
    public BeginBattle(BattleManager battleManager) : base(battleManager) { 
    }

    public override IEnumerator Start()
    {
        //some logic
        BattleManager.BU.CharPanel[0].Select();
        //BattleManager.BU.SelectedButton = BattleManager.BU.CommandPanel[0];

        yield return new WaitForSeconds(1f);
        BattleManager.SetState(new ActionState(BattleManager));
    }

}
