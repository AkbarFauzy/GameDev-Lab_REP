using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats", menuName = "Character Stats")]
public class CharacterStats : ScriptableObject
{
    public int lvl;
    public string attackType;
    [Space(10)]
    public int baseATK;
    public int baseMAG;
    public int basePDEF;
    public int baseMDEF;
    public int baseHealth;
    public int baseMana;
    public int Speed;
    [Space(10)]
    public int MAXHealth;
    public int currentHealth;
    [Space(10)]
    public int MAXMana;
    public int currentMana;
    [Space(10)]
    public int STR;
    public int INT;
    public int FOC;
    public int VIT;
    public int AGI;
    public int LUK;
    [Space(10)]
    public float healthModifier;
    public float manaModifier;
    public float statsModifier;
}
