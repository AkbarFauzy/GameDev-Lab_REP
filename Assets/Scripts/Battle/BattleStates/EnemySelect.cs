using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelect : BattleState
{
    public EnemySelect(BattleManager battleManager) : base(battleManager)
    {

    }

    public override IEnumerator Start() 
    {   
        BattleManager.ChangeCamPriority(6);
        yield return new WaitForFixedUpdate();
        BattleManager.MainCam.ActiveVirtualCamera.LookAt = BattleManager.EnemyObject[BattleManager.EnemyIndex].transform;
        yield break;
    }
    
    // Select Next Enemy
    public override IEnumerator RightTopButton()
    {
        if (!isCD)
        {
            int max = BattleManager.EnemyObject.Count;

            BattleManager.EnemyIndex += 1;
            if (BattleManager.EnemyIndex >= max)
            {
                BattleManager.EnemyIndex = 0;
            }
            BattleManager.MainCam.ActiveVirtualCamera.LookAt = BattleManager.EnemyPos[BattleManager.GetEnemySlot(BattleManager.EnemyIndex)];
            isCD = true;
            yield return new WaitForSeconds(0.2f);
            isCD = false;
        }
    }

    // Select Previous Enemy
    public override IEnumerator LeftTopButton()
    {
        if (!isCD)
        {
            int max = BattleManager.EnemyObject.Count;

            BattleManager.EnemyIndex -= 1;
            if (BattleManager.EnemyIndex <= -1)
            {
                BattleManager.EnemyIndex = max - 1;
            }
            BattleManager.MainCam.ActiveVirtualCamera.LookAt = BattleManager.EnemyPos[BattleManager.GetEnemySlot(BattleManager.EnemyIndex)];
            isCD = true;
            yield return new WaitForSeconds(0.2f);
            isCD = false;
        }
    }

    public override IEnumerator SubmitButton()
    {
        BattleManager.Player[BattleManager.PlayerIndex].Target = BattleManager.EnemyObject[BattleManager.EnemyIndex];
        BattleManager.Player[BattleManager.PlayerIndex].IsTargeting = true;
        BattleManager.SetState(new ActionState(BattleManager));
        yield break;
    }

    public override IEnumerator CancelButton()
    {
        BattleManager.Player[BattleManager.PlayerIndex].CancelAction();
        BattleManager.SetState(new ActionState(BattleManager));
        yield break;
    }

}
