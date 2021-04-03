using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Abilities", menuName = "Abilities")]
public class Abilities : ScriptableObject
{
    public string abilitiesName;
    public string Category;
    public string SubCategory;

    public int Element;
    //public int ApCost;
    
    public int MpCost;
    public int DamageModifier;
    public int AttackType;
    
}
