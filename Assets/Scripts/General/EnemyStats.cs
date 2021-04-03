using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Stats", menuName ="Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [System.Serializable]
    public struct StatRangeFloat {
        public float min;
        public float max;
    }
    [System.Serializable]
    public struct StatRangeInt
    {
        public int min;
        public int max;
    }
    public StatRangeInt lvlRange;
    public int baseHealth;
    public int baseMana;
    public int Speed;
    public string attackType;
    [Space]
    public int baseATK;
    public int basePDEF;
    public int baseMDEF;
    [Space]
    public StatRangeInt STR;
    public StatRangeInt INT;
    public StatRangeInt FOC;
    public StatRangeInt VIT;
    public StatRangeInt AGI;
    public StatRangeInt LUK;
    [Space]
    public StatRangeFloat healthModifier;
    public StatRangeFloat manaModifier;
    public StatRangeInt expDrop;


    public int GetLvl()
    {
        return Random.Range(lvlRange.min, lvlRange.max);
    }

    public int GetSTR()
    {
        return Random.Range(STR.min, STR.max);
    }

    public int GetINT() {
        return Random.Range(INT.min, INT.max);
    }

    public int GetFOC()
    {
        return Random.Range(FOC.min, FOC.max);
    }

    public int GetVIT()
    {
        return Random.Range(VIT.min, VIT.max);
    }

    public int GetAGI()
    {
        return Random.Range(AGI.min, AGI.max);
    }

    public int GetLUK()
    {
        return Random.Range(LUK.min, LUK.max);
    }

    public float GetHealthModifier() {
        return Random.Range(healthModifier.min, healthModifier.max);
    }

    public float GetManaModifier()
    {
        return Random.Range(manaModifier.min, manaModifier.max);
    }

    public int GetExpDrop()
    {
        return Random.Range(expDrop.min, expDrop.max);
    }

}
