using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Character Skills", menuName ="Character Skill")]
public class CharacterSkills : ScriptableObject
{
    public string skillName;
    public string description;
    public int cost;
    public SkillType skillType;
    public ElementType elementType;
    public float value;
    public GameObject animationPrefab;
    public AudioSource SFX;

}
