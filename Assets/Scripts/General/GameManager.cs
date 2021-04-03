using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ActionType {Attack, Skill, Guard, Item}
public enum SkillType {Offensive, Support}
public enum ElementType {None, Fire, Water, Wind, Earth, Light, Dark}
public enum EnemyType {Undead, Beast, Humanoid, Lowlife, Dragon, ElderDragon}

public class GameManager : MonoBehaviour
{

    public GameObject player;

    const int MAXLVL = 99;

    public static GameManager Instance { get; private set; }

    //public List<GameObject> characters;
    public GameObject[] activeCharacter = new GameObject[4]; //character PREFAB
   
    public int GameSpeed;
    public bool isBattle;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }else {
            Destroy(gameObject);
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        //SceneManager.LoadScene("battle", new LoadSceneParameters(LoadSceneMode.Additive));
        //battleStart();
      
    }

    //if LVL UP change Health, Mana
    void LevelUp(CharacterStats stats) { 
        
    
    }

    //CALCULATE MAX HP
    
}
