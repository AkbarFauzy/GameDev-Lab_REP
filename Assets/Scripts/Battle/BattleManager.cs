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
    public Test GetControl() => _control;

    //UI Reference
    [Header("========================= UI Reference =========================")]
    public BattleUi BU;
    public BattleQueue Queue;
    public List<Transform> CharPos;
    public List<Transform> EnemyPos;

    //Characters
    [Header("=========================  Characters ========================= ")]
    [Space(10)]
    [SerializeField]  
    public List<GameObject> EnemyObject;

    public List<Player> Player;
    
    [HideInInspector]private List<GameObject> characterPanel;

    public GameObject commandPanel;

    [Header("========================= Cameras =========================")]
    [Space(10)]
    public List<CinemachineVirtualCamera> Cams;
    public CinemachineBrain MainCam;

    [Header("========================= Variabel =========================")]
    [Space(10)]

    private int[] playerSlots = new int[6];
    private int[] enemySlots = new int[6];

    public int playerIndex;
    public int enemyIndex;
    public bool isCharacterMove;
    public bool isAction;

    public int[] playerTarget = new int[4];
    public int[] enemyTarget = new int[5];
    //private BattleState _battleState;
    //private int enemyATB;
    //private int playerATB;

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
        BattleStart();
        _control = new Test();
        _control.Battle.Right.performed += ctx => StartCoroutine(State.RightTopButton());
        _control.Battle.Left.performed += ctx => StartCoroutine(State.LeftTopButton());
        _control.Battle.Cancel.performed += ctx => StartCoroutine(State.CancelButton());
        _control.Battle.Submit.performed += ctx => StartCoroutine(State.SubmitButton());
        _control.Enable();
    }

    void Start()
    {
        playerIndex = 0;
        enemyIndex = 0;
        //StartCoroutine(startCinematicCam());
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
                enemyObject.GetComponent<Enemy>().SetSlot(ran);

                EnemyObject.Add(enemyObject);
            }
        }

        for (int i = 0; i < gm.activeCharacter.Length; i++)
        {

            //-------------------------------------- testing purpose
            int charSlot = GameManager.Instance.activeCharacter[i].GetComponent<Player>().GetSlot();
            GameObject charObject =
              Instantiate(GameManager.Instance.activeCharacter[i],
              CharPos[charSlot].position,
              Quaternion.identity);
            playerSlots[charSlot] = 1;
            //--------------------------------------

            Player.Add(charObject.GetComponent<Player>());
    
            int FullHp = Player[i].GetMaxHP();
            int FullMp = Player[i].GetMaxMP();

            playerBattlePanel panel = BU.CharPanel[i].gameObject.GetComponent<playerBattlePanel>();
            panel.charName.SetText(charObject.GetComponent<Player>().characterName);
            panel.Mp.SetText(Player[i].GetCurrentMP().ToString() + "/" + FullMp.ToString());
            panel.MpBar.SetMaxValue(Player[i].GetCurrentMP(), FullMp);
            panel.MpBar.SetValue(Player[i].GetCurrentMP());

            panel.Hp.SetText(Player[i].GetCurrentHealth().ToString() + "/" + FullHp.ToString());
            panel.Hpbar.SetMaxValue(Player[i].GetCurrentHealth(), FullHp);
            panel.Hpbar.SetValue(Player[i].GetCurrentHealth());

            panel.DriveGauge.SetMaxValue(0, 100);
            panel.DriveGauge.SetValue(0);

            panel.charSprite.sprite = charObject.GetComponent<Player>().characterSprite;
        }

        commandPanel.SetActive(false);
        //StartCoroutine(CheckQueue());
        Queue.Init();

        SetState(new BeginBattle(this));

    }

    public void BattleEnd() {
        _control.Disable();
    }

    public void StateEnemySelect() {
        commandPanel.SetActive(false);
        SetState(new EnemySelect(this));
        ChangeCamPriority(6);
        MainCam.ActiveVirtualCamera.LookAt = EnemyObject[GetEnemySlot(ref enemyIndex)].transform;
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

    public void OnItemButton() { 
    
    }

    public void OnSkillButton() { 
    
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

    public int GetCharSlot(ref int id) {
        //return GameManager.Instance.activeCharacter[id].GetComponent<Player>().GetSlot();
        return Player[id].GetSlot();
    }

    public int GetEnemySlot(ref int id)
    {
        return EnemyObject[id].GetComponent<Enemy>().GetSlot();
    }

    public bool IsCommandPanelOpen()
    {
        return commandPanel.activeSelf;
    }

    public bool CharacterIsMoving()
    {
        return isCharacterMove = true;
    }

    public bool IsCritical(int f1, int f2) {
        return false;
    }
    
    IEnumerator CheckQueue() {
        while (GameManager.Instance.isBattle) {
            yield return new WaitUntil(() => Queue.battleQueue.Count != 0);
            StartCoroutine(QueueAction());
            isAction = true;
            yield return new WaitUntil(() => !isAction);
        }
    }

    IEnumerator QueueAction() {
        yield return new WaitUntil(() => isAction);
        int playerIndex = Queue.battleQueue.Peek().character;
        int enemyIndex = Queue.battleQueue.Peek().enemy;
        if (Queue.battleQueue.Peek().isPlayer)
        {
            DoActionPlayer(ref playerIndex, ref enemyIndex);
        }else
        { 
            DoActionEnemy(ref enemyIndex, ref playerIndex);
        }
        Player[playerIndex].GetComponent<Player>().RunToTarget(enemyIndex);
        yield return new WaitUntil(() => !isCharacterMove);

        //Player[playerIndex].GetComponent<Player>().doAction(enemyIndex);
        isCharacterMove = false;    
        Queue.deleteActionQueue();
        Player[playerIndex].GetComponent<Player>().RunToOriginalSpot(playerIndex);
        yield return new WaitUntil(() => Player[playerIndex].GetComponent<Player>().CheckOriginalSpot());

        isAction = false;
    }

    public void DoActionPlayer(ref int playerIndex, ref int enemyIndex)
    {
        switch (Queue.battleQueue.Peek().actionType)
        {
            case ActionType.Attack:
                int damage = Player[playerIndex].CalculateDamage(EnemyObject[enemyIndex].GetComponent<Enemy>().GetPDEF());
                EnemyObject[enemyIndex].GetComponent<Enemy>().SetCurrentHealth(EnemyObject[enemyIndex].GetComponent<Enemy>().GetCurrentHealth() - damage);
                break;
            case ActionType.Skill:
                break;
            case ActionType.Guard:
                break;
            case ActionType.Item:
                break;
            default:
                break;
        }
    }

    public void DoActionEnemy(ref int enemyIndex, ref int playerIndex)
    {
        switch (Queue.battleQueue.Peek().actionType)
        {
            case ActionType.Attack:
                break;
            case ActionType.Skill:
                break;
            case ActionType.Guard:
                break;
            case ActionType.Item:
                break;
            default:
                break;
        }
    }

}
