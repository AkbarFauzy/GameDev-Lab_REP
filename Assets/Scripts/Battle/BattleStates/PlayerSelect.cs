using UnityEngine;
using System.Collections;

public class PlayerSelect : BattleState
{
    private bool isCD;
    public PlayerSelect(BattleManager battleManager) : base(battleManager) { 
    
    }

    public override IEnumerator Start()
    {
        BattleManager.ChangeCamPriority(BattleManager.GetCharSlot(ref BattleManager.playerIndex));
        yield break;
    }
    public override IEnumerator SubmitButton()
    {
        if (!BattleManager.Player[BattleManager.playerIndex].IsIncapitate()) { 
            BattleManager.Player[BattleManager.playerIndex].SetState(new CharacterAttack(BattleManager, BattleManager.Player[BattleManager.playerIndex]));
            BattleManager.SetState(new EnemySelect(BattleManager));
        }
        yield break;
    }

    public override IEnumerator CancelButton()
    {
        return base.CancelButton();
    }

    // Select Next Player
    public override IEnumerator RightTopButton()
    {
        if (!isCD)
        {
            int max = GameManager.Instance.activeCharacter.Length;

            BattleManager.playerIndex += 1;
            if (BattleManager.playerIndex >= max)
            {
                BattleManager.playerIndex = 0;
            }
            BattleManager.BU.CharPanel[BattleManager.playerIndex].Select();
            BattleManager.ChangeCamPriority(BattleManager.GetCharSlot(ref BattleManager.playerIndex));
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

            BattleManager.playerIndex -= 1;
            if (BattleManager.playerIndex <= -1)
            {
                BattleManager.playerIndex = max - 1;
            }
            BattleManager.BU.CharPanel[BattleManager.playerIndex].Select();
            BattleManager.ChangeCamPriority(BattleManager.GetCharSlot(ref BattleManager.playerIndex));
            BattleManager.BU.gameObject.GetComponent<Carousell>().PreviousPanel();
            isCD = true;
            yield return new WaitUntil(() => BattleManager.BU.GetComponent<Carousell>().IsCD() == false);
            isCD = false;
        }
    }

}
