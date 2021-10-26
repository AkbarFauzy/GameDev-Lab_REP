using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BattleManager : BattleStateMachine
{
    public static BattleManager Instance { get; private set; }

    public Test _control;
  
    //UI Reference
    [Header("========================= UI Reference =========================")]
    public BattleUi BU;
    public BattleQueue Queue;
    public List<Transform> CharPos;
    public List<Transform> EnemyPos;

    //Characters
    [Header("=========================  Characters ========================= ")]
    [Space(10)]
    public List<GameObject> EnemyObject;
    public List<Player> Player;

    [Header("========================= Cameras =========================")]
    [Space(10)]
    public List<CinemachineVirtualCamera> Cams;
    public CinemachineBrain MainCam;

    [Header("========================= Variabel =========================")]
    [Space(10)]

    private int[] playerSlots = new int[6];
    private int[] enemySlots = new int[6];

    private int playerIndex;
    private int enemyIndex;
    public bool isCharacterMove;
    private bool isAction;

    public int[] playerTarget = new int[4];
    public int[] enemyTarget = new int[5];
    //private int enemyATB;
    //private int playerATB;
    public int EnemyIndex {
        get => enemyIndex; set => enemyIndex = value;
    }
    public int PlayerIndex {
        get => playerIndex; set => playerIndex = value;
    }

    public bool IsAction {
        get => isAction; set => isAction = value;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
 
        GameManager.Instance.DisableControl();
        _control = new Test();
        _control.Battle.Right.performed += ctx => StartCoroutine(State.RightTopButton());
        _control.Battle.Left.performed += ctx => StartCoroutine(State.LeftTopButton());
        _control.Battle.Cancel.performed += ctx => StartCoroutine(State.CancelButton());
        _control.Battle.Submit.performed += ctx => StartCoroutine(State.SubmitButton());
        _control.Enable();
    }

    void Start()
    {
        BattleStart();
        playerIndex = 0;
        enemyIndex = 0;
    }

    #region Battle States
    public void BattleStart()
    {
        GameManager.Instance.isBattle = true;
       
        var gm = GameManager.Instance;

        for(int i =0;i < GameManager.Instance.EncounteredEnemy.prefab.Count; i++) {
            for (int j = 0; j < GameManager.Instance.EncounteredEnemy.number[i]; j++) {
                int ran = Random.Range(0, 6);
                while (enemySlots[ran] == 1)
                {
                    ran = Random.Range(0, 6);
                }
                enemySlots[ran] = 1;

                GameObject enemyObject = Instantiate(GameManager.Instance.EncounteredEnemy.prefab[i],
                    EnemyPos[ran].position,
                    new Quaternion(0f, 180f, 0f, 0));
                enemyObject.GetComponent<Enemy>().Slot = ran;

                EnemyObject.Add(enemyObject);
            }
        }

        for (int i = 0; i < gm.activeCharacter.Length; i++)
        {

            //-------------------------------------- testing purpose
            int charSlot = GameManager.Instance.activeCharacter[i].GetComponent<Player>().Slot;
            GameObject charObject =
              Instantiate(GameManager.Instance.activeCharacter[i],
              CharPos[charSlot].position,
              Quaternion.identity);
            playerSlots[charSlot] = 1;
            //--------------------------------------

            charObject.GetComponent<Player>().IDX = i;
            Player.Add(charObject.GetComponent<Player>());
    
            int FullHp = Player[i].MaxHp;
            int FullMp = Player[i].MaxMp;

            playerBattlePanel panel = BU.CharPanel[i].gameObject.GetComponent<playerBattlePanel>();
            panel.charName.SetText(charObject.GetComponent<Player>().characterName);
            panel.Mp.SetText(Player[i].CurrentMp.ToString() + "/" + FullMp.ToString());
            panel.MpBar.SetMaxValue(Player[i].CurrentMp, FullMp);
            panel.MpBar.SetValue(Player[i].CurrentMp);

            panel.Hp.SetText(Player[i].CurrentHp.ToString() + "/" + FullHp.ToString());
            panel.Hpbar.SetMaxValue(Player[i].CurrentHp, FullHp);
            panel.Hpbar.SetValue(Player[i].CurrentHp);

            panel.DriveGauge.SetMaxValue(0, 100);
            panel.DriveGauge.SetValue(0);

            panel.charSprite.sprite = charObject.GetComponent<Player>().characterSprite;
           
        }

        BU.commandPanel.SetActive(false);
        //StartCoroutine(CheckQueue());
        Queue.Init();

        SetState(new BeginBattle(this));

    }

    public void BattleEnd() {
        _control.Disable();
    }

    public void StateEnemySelect() {
        BU.commandPanel.SetActive(false);
        SetState(new EnemySelect(this));
        ChangeCamPriority(6);
        MainCam.ActiveVirtualCamera.LookAt = EnemyObject[GetEnemySlot(enemyIndex)].transform;
    }
    #endregion
    IEnumerator StartCinematicCam()
    {                                    
        while (true)
        {
            if (!CinemachineCore.Instance.IsLive(Cams[0]) && !CinemachineCore.Instance.IsLive(Cams[2]))
            {
                Cams[0].Priority = 2;
                Cams[1].Priority = 1;
            }
            else if (!CinemachineCore.Instance.IsLive(Cams[1]) && !CinemachineCore.Instance.IsLive(Cams[2]))
            {
                Cams[0].Priority = 1;
                Cams[1].Priority = 2;
            }
            yield return null;
        }
    }

    public void ChangeCamPriority(int nextCam, bool follow = false)
    {
        MainCam.ActiveVirtualCamera.Priority = 0;
        Cams[nextCam].Priority = 1;
        if (follow)
        {
            Cams[nextCam].LookAt = Player[playerIndex].transform;
            Cams[nextCam].Follow = Player[playerIndex].transform;
        }

    }

    public void OnNormalAttackTrigger()
    {
        SetState(new EnemySelect(this));
    }

    /*public void UpDownUI(Vector2 ctx)
    {
        if (!isAnalogMoving)
        {
            if (ctx.y == 1f)
            {
                UpUi();
            }
            else if (ctx.y == -1f)
            {
                DownUi();
            }
            StartCoroutine(AnalogCD());
        }

    }*/

    public int GetCharSlot(int id) {
        //return GameManager.Instance.activeCharacter[id].GetComponent<Player>().GetSlot();
        return Player[id].Slot;
    }

    public int GetEnemySlot(int id)
    {
        return EnemyObject[id].GetComponent<Enemy>().Slot;
    }

    public bool IsCommandPanelOpen()
    {
        return BU.commandPanel.activeSelf;
    }

    public bool CharacterIsMoving()
    {
        return isCharacterMove = true;
    }

    /*    public bool IsCritical(int f1, int f2) {
            return false;
        }*/

    IEnumerator CheckQueue()
    {
        while (GameManager.Instance.isBattle)
        {
            yield return new WaitUntil(() => Queue.battleQueue.Count != 0);
            StartCoroutine(QueueAction());
            isAction = true;
            yield return new WaitUntil(() => !isAction);
        }
    }

    IEnumerator QueueAction()
    {
        yield return new WaitUntil(() => isAction);
        int playerIndex = Queue.battleQueue.Peek().character;
        int enemyIndex = Queue.battleQueue.Peek().enemy;

        Player[playerIndex].GetComponent<Player>().RunToTarget(EnemyObject[enemyIndex].transform.position);
        yield return new WaitUntil(() => !isCharacterMove);

        //Player[playerIndex].GetComponent<Player>().doAction(enemyIndex);
        isCharacterMove = false;
        Queue.deleteActionQueue();
        Player[playerIndex].GetComponent<Player>().RunToOriginalSpot(playerIndex);
        yield return new WaitUntil(() => Player[playerIndex].GetComponent<Player>().CheckOriginalSpot());

        isAction = false;
    }

    public int CalculatePDamageOnEnemy(Player player, Enemy target, float damageModifier) {
        bool isCritical = false;
        int damage = Mathf.FloorToInt(((Mathf.Pow(player.ATK, 2) / 4) / ((target.DEF * 1.5f)+1)) * damageModifier);
        if (isCritical)
        {
            damage *= 2;
        }
        return damage;
    }

    public int CalculateMDamageOnEnemy(Player player, Enemy target, float damageModifier)
    {
        bool isCritical = false;
        int damage = Mathf.FloorToInt(((Mathf.Pow(player.ATK, 2) / 4) / (target.RES * 1.5f)) * damageModifier);
        if (isCritical)
        {
            damage *= 2;
        }
        return damage;
    }

    public int CalculatePDamageOnPlayer(Enemy enemy, Player target, float damageModifier) {
        bool isCritical = false;
        int damage = Mathf.FloorToInt(((Mathf.Pow(enemy.ATK, 2) / 4) / (target.DEF * 1.5f)) * damageModifier);
        if (isCritical)
        {
            damage *= 2;
        }
        return damage;
    }

    public void CalculateMDamageOnPlayer(Enemy enemy, Player target, float damageModifier)
    {
        bool isCritical = false;
        int damage = Mathf.FloorToInt(((Mathf.Pow(target.ATK, 2) / 4) / (target.RES * 1.5f)) * damageModifier);
        if (isCritical)
        {
            damage *= 2;
        }
    }


    public float FireElemental(ElementType otherElement)
    {
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

    public float EarthElemental(ElementType otherElement)
    {
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

    public float WindElemental(ElementType otherElement)
    {
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

