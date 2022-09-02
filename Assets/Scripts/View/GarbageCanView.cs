using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarbageCanView : MonoBehaviour
{
    [SerializeField]
    Slider _slider;

    public void SetSliderMaxValue(float maxValue)
    {
        _slider.maxValue = maxValue;
    }

    public void SliderValueUpdate(float value)
    {
        _slider.value = value;
    }
}
