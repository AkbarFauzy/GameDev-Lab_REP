using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{
    [SerializeField] private EnemyStats stats;
    private List<float> aggro;

    void Start() {
        lvl = stats.GetLvl();

        ATK = stats.baseATK;
        PDEF = stats.basePDEF;
        MDEF = stats.baseMDEF;
        
        STR = stats.GetSTR();
        INT = stats.GetINT();
        FOC = stats.GetFOC();
        VIT = stats.GetVIT();
        AGI = stats.GetAGI();
        LUK = stats.GetLUK();

        maxHealth = CalculateMaxHealth();
        maxMana = CalculateMaxMana() ;
        currentHealth = maxHealth;
        currentMana = maxMana;

        aggro = new List<float>();
        if (GameManager.Instance.isBattle) {
            foreach (GameObject players in GameManager.Instance.activeCharacter)
            {
                aggro.Add(InitAggro());
            }
        }
    }

    void Update() { 
        

    }

    float InitAggro()
    {
        return 0f;
    }

    int CalculateTargetAggro()
    {
        if (aggro.Count == 1)
        {
            return 0;
        }
        int max = 0;
        for (int i = 1; i<aggro.Count;i++) 
        {
            if (aggro[max] < aggro[i])
            {
                max = i;
            } else if (aggro[max] == aggro[i])
            {
                max = CalculateDominantTarget(max,i, "melee");
            }
            i++;
        }
        return max;
    }

    int CalculateDominantTarget(int target1, int target2, string priority)
    {
        Player t1 = BattleManager.Instance.Player[target1].GetComponent<Player>();
        Player t2 = BattleManager.Instance.Player[target2].GetComponent<Player>();
        if ((t1.GetSlot() < 3 && t2.GetSlot() < 3) || (t1.GetSlot() > 2 && t2.GetSlot() > 2))
        {
            int ran = Random.Range(1, 3);
            if (ran == 2) {
                return target2;
            }
            return target1;
        }

        if (priority == "melee")
        {
            return target1;
        }
        return target2;
    }

    public EnemyStats GetStats() {
        return stats;
    }

}
