using UnityEngine;
using System.Collections;

public abstract class CharacterState
{
    protected BattleManager BattleManager;
    protected Characters Character;

    public CharacterState(BattleManager battleManager, Player character)
    {
        BattleManager = battleManager;
        Character = character;
    }
    public CharacterState(BattleManager battleManager, Enemy character)
    {
        BattleManager = battleManager;
        Character = character;
    }

    public virtual IEnumerator Start() {
        yield break;
    }

    public virtual IEnumerator Attack()
    {
        yield break;
    }

    public virtual IEnumerator Attacked()
    {
        yield break;
    }

    public virtual IEnumerator Skill()
    {
        yield break;
    }

    public virtual IEnumerator Auto() {
        yield break;
    }
}
