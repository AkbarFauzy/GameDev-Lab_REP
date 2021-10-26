using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class BattleUi : MonoBehaviour
{
    public List<Button> CharPanel;
    public GameObject CommandPanel;
    public Button SelectedButton;

    public List<Slider> playerTimelines;
    public List<Slider> enemyTimelines;

    public GameObject commandPanel;
    public GameObject skillPanel;
    public GameObject SkillButtonPrefab;
    //public PlayableDirector director;

    //private bool isAnalogMoving;
    private int index;

    void Start()
    {
        commandPanel.SetActive(false);
        for (int i = 0; i < BattleManager.Instance.Player.Count; i++)
        {
            playerTimelines[i].gameObject.SetActive(true);
            CharPanel[i].gameObject.SetActive(true);
            StartCoroutine(StartPlayerTimeline(i));
        }

        for (int i = 0; i < BattleManager.Instance.EnemyObject.Count; i++)
        {
            enemyTimelines[i].gameObject.SetActive(true);
            StartCoroutine(StartEnemyTimeline(i));
        }

        CharPanel[0].Select();
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
    public void ShowSkillPanel()
    {
        skillPanel.SetActive(true);
        foreach (Abilities skill in BattleManager.Instance.Player[BattleManager.Instance.PlayerIndex].abilities)
        {
            if (skill.isUnlocked)
            {
                GameObject button = Instantiate(SkillButtonPrefab);
                button.transform.SetParent(skillPanel.transform, false);
                //GameObject txt = button.transform.GetChild(0).gameObject;
                button.GetComponentInChildren<TMP_Text>().text = skill.skills.skillName;
            }
        }
    }

    public void HideSkillPanel() {
        skillPanel.SetActive(false);
        foreach (Transform child in skillPanel.transform) {
            Destroy(child.gameObject);
        }
    }

    public void ShowTalent()
    {

    }

    public void ShowItem()
    {

    }

    IEnumerator StartPlayerTimeline(int index)
    {
        float APBar = 0;
        bool isGaugeFull = false;

        while (GameManager.Instance.isBattle)
        {
            if (!isGaugeFull)
            {
                float speed = BattleManager.Instance.Player[index].GetComponent<Player>().SPD;
                APBar += (speed * Time.deltaTime) / 2;
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
                if (BattleManager.Instance.PlayerIndex == index) {
                    BattleManager.Instance.Player[index].IsAction = true;
                    BattleManager.Instance.BU.commandPanel.SetActive(true);
                    Debug.Log("waiting");
                    yield return new WaitWhile(() => BattleManager.Instance.PlayerIndex == index || !BattleManager.Instance.Player[index].IsAction);
                    Debug.Log("go");
                }
                else { 
                
                }
                BattleManager.Instance.Player[index].IsAction = false;
                yield return new WaitForSeconds(1f);
                playerTimelines[index].value = 0;
                isGaugeFull = false;
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator StartEnemyTimeline(int index)
    {
        float APBar = 0;
        bool isGaugeFull = false;

        while (GameManager.Instance.isBattle)
        {
            if (!isGaugeFull)
            {
                float speed = BattleManager.Instance.EnemyObject[index].GetComponent<Enemy>().SPD;
                APBar += (speed * Time.deltaTime) / 2;
                APBar = Mathf.Clamp(APBar, 1, 100f);
                enemyTimelines[index].value = APBar;
                if (APBar >= 100)
                {
                    isGaugeFull = true;
                    APBar = 0;
                }
            }
            else
            {
                enemyTimelines[index].value = 0;
                isGaugeFull = false;
            }
            yield return null;
        }
        yield return null;
    }

}
