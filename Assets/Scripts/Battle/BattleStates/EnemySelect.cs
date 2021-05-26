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
        BattleManager.MainCam.ActiveVirtualCamera.LookAt = BattleManager.EnemyObject[BattleManager.enemyIndex].transform;
        yield break;
    }
    
    // Select Next Enemy
    public override IEnumerator RightTopButton()
    {
        if (!isCD)
        {
            int max = BattleManager.EnemyObject.Count;

            BattleManager.enemyIndex += 1;
            if (BattleManager.enemyIndex >= max)
            {
                BattleManager.enemyIndex = 0;
            }
            BattleManager.MainCam.ActiveVirtualCamera.LookAt = BattleManager.EnemyPos[BattleManager.GetEnemySlot(ref BattleManager.enemyIndex)];
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

            BattleManager.enemyIndex -= 1;
            if (BattleManager.enemyIndex <= -1)
            {
                BattleManager.enemyIndex = max - 1;
            }
            BattleManager.MainCam.ActiveVirtualCamera.LookAt = BattleManager.EnemyPos[BattleManager.GetEnemySlot(ref BattleManager.enemyIndex)];
            isCD = true;
            yield return new WaitForSeconds(0.2f);
            isCD = false;
        }
    }

    public override IEnumerator SubmitButton()
    {
        return base.SubmitButton();
    }

    public override IEnumerator CancelButton()
    {
        BattleManager.Player[BattleManager.playerIndex].CancelAction();
        BattleManager.SetState(new PlayerSelect(BattleManager));
        yield break;
    }

}
