using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionState : BattleState
{
    public ActionState(BattleManager battleManager):base (battleManager) { 
    
    }

    public override IEnumerator Start()
    {
        BattleManager.IsAction = false;
        BattleManager.ChangeCamPriority(BattleManager.GetCharSlot(BattleManager.PlayerIndex));
        yield break;
    }

    public override IEnumerator SubmitButton()
    {
        if (!BattleManager.Player[BattleManager.PlayerIndex].IsIncapitate() && BattleManager.Player[BattleManager.PlayerIndex].IsAction)
        {
            BattleManager.Player[BattleManager.PlayerIndex].SetState(new CharacterAttack(BattleManager, BattleManager.Player[BattleManager.PlayerIndex]));
            BattleManager.SetState(new EnemySelect(BattleManager));
            BattleManager.BU.commandPanel.SetActive(false);
        }
        yield break;
    }

    public override IEnumerator CancelButton()
    {
        if (!BattleManager.Player[BattleManager.PlayerIndex].IsIncapitate() && BattleManager.Player[BattleManager.PlayerIndex].IsAction)
        {
            BattleManager.SetState(new SkillSelect(BattleManager));
            BattleManager.BU.commandPanel.SetActive(false);
        }
        yield break;
    }

    // Select Next Player
    public override IEnumerator RightTopButton()
    {
        if (!isCD)
        {
            int max = GameManager.Instance.activeCharacter.Length;

            BattleManager.PlayerIndex += 1;
            if (BattleManager.PlayerIndex >= max)
            {
                BattleManager.PlayerIndex = 0;
            }
            BattleManager.BU.CharPanel[BattleManager.PlayerIndex].Select();
            BattleManager.ChangeCamPriority(BattleManager.GetCharSlot(BattleManager.PlayerIndex));
            BattleManager.BU.gameObject.GetComponent<Carousell>().NextPanel();
            isCD = true;
            yield return new WaitUntil(() => BattleManager.BU.GetComponent<Carousell>().IsCD() == false);
            isCD = false;
        }
        yield break;
    }

    //Select Previous Player
    public override IEnumerator LeftTopButton()
    {
        if (!isCD)
        {
            int max = GameManager.Instance.activeCharacter.Length;

            BattleManager.PlayerIndex -= 1;
            if (BattleManager.PlayerIndex <= -1)
            {
                BattleManager.PlayerIndex = max - 1;
            }
            BattleManager.BU.CharPanel[BattleManager.PlayerIndex].Select();
            BattleManager.ChangeCamPriority(BattleManager.GetCharSlot(BattleManager.PlayerIndex));
            BattleManager.BU.gameObject.GetComponent<Carousell>().PreviousPanel();
            isCD = true;
            yield return new WaitUntil(() => BattleManager.BU.GetComponent<Carousell>().IsCD() == false);
            isCD = false;
        }
    }

}
