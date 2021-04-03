using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{

    public Image BarFill;
    public Slider slider;
    public Gradient gradient;

    private void Awake()
    {
        //setMaxValue(100, 100);
    }

    public void SetMaxValue(int Value, int maxValue){
        slider.value = Value;
        slider.maxValue = maxValue;

        BarFill.color = gradient.Evaluate(1f);
    }

    public void SetValue(int Value){
        slider.value = Value;

        BarFill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetFloatValue(float Value)
    {
        slider.value = Value;

        BarFill.color = gradient.Evaluate(slider.normalizedValue);
    }


}
