using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Abilities
{
    [SerializeField]public bool isUnlocked;
    [SerializeField]public CharacterSkills skills;

   public Abilities(bool unlocked, CharacterSkills skill) {
        this.isUnlocked = unlocked;
        this.skills = skill;
    } 
}
