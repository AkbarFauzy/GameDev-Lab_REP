using System.Collections;
using System.Collections.Generic;
using UnityEngine;
                                             
public class Characters : CharacterStateMachine
{
    private IEnumerator MainCoroutine { get; set;}

    public string characterName;
    public Classes characterClasses;
    protected CharacterClasses classes;
    public string characterTraitsDesc;
    public Sprite characterSprite;
    public AttackType attackType;      

    //[SerializeField] private CharacterClasses job;

    public Animator anim;
    public List<Equipment> equipment;
    [SerializeField]public List<Abilities> abilities = new List<Abilities>();
    
    private string[] elementDebuff = new string[2];

    public int slot;
    private GameObject target;

    private bool isIncapitate;
    private bool isTargeting;
    private bool isBloking;
    private bool isRunning;
    private bool isAction;

    protected int lvl;
    protected int baseHealth;
    protected int maxHealth;
    protected int currentHealth;
    protected int baseMana;
    protected int maxMana;
    protected int currentMana;

    protected int baseATK;
    protected int baseMAG;
    protected int baseDEF;
    protected int baseRES;
    protected int baseSPD;

    protected int calculatedATK;
    protected int calculatedMAG;
    protected int calculatedDEF;
    protected int calculatedRES;
    protected int calculatedSPD;

    protected int str;
    protected int inte;
    protected int foc;
    protected int vit;
    protected int agi;
    protected int luc;
    protected float healthModifier;
    protected float manaModifier;
    protected float damageModifier;

    private int idx;

    public int IDX {
        get => idx; set => idx = value;
    }
    public bool IsTargeting{
        get => isTargeting; set => isTargeting = value;
    }
    public bool IsRunning {
        get => isRunning; set => isRunning = value;
    }
    public bool IsAction {
        get => isAction; set => isAction = value;
    }

    #region Stats
    public GameObject Target{
        get => target; set => target = value;
    }

    public int Slot {
        get => slot; set => slot = value;
    }

    public int LVL {
        get => lvl; set => lvl = value;
    }

    public int ATK {
        get => calculatedATK; set => calculatedATK = value;
    }
    public int MAG {
        get => calculatedMAG; set => calculatedMAG = value;
    }
    public int DEF {
        get => calculatedDEF; set => calculatedDEF = value;
    }
    public int RES {
        get => calculatedRES; set => calculatedRES = value;
    }
    public int SPD{
        get => calculatedSPD; set => calculatedSPD = value;
    }
    public int STR {
        get => str; set => str = value;
    }
    public int INT {
        get => inte; set => inte = value;
    }
    public int FOC {
        get => foc; set => foc = value;
    }
    public int VIT {
        get => vit; set => vit = value;
    }
    public int AGI {
        get => agi; set => agi = value;
    }
    public int LUC {
        get => luc; set => luc = value;
    }

    public int CurrentHp {
        get => currentHealth; set => currentHealth = value;
    }

    public int MaxHp{
        get => maxHealth;
    }

    public int MaxMp {
        get => maxMana;
    }
    public int CurrentMp {
        get => currentMana; set => currentMana = value;
    }

    public float DamageModifier
    {
        get => damageModifier; set => damageModifier = value;
    }

    private int CalculateFinalATK() => baseATK + (lvl * Mathf.CeilToInt((float)baseATK * 0.05f)) + (STR * Mathf.CeilToInt(0.01f * (float)baseATK));
    private int CalculateFinalMAG() => baseMAG + (lvl * Mathf.CeilToInt((float)baseMAG * 0.05f)) + (INT * Mathf.CeilToInt(0.02f * (float)baseMAG));
    private int CalculateFinalDEF() => baseDEF + (lvl * Mathf.CeilToInt((float)baseDEF * 0.1f)) + (VIT * Mathf.CeilToInt(0.01f * (float)baseDEF));
    private int CalculateFinalRES() => baseRES + (lvl * Mathf.CeilToInt((float)baseRES * 0.05f)) + (INT * Mathf.CeilToInt(0.02f * (float)baseRES));
    private int CalculateFinalSPD() => baseSPD + (lvl * Mathf.CeilToInt((float)baseSPD * 0.01f)) + (INT * Mathf.CeilToInt(0.01f * (float)baseRES));

    public void UpdateStats() {
        ATK = CalculateFinalATK();
        MAG = CalculateFinalMAG();
        DEF = CalculateFinalDEF();
        RES = CalculateFinalRES();
        SPD = CalculateFinalSPD();
    }

    public bool CheckOriginalSpot() => this.transform.position.z == BattleManager.Instance.CharPos[Slot - 1].position.z;
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

    public void SetTargetOn() => isTargeting = true;
    public void SetTargetOff() => isTargeting = false;
    public bool IsIncapitate() => isIncapitate;

    public void SetCharacterClasses()
    {
        switch (characterClasses) {
            case (Classes.Tanker):
                classes = new TankerClass();
                classes.Traits();
                break;
            case (Classes.Warrior):
                classes = new WarriorClass();
                break;
        }
    }                                                        
    public void RunToTarget(Vector3 target)
    {
        IsRunning = true;
        anim.SetBool("isRunning", IsRunning);
        StartCoroutine(MoveFromTo(this.transform, this.transform.position, target - new Vector3(0f,0f,1f), 10f));        
    }

    public void RunToOriginalSpot(int slot)
    {
        isRunning = true;
        anim.SetBool("isRunning", isRunning);
        StartCoroutine(MoveFromTo(this.transform, this.transform.position, BattleManager.Instance.CharPos[slot].position, 10f));
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
        IsRunning = false;
        anim.SetBool("isRunning", IsRunning);
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
}