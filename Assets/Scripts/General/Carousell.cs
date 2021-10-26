using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousell : MonoBehaviour
{
   [System.Serializable] private struct ScaleRange {
         [SerializeField] public float min;
         [SerializeField] public float max;
    }

    [SerializeField] private List<GameObject> panels;
    [SerializeField] private ScaleRange scaleRange;
 
    [SerializeField] [Range(10, 30)] private float speed;
    [SerializeField] private bool isLoop;

    private Vector3 offset;
    private Vector3 lastPos;
    private Vector3 initPos;
    private int currentPanels;
    private int lastPanels;
    private bool isCD;
    private int activePanel;


    private void Start()
    {
        isCD = false;
        activePanel = 0;

        foreach (GameObject _panel in panels)
        {
            _panel.transform.localScale = new Vector3(scaleRange.min, scaleRange.min, scaleRange.min);
            if (_panel.activeSelf) {
                activePanel++;
            }
        }

        currentPanels = 0;
        lastPanels = GameManager.Instance.activeCharacter.Length-1;
        panels[currentPanels].transform.localScale = new Vector3(scaleRange.max, scaleRange.max, scaleRange.max);
        offset = (activePanel > 1) ? offset = panels[0].transform.localPosition - panels[1].transform.localPosition : offset = new Vector3(0,0,0);
        initPos = panels[0].transform.localPosition;
        lastPos = panels[activePanel-1].transform.localPosition;
    }

    public void NextPanel()
    {
        if (!isCD)
        {
            for (int i = 0; i < activePanel; i++)
            {
                if (i == currentPanels)
                {
                    StartCoroutine(ChangePositionNext(panels[i], true));
                }
                else { 
                    StartCoroutine(ChangePositionNext(panels[i]));
                }
            }
            if (currentPanels + 1 >= activePanel)
            {
                currentPanels = 0;
                lastPanels = activePanel - 1;
                StartCoroutine(ChangeScale(panels[lastPanels]));
            }
            else
            {
                lastPanels = currentPanels;
                currentPanels += 1;
                StartCoroutine(ChangeScale(panels[currentPanels - 1]));
            }

        }
       
    }

    public void PreviousPanel() {
        if (!isCD)
        {
            for (int i = 0; i < activePanel; i++)
            {
                if (i == lastPanels)
                {
                    StartCoroutine(ChangePositionPrev(panels[i], true));
                }
                else
                {
                    StartCoroutine(ChangePositionPrev(panels[i]));
                }
            }
            if (lastPanels - 1 <= -1)
            {
                currentPanels = 0;
                lastPanels = activePanel - 1;
                StartCoroutine(ChangeScale(panels[currentPanels + 1]));
            }
            else
            {
                StartCoroutine(ChangeScale(panels[currentPanels]));
                currentPanels = lastPanels;
                lastPanels -= 1;
            }

        }
    }

    private IEnumerator ChangeScale(GameObject panel)
    {
        float counter = 0f;
        isCD = true;
        while (counter < 0.3f)
        {
            panels[currentPanels].transform.localScale = Vector3.Lerp(panels[currentPanels].transform.localScale, new Vector3(scaleRange.max, scaleRange.max, scaleRange.max), Mathf.Clamp(speed * Time.deltaTime, 0, 1));
            panel.transform.localScale = Vector3.Lerp(panel.transform.localScale, new Vector3(scaleRange.min, scaleRange.min, scaleRange.min), Mathf.Clamp(speed* Time.deltaTime,0,1));
            counter += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.1f);
        isCD = false;
    }

    private IEnumerator ChangePositionNext(GameObject panel1, bool isChangeToLast = false) {
        float counter = 0f;
        Vector3 originalPos = panel1.transform.localPosition;

        if (isChangeToLast)
        {
            while (counter < 0.05f)
            {
                panel1.transform.localPosition = Vector3.Lerp(panel1.transform.localPosition, originalPos + offset, Mathf.Clamp(speed * Time.deltaTime, 0, 1));
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            panel1.transform.localPosition = lastPos - offset;
            while (counter < 0.1f)
            {
                panel1.transform.localPosition = Vector3.Lerp(panel1.transform.localPosition, lastPos, Mathf.Clamp(speed * Time.deltaTime, 0, 1));
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        else { 
            while (counter < 0.1f) {
                panel1.transform.localPosition = Vector3.Lerp(panel1.transform.localPosition, originalPos + offset, Mathf.Clamp(speed * Time.deltaTime, 0, 1));            
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private IEnumerator ChangePositionPrev(GameObject panel1, bool isChangeToFirst = false)
    {
        float counter = 0f;
        Vector3 originalPos = panel1.transform.localPosition;

        if (isChangeToFirst)
        {
            while (counter < 0.05f)
            {
                panel1.transform.localPosition = Vector3.Lerp(panel1.transform.localPosition, originalPos - offset, Mathf.Clamp(speed * Time.deltaTime, 0, 1));
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
           panel1.transform.localPosition = initPos + offset;
            while (counter < 0.1f)
            {
                panel1.transform.localPosition = Vector3.Lerp(panel1.transform.localPosition, initPos, Mathf.Clamp(speed * Time.deltaTime, 0, 1));
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (counter < 0.1f)
            {
                panel1.transform.localPosition = Vector3.Lerp(panel1.transform.localPosition, originalPos - offset, Mathf.Clamp(speed * Time.deltaTime, 0, 1));
                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public bool IsCD() => isCD;

}
