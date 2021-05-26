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
    public Button SelectedButton;

    public List<Slider> playerTimelines;
    public List<Slider> enemyTimelines;

    public GameObject commandPanel;
    //public PlayableDirector director;

    //private bool isAnalogMoving;
    private int index;

    void OnEnable()
    {
        //CharPanel[index].Select();
        //StartCoroutine(startCinematicCam());
        //commandPanel.SetActive(false);
        //isAnalogMoving = false;
        for (int i = 0; i < BattleManager.Instance.Player.Count;i++) {
            playerTimelines[i].gameObject.SetActive(true);
            CharPanel[i].gameObject.SetActive(true);
            StartCoroutine(StartPlayerTimeline(i));
        }


        CharPanel[0].Select();
    }

    void Update()
    {

    }

    public void ChangeCommand(int id)
    {
        //MainCam.ActiveVirtualCamera.LookAt = CharPos[index];
        // MainCam.ActiveVirtualCamera.Follow = CharPos[index];
        CommandPanel[id].Select();
    }

    /*public void UpDownUI(Vector2 ctx) {
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
        
    }     */

    /*IEnumerator AnalogCD() {
        isAnalogMoving = true;
        yield return new WaitForSecondsRealtime(0.2f);
        isAnalogMoving = false;
    }*/

    public bool AnalogMoving(Vector2 ctx) {
        return ctx.y == 1 || ctx.y == -1f;
    }

    public void ClickButton() {
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
    }

    public void Back() { 
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
                if (index == BattleManager.Instance.playerIndex)
                {
                  /*  BattleManager.Instance.commandPanel.SetActive(true);

                    yield return new WaitUntil(() => BattleManager.Instance.IsCommandPanelOpen() == false);
                    BattleManager.Instance.battleState = 0;*/
                }
                else
                {
                    yield return new WaitForSeconds(2f);
                    if (index == BattleManager.Instance.playerIndex)
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
