using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Characters
{
    [SerializeField] private CharacterStats stats;
    public CharacterController CControl;
    // public BarScript bar;

    // Start is called before the first frame update
    private void Awake()
    {
        //Load Player LVL and Stats
        lvl = stats.lvl;
        ATK = stats.baseATK;
        PDEF = stats.basePDEF;
        MDEF = stats.baseMDEF;

        STR = stats.STR;
        INT = stats.INT;
        FOC = stats.FOC;
        VIT = stats.VIT;
        AGI = stats.AGI;
        LUK = stats.LUK;

        // Init Health
        baseHealth = stats.baseHealth;
        maxHealth = baseHealth + CalculateMaxHealth();
        currentHealth = stats.currentHealth;

        // Init mana
        baseMana = stats.baseMana;
        maxMana = baseMana + CalculateMaxMana();
        currentMana = stats.currentMana;
         
    
    }

    void Start()
    {
        anim = this.GetComponent<Animator>();
        //bar.setMaxValue(stats.currentHealth, maxHP);
        //bar.setValue(stats.currentHealth);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int NextLevel(int currentLvl) {
        int baseExp = 8;
        int Exp = baseExp;
        for (int i = 1; i<currentLvl;i++)
        {
            Exp += Exp * baseExp;
        }
        
        return Exp;
    }


    public void GenerateAP()
    {

    }

    public int GetMaxHP() {
        return maxHealth;
    }

    public int GetMaxMP()
    {
        return maxMana;
    }
    public int GetCurrentMP()
    {
        return currentMana;
    }

    public CharacterStats GetStats()
    {
        return stats;
    }

}
