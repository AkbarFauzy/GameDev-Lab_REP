using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    private Test _control;
    //UI Reference
    public BattleUi BU;
    public List<Transform> CharPos;
    public BattleQueue Queue;

    public List<Transform> EnemyPos;
    public List<Enemy> Enemy;
    public List<Player> Player;
    public List<GameObject> characterPanel;

    public GameObject commandPanel;

    public List<CinemachineVirtualCamera> Cams;
    public CinemachineBrain MainCam;
    
    public int activeIndex;
    public bool isCharacterMove;
    public bool isAction;
    public int playerActionIndex;
    public int battleState;

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

        _control = new Test();

        _control.Battle.Right.performed += ctx => ChangeCharRight();
        _control.Battle.Left.performed += ctx => ChangeCharLeft();
        _control.Battle.Up.performed += ctx => UpUi();
        _control.Battle.Down.performed += ctx => DownUi();
       // _control.Battle.UpDown.performed += ctx => UpDownUI(ctx.ReadValue<Vector2>());
        _control.Battle.Submit.performed += ctx => AttackAction();
        

    }

    private void OnEnable()
    {
        _control.Enable();
 
    }

    void Start()
    {
        //StartCoroutine(startCinematicCam());
        commandPanel.SetActive(false);
        BattleStart();
        StartCoroutine(CheckQueue());
    }


    public void BattleStart()
    {
        GameManager.Instance.isBattle = true;
        var gm = GameManager.Instance;
        BU.CharPanel[0].Select();
        for (int i = 0; i < gm.activeCharacter.Length; i++)
        {

            //-------------------------------------- testing purpose
            GameObject charObject =
              Instantiate(GameManager.Instance.activeCharacter[i],
              CharPos[GameManager.Instance.activeCharacter[i].GetComponent<Player>().GetSlot()].position,
              Quaternion.identity);
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

    }


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

    private void Update()
    {
        if (battleState < 0)
        {
            battleState = 0;
        }

        if (isCharacterMove)
        {

        }
    }

    public void ChangeCharRight()
    {
        int max = GameManager.Instance.activeCharacter.Length;
        if (IsCommandPanelOpen())
        {
            max = Enemy.Count;
        }

        activeIndex += 1;
        if (activeIndex >= max)
        {
            activeIndex = 0;
        }
        ChangeChar(activeIndex);
    }
    public void ChangeCharLeft()
    {
        int max = GameManager.Instance.activeCharacter.Length-1 ;
        if (IsCommandPanelOpen())
        {
            max = Enemy.Count - 1;
        }

        activeIndex -= 1;
        if (activeIndex <= -1)
        {
            activeIndex = max;
        }
        ChangeChar(activeIndex);
    }

    public void ChangeChar(int id)
    {
        if (battleState == 0)
        {
            BU.CharPanel[id].Select();
        }
        ChangeCamChar(id);
    }


    public void ChangeCamChar(int id)
    {
        if (battleState == 0)
        {
            ChangeCamPriority(GetCharSlot(id));
        }
        else
        {
           MainCam.ActiveVirtualCamera.LookAt = EnemyPos[GetEnemySlot(id)];
        }
    }

    public void ChangeCamPriority(int nextCam, bool follow = false)
    {
        MainCam.ActiveVirtualCamera.Priority = 0;
        Cams[nextCam].Priority = 1;
        if (follow)
        {
            Cams[nextCam].LookAt = Player[playerActionIndex].transform;
            Cams[nextCam].Follow = Player[playerActionIndex].transform;
        }

    }

    public void AttackAction() {
        if (IsCommandPanelOpen())
        {
            switch (battleState)
            {
                case 0:
                    //Player selected the attack button
                    playerActionIndex = activeIndex;
                    battleState += 1;
                    ChangeCamPriority(6);
                    MainCam.ActiveVirtualCamera.LookAt = EnemyPos[GetEnemySlot(0)];

                    break;
                case 1:
                    //Player Choosing enemy to attack
                    //Player[playerActionIndex].GetComponent<Player>().target = Enemy[index];
                    //string attackType = Player[playerActionIndex].GetComponent<Player>().attackType;
                    //Player[playerActionIndex].GetComponent<Player>().attack = true;
                    ChangeCamPriority(GetCharSlot(playerActionIndex));

                    Queue.addActionQueue(playerActionIndex, activeIndex, ActionType.Attack, true);

                    battleState = 0;

                    commandPanel.SetActive(false);
                    break;
                case 2:
                    break;
                default:
                    break;
            }

        }
   
       //MainCam.ActiveVirtualCamera.Follow = EnemyPos[getEnemySlot(0)];
 
    }

    public void ClickButton()
    {
        
    }

    public void UpUi()
    {
        if (IsCommandPanelOpen())
        {
            activeIndex -= 1;
            if (activeIndex == -1)
            {
                activeIndex = 3;
            }
            //ChangeCommand(index);
        }
    }

    public void DownUi()
    {
        if (IsCommandPanelOpen())
        {
            activeIndex += 1;
            if (activeIndex == 4)
            {
                activeIndex = 0;
            }
            //ChangeCommand(index);
        }
    }


    /*public void UpDownUI(Vector2 ctx)
    {
        if (!isAnalogMoving)
        {
            Debug.LogWarning("asw");
            if (ctx.y == 1f)
            {
                Debug.LogWarning("asw");
                UpUi();
            }
            else if (ctx.y == -1f)
            {
                Debug.LogWarning("asw");
                DownUi();
            }
            Debug.LogWarning("asw");
            StartCoroutine(AnalogCD());
        }

    }*/


    public int GetCharSlot(int id) {
        return GameManager.Instance.activeCharacter[id].GetComponent<Player>().GetSlot();
    }

    public int GetEnemySlot(int id)
    {
        return Enemy[id].GetComponent<Player>().GetSlot();
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
        switch (Queue.battleQueue.Peek().isPlayer)
        {
            case true:
                DoActionPlayer(playerIndex, enemyIndex);
                break;
            default:
                DoActionEnemy(enemyIndex, playerIndex);
                break;
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

    public void DoActionPlayer(int playerIndex, int enemyIndex)
    {
        switch (Queue.battleQueue.Peek().actionType)
        {
            case ActionType.Attack:
                int damage = Player[playerIndex].CalculateDamage(Enemy[enemyIndex].GetPDEF());
                Enemy[enemyIndex].SetCurrentHealth(Enemy[enemyIndex].GetCurrentHealth() - damage);
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

    public void DoActionEnemy(int enemyIndex, int playerIndex)
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
