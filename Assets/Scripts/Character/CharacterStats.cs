using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats", menuName = "Character Stats")]
public class CharacterStats : ScriptableObject
{
    public int lvl;
    public string attackType;
    [Space]
    public int baseATK;
    public int basePDEF;
    public int baseMDEF;
    public int baseHealth;
    public int baseMana;
    public int Speed;
    [Space]
    public int MAXHealth;
    public int currentHealth;
    [Space]
    public int MAXMana;
    public int currentMana;
    [Space]
    public int STR;
    public int INT;
    public int FOC;
    public int VIT;
    public int AGI;
    public int LUK;
    [Space]
    public float healthModifier;
    public float manaModifier;
    public float statsModifier;
}
