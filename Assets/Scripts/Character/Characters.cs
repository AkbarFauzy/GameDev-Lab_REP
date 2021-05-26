using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Characters : CharacterStateMachine
{
    public IEnumerator MainCoroutine { get; set;}

    public string characterName;
    public Sprite characterSprite;
    public AttackType attackType;      

    //[SerializeField] private CharacterClasses job;

    public Animator anim;
    public List<ScriptableObject> equipment;
    public List<CharacterSkills> skills;
    
    private string[] elementDebuff = new string[2];

    public int slot;
    private bool isIncapitate;
    private bool isTargeting;
    private bool isBloking;
    private bool isRunning;

    protected int lvl;
    protected int baseHealth;
    protected int maxHealth;
    protected int currentHealth;
    protected int baseMana;
    protected int maxMana;
    protected int currentMana;
    protected int ATK;
    protected int PDEF;
    protected int MDEF;
    protected float speed;

    protected int STR;
    protected int INT;
    protected int FOC;
    protected int VIT;
    protected int AGI;
    protected int LUK;
    protected float healthModifier;
    protected float manaModifier;

    #region Stats
    public int GetSlot() => slot;
    public void SetSlot(int s) {
        slot = s;
    }

    public int GetPDEF() => PDEF;
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHP() => maxHealth;
    public void SetCurrentHealth(int newHealthValue)
    {
        currentHealth = newHealthValue;
    }

    public int GetMaxMP() => maxMana;
    public int GetCurrentMP() => currentMana;
    public float CalculateSpeed() => speed + (AGI / 2);
    public int CalculateLuck() => 0;
    public int CalculateDef() => 0;
    public bool CheckOriginalSpot() => this.transform.position.z == BattleManager.Instance.CharPos[GetSlot() - 1].position.z;
    #endregion

    #region Health
    // Health By LVL + HP Gain
    public int CalculateMaxHealth() => CalculateHealthByLvl() + CalculateHpGain();
    //Base Health + (Base Health * (Health Modifier * LVL))
    private int CalculateHealthByLvl() => baseHealth + Mathf.RoundToInt(baseHealth * (healthModifier * lvl));
    private int CalculateHpGain() => Mathf.RoundToInt((VIT * (0.01f * baseHealth)));
    #endregion

    #region Mana
    public int CalculateMaxMana() => CalculateManaByLvl() + CalculateManaGain();
    //Mana By LVL + Mana Gain
    private int CalculateManaGain() => INT * 2;
    //Base Mana + (Base Mana * Mana Modifier)
    private int CalculateManaByLvl() => baseMana + Mathf.RoundToInt(baseMana * manaModifier);
    #endregion

    public bool IsTargeting() => isTargeting;
    public void SetTargetOn() => isTargeting = true;
    public void SetTargetOff() => isTargeting = false;
    public bool IsIncapitate() => isIncapitate;

    public void RunToTarget(int target)
    {
        anim.SetBool("isRunning", true);
        BattleManager.Instance.isCharacterMove = true;
        //running to target if attack type melee else stay in place
        StartCoroutine(MoveFromTo(this.transform, this.transform.position, BattleManager.Instance.EnemyObject[target].transform.position, 2f));
    }

    public void RunToOriginalSpot(int slot)
    {
        int charSlot = BattleManager.Instance.Player[slot].GetComponent<Player>().GetSlot();
        anim.SetBool("isRunning", true);
        BattleManager.Instance.isCharacterMove = true;
        //running to target if attack type melee else stay in place
        StartCoroutine(MoveFromTo(this.transform, this.transform.position, BattleManager.Instance.CharPos[charSlot].position, 2f));
    }

    IEnumerator MoveFromTo(Transform objToMove, Vector3 a, Vector3 b, float speed)
    {
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step;
            objToMove.position = Vector3.Lerp(a, b, t);
            yield return new WaitForFixedUpdate();
        }
        BattleManager.Instance.isCharacterMove = false;
    }


    public void EndAction()
    {
        anim.SetBool("isAttacking", false);
    }


    public void CancelAction() {
        StopCoroutine(MainCoroutine);
        MainCoroutine = null;
    }

    public void DoNormalAttack() {
        MainCoroutine = State.Attack();
        StartCoroutine(MainCoroutine);
    }

    public void DoSkill(int target, int index)
    {
        switch (skills[index].skillType) {
            case SkillType.Offensive:
                break;
            case SkillType.Support:
                break;
            default:
                break;
        }
    }

    public void DoItem(int player, int target)
    {
        //start Item
    }

    public int CalculateDamage(int targetDef,float damageModifier = 1f, bool isCritical = false)
    {
        int damage = Mathf.FloorToInt(((Mathf.Pow(ATK, 2) / 4) / (targetDef * 1.5f)) * damageModifier);
        if (isCritical)
        {
            damage *= 2;
        }

        return damage;
    }

}