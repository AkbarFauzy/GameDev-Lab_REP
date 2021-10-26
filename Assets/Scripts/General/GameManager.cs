using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum SkillType {Offensive, Support}
public enum Classes {Tanker, Warrior, Mage,Ranger, Thief, Priest, Monster, Undead, Oni}
public enum ElementType {None, Fire, Water, Wind, Earth, Light, Dark}
public enum EnemyType {Undead, Beast, Humanoid, Lowlife, Dragon, ElderDragon}
public enum ActionType {Attack, Skill, Item, Guard}
public enum AttackType { melee, ranged }; 
public enum EquipmentType {WEAPON, HEADGER, ARMOR, BOOTS, ACCESSORY }
public enum ItemType {CONSUMABLE, CRAFTING, VALUABLE }


public class GameManager : GameStateMachine
{
    public static GameManager Instance { get; private set; }
    public LevelLoader LevelLoader;

    public GameObject player;

    const int MAXLVL = 99;
    public static int MAX_PARTY_MEMBER = 4;

    private Vector3 lastPosition;
    private Test _control;

    //public List<GameObject> characters;
    public GameObject[] activeCharacter = new GameObject[MAX_PARTY_MEMBER]; //character PREFAB
    public EnemyGroup EncounteredEnemy;

    public int GameSpeed;
    public bool isBattle;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(Instance);
        } else {
            Destroy(gameObject);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _control = new Test();
        _control.UI.Pause.performed += ctx => StartCoroutine(State.UIPause());
        _control.Enable();
    
    }

    public void DisableControl() {
        _control.Disable();
    }

    public void OpenPauseMenu() {
        //SetState to OpenPauseMenu
        _control.Disable();
        _control = new Test();
        _control.UI.Submit.performed += ctx => StartCoroutine(State.UISubmit());
        _control.UI.Cancel.performed += ctx => StartCoroutine(State.UICancel());
        _control.UI.Pause.performed += ctx => StartCoroutine(State.UIPause());
        _control.UI.Up.performed += ctx => StartCoroutine(State.UIUp());
        _control.UI.Down.performed += ctx => StartCoroutine(State.UIDown());
        _control.UI.Left.performed += ctx => StartCoroutine(State.UILeft());
        _control.UI.Right.performed += ctx => StartCoroutine(State.UIRight());
        _control.Enable();
        PauseMenu.Instance.gameObject.SetActive(true);
    }

    private void Start()
    {
        SetState(new FreeroamState(this, PauseMenu.Instance));
    }


    //if LVL UP change Health, Mana
    void LevelUp(CharacterStats stats) { 
        
    
    }

    //CALCULATE MAX HP
    
}
