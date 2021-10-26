using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : CharacterState
{
    public CharacterAttack(BattleManager battleManager, Enemy character) : base(battleManager, character) { 
    
    }
    public CharacterAttack(BattleManager battleManager, Player character) : base(battleManager, character)
    {

    }

    public override IEnumerator Start() {
        Character.DoNormalAttack();
        yield break;
    }

    public override IEnumerator Attack()
    {
        switch (Character.attackType) {
            case (AttackType.melee):
                yield return new WaitUntil(() => !Character.IsIncapitate());
                yield return new WaitUntil(() => Character.IsTargeting);
                Character.RunToTarget(Character.Target.transform.position);

                yield return new WaitUntil(() => !Character.IsRunning);

                Character.anim.SetInteger("Attack_ID",0);
                Character.anim.SetTrigger("isAttacking");

                yield return new WaitUntil(() => !Character.IsTargeting);
                Character.RunToOriginalSpot(Character.Slot);

                break;
            case (AttackType.ranged):

                break;
            default:
                break;
        }
        yield break;
    }
}
