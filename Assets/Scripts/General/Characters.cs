using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public string characterName;
    public Sprite characterSprite;

    public string attackType;

    public Animator anim;
    public List<ScriptableObject> equipment;
    public List<CharacterSkills> skills;
    private string[] elementDebuff = new string[2];

    public int slot;

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

    public void RunToTarget(int target)
    {
        anim.SetBool("isRunning", true);
        Debug.LogWarning(BattleManager.Instance.isCharacterMove);
        BattleManager.Instance.isCharacterMove = true;
        //running to target if attack type melee else stay in place
        StartCoroutine(MoveFromTo(this.transform, this.transform.position, BattleManager.Instance.Enemy[target].transform.position, 2f));
    }

    public void RunToOriginalSpot(int slot)
    {
        int charSlot = BattleManager.Instance.Player[slot].GetComponent<Player>().GetSlot();
        anim.SetBool("isRunning", true);
        Debug.LogWarning(BattleManager.Instance.isCharacterMove);
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
    public void DoMelee(int player, int Target)
    {
        //start Melee Attack animation 
    }

    public void DoRanged(int player, int target)
    {
        //start Ranged animation
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

    public bool CheckOriginalSpot()
    {
        return this.transform.position.z == BattleManager.Instance.CharPos[GetSlot() - 1].position.z;
    }

    public int GetSlot()
    {
        return slot;
    }


    public int CalculateMaxHealth()
    {
        // Health By LVL + HP Gain
        return CalculateHealthByLvl() + CalculateHpGain();
    }

    private int CalculateHealthByLvl()
    {
        //Base Health + (Base Health * (Health Modifier * LVL))
        return baseHealth + Mathf.RoundToInt(baseHealth * (healthModifier * lvl));
    }

    private int CalculateHpGain()
    {
        //VIT * (1% * Base Health)
        return Mathf.RoundToInt((VIT * (0.01f * baseHealth)));
    }

    public int CalculateMaxMana()
    {
        //Mana By LVL + Mana Gain
        return CalculateManaByLvl() + CalculateManaGain();
    }

    private int CalculateManaByLvl()
    {
       //Base Mana + (Base Mana * Mana Modifier)
        return baseMana + Mathf.RoundToInt(baseMana * manaModifier);
    }

    private int CalculateManaGain()
    {
        return INT * 2;
    }

    public int CalculateDef()
    {
        return 0;
    }

    public float CalculateSpeed()
    {
        return speed + (AGI / 2);
    }

    public int CalculateLuck()
    {
        return 0;
    }

    public int GetPDEF()
    {
        return PDEF;
    }

    public int GetCurrentHealth() {
        return currentHealth;
    }

    public void SetCurrentHealth(int newHealthValue) {
        currentHealth = newHealthValue;
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
