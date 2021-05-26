using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AtbGauge : MonoBehaviour
{
    public List<GameObject> gauge;
    public List<BarScript> ATBBar;
    public CharacterStats character;
    public TextMeshProUGUI txt;
    public float atbAmount;


    // Start is called before the first frame update
    /*void Start()
    {
        int count = character.driveGauge;
        for (int i = 0; i < count; i++)
        {
            gauge[i].SetActive(true);
            ATBBar.Add(gauge[i].GetComponent<BarScript>());
            ATBBar[i].setMaxValue(0.001f, 100);
        }
        txt.text = "x0";
        //StartCoroutine(StartATB());
        StartCoroutine(check());
    }

    IEnumerator check()
    {
        yield return new WaitForSeconds(1f);
    } */ 

        /*IEnumerator StartATB()
        {
            float atbRegen = GameManager.Instance.calculateSpeed(character.Speed, character.AGI);
            int atbCount = 0;
            bool atbNotFull = true;
            while (true)
            {
                if (atbNotFull)
                {
                    atbAmount += atbRegen * Time.deltaTime;
                    Mathf.Clamp(atbAmount, 1, 100f * ATBBar.Count);
                    ATBBar[atbCount].setFloatValue(atbAmount);
                    if (atbAmount > 100)
                    {
                        atbCount += 1;
                        atbAmount = 0;
                        txt.SetText("x" + atbCount.ToString());
                    }

                    if (atbCount == ATBBar.Count)
                    {
                        atbNotFull = false;

                    }
                }
                yield return null;
            }
            //yield return null;
        }
        */

    }
