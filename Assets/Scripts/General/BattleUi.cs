using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BattleUi : MonoBehaviour
{
    public List<Button> CharPanel;
    public List<Button> CommandPanel;

    public GameObject TimelinePrefab;
    public List<Slider> playerTimelines;
    public List<Slider> enemyTimelines;

    public GameObject commandPanel;
    //public PlayableDirector director;

    private bool isAnalogMoving;
    private int index;

    private void Awake()
    {
        //control.Battle.Cancel.performed += ctx => back();
        //BattleManager.Instance.battleStart(CharPanel);
    }

    void Start()
    {
        //CharPanel[index].Select();
        //StartCoroutine(startCinematicCam());
        //commandPanel.SetActive(false);
        //isAnalogMoving = false;
        for (int i = 0; i < BattleManager.Instance.Player.Count;i++) {
            GameObject timelines = Instantiate(TimelinePrefab, TimelinePrefab.transform.position, TimelinePrefab.transform.rotation);
            timelines.transform.parent = BattleManager.Instance.BU.gameObject.transform;
            playerTimelines.Add(timelines.GetComponent<Slider>());
            StartCoroutine(StartPlayerTimeline(i));
        }

        CharPanel[0].Select();
    }

    void Update()
    {
       // Debug.LogWarning(index);
    }

    public void ChangeCommand(int id)
    {
        //MainCam.ActiveVirtualCamera.LookAt = CharPos[index];
        // MainCam.ActiveVirtualCamera.Follow = CharPos[index];
        CommandPanel[id].Select();
    }

    public bool IsCommadPanelOpen()
    {
        return BattleManager.Instance.battleState >= 1;
    }

    public void ChangeCharRight()
    {
       // int max = GameManager.Instance.activeCharacter.Length;
        if (IsCommadPanelOpen())
        {
           // max = BattleManager.Instance.Enemy.Count;
        }

        index += 1;
        if (index >= 4)
        {
            index = 0;
        }
        ChangeChar(index);
    }
    public void ChangeCharLeft()
    {
        //int max = GameManager.Instance.activeCharacter.Length - 1;
        if (IsCommadPanelOpen())
        {
            //max = BattleManager.Instance.Enemy.Count - 1;
        }

        index -= 1;
        if (index <= -1)
        {
            index = 4;
        }
        ChangeChar(index);
    }

    public void ChangeChar(int id)
    {
        if (BattleManager.Instance.battleState == 0)
        {
            CharPanel[id].Select();
        }
    }

    public void UpUi() {
        if (IsCommadPanelOpen()) {
            index -= 1;
            if (index == -1)
            {
                index = 3;
            }
            ChangeCommand(index);
        }
    }

    public void DownUi() {
        if (IsCommadPanelOpen()) {
            index += 1;
            if (index == 4)
            {
                index = 0;
            }
            ChangeCommand(index);
        }
    }


    public void UpDownUI(Vector2 ctx) {
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
        
    }

    IEnumerator AnalogCD() {
        isAnalogMoving = true;
        yield return new WaitForSecondsRealtime(0.2f);
        isAnalogMoving = false;
    }

    public bool AnalogMoving(Vector2 ctx) {
        return ctx.y == 1 || ctx.y == -1f;
    }

    public void ChangetoCloseUp() {
        BattleManager.Instance.battleState += 1;

        commandPanel.SetActive(true);
        BattleManager.Instance.Cams[2].LookAt = BattleManager.Instance.CharPos[index];
        BattleManager.Instance.Cams[2].Follow = BattleManager.Instance.CharPos[index];
        BattleManager.Instance.Cams[2].Priority = 10;
        Time.timeScale = 0;
        index = 0;
        CommandPanel[index].Select();

    }

    public void ClickButton() {
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
    }

    public void Back() {
        BattleManager.Instance.battleState -= 1;

        index = 0;
        CharPanel[index].GetComponent<Button>().Select();
        commandPanel.SetActive(false);
        BattleManager.Instance.Cams[2].LookAt = null;
        BattleManager.Instance.Cams[2].Follow = null;
        Time.timeScale = 1;
        BattleManager.Instance.Cams[2].Priority = 0;
    }

    public void ShowSkill()
    {
        if (BattleManager.Instance.battleState == 0)
        {

        }
        else
        {
            Back();
        }

    }

    public void ShowTalent()
    {

    }

    public void ShowItem()
    {


    }

    IEnumerator StartPlayerTimeline(int index) {
        Player player = BattleManager.Instance.Player[index].GetComponent<Player>();
        float speed = player.CalculateSpeed();
        float APBar = 0;
        bool isGaugeFull = false ;
        Debug.LogWarning(index);
        while (GameManager.Instance.isBattle)
        {
            if (!isGaugeFull)
            {
                APBar += speed * Time.deltaTime;
                APBar = Mathf.Clamp(APBar, 1, 100f);

                playerTimelines[index].value = APBar;
                if (APBar >= 100)
                {
                    isGaugeFull = true;
                    APBar = 0;
                }
            }
            else {
                //check if it's active character
                if (index == BattleManager.Instance.activeIndex)
                {
                    BattleManager.Instance.commandPanel.SetActive(true);

                    yield return new WaitUntil(() => BattleManager.Instance.IsCommandPanelOpen() == false);
                    BattleManager.Instance.battleState = 0;
                }
                else
                {
                    yield return new WaitForSeconds(2f);
                    if (index == BattleManager.Instance.activeIndex)
                    {

                    }
                    else
                    {
                        //do Command
                    }
                }
        
                playerTimelines[index].value = 0f;
            }

            yield return null;
        }
        yield return null;
    }

    /*IEnumerator startEnemyTimeline(int index)
    {
        EnemyStats playerStat = BattleManager.Instance.Enemy[index].GetComponent<Enemy>().stats;
        float speed = GameManager.Instance.calculateSpeed(playerStat.Speed, playerStat.AGI);
        float APBar = 0;
        bool isGaugeFull = false;

        while (GameManager.Instance.isBattle)
        {
            if (!isGaugeFull)
            {
                APBar += speed * Time.deltaTime;
                APBar = Mathf.Clamp(APBar, 1, 100f);

                playerTimelines[index].value = APBar;
                if (APBar >= 100)
                {
                    isGaugeFull = true;
                    APBar = 0;
                }
            }
            else
            {
                BattleManager.Instance.Enemy[index].GetComponent<Enemy>();
            }
            yield return null;
        }
        yield return null;
    }*/

}
