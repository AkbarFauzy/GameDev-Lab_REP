using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Character Skills", menuName ="Character Skill")]
public class CharacterSkills : ScriptableObject
{
    public enum SkillName { 
        Fireball,
        Waterball,
        WindBlade,
        StoneShot,
        Cure,
        Poison,
    }

    public SkillName skillName;
    public string description;
    public SkillType skillType;
    public ElementType elementType;
    public float value;

    public float FireElemental(ElementType otherElement) {
        switch (otherElement)
        {
            case ElementType.Water:
                return 0.5f;
            case ElementType.Wind:
                return 1.5f;
            default:
                return 1f;
        }
    }

    public float WaterElemental(ElementType otherElement)
    {
        switch (otherElement)
        {
            case ElementType.Earth:
                return 0.5f;
            case ElementType.Fire:
                return 1.5f;
            default:
                return 1f;
        }
    }

    public float EarthElemental(ElementType otherElement) {
        switch (otherElement)
        {
            case ElementType.Wind:
                return 0.5f;
            case ElementType.Water:
                return 1.5f;
            default:
                return 1f;
        }
    }

    public float WindElemental(ElementType otherElement) {
        switch (otherElement)
        {
            case ElementType.Fire:
                return 0.5f;
            case ElementType.Earth:
                return 1.5f;
            default:
                return 1f;
        }
    }



}
