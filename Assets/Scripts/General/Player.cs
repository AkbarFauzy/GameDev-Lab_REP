using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Characters
{
    [SerializeField] private CharacterStats defaultStats;
    public GameObject characterBoxPrefab;
    private CharacterController CControl;

    [Header("Character Equipment")]
    [Space(10)]
    [SerializeField] private Equipment[] characterEquipment = new Equipment[System.Enum.GetNames(typeof(EquipmentType)).Length];


    [SerializeField]private bool isDontDestroyedOnLoad;
    private GameObject Instance;

    // public BarScript bar;

    // Start is called before the first frame update
    private void Awake()
    {
        //Load Player LVL and Stats
        lvl = defaultStats.lvl;
        baseATK = defaultStats.baseATK;
        baseMAG = defaultStats.baseMAG;
        baseDEF = defaultStats.basePDEF;
        baseRES = defaultStats.baseMDEF;
        baseSPD = defaultStats.Speed;

        STR = defaultStats.STR;
        INT = defaultStats.INT;
        FOC = defaultStats.FOC;
        VIT = defaultStats.VIT;
        AGI = defaultStats.AGI;
        LUC = defaultStats.LUK;

        // Init Health
        baseHealth = defaultStats.baseHealth;
        maxHealth = baseHealth + CalculateMaxHealth();
        currentHealth = defaultStats.currentHealth;

        // Init mana
        baseMana = defaultStats.baseMana;
        maxMana = baseMana + CalculateMaxMana();
        currentMana = defaultStats.currentMana;
        if (Instance == null)
        {
            Instance = this.gameObject;
            if (isDontDestroyedOnLoad )
            {
                DontDestroyOnLoad(Instance);
            }
        }
        else {
            Destroy(gameObject);
            Debug.Log(Instance);
        }
    }

    void Start()
    {
        SetCharacterClasses();
        UpdateStats();
        anim = this.GetComponent<Animator>();
        //bar.setMaxValue(stats.currentHealth, maxHP);
        //bar.setValue(stats.currentHealth);
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
    public int DoPDamage(float modifier)
    {
         return BattleManager.Instance.CalculatePDamageOnEnemy(this, Target.GetComponent<Enemy>(), modifier);
    }

    public int DoMdamage(float modifier)
    {
        return BattleManager.Instance.CalculateMDamageOnEnemy(this, Target.GetComponent<Enemy>(), modifier);
    }

    public void RecievedDamaged(int damage)
    {
        currentHealth -= damage;
        BattleManager.Instance.BU.CharPanel[IDX].GetComponent<playerBattlePanel>().Hpbar.SetValue(currentHealth);
    }
}
