using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarbageCanView : MonoBehaviour
{
    [SerializeField]
    Text _text;

    public void SliderValueUpdate(int value)
    {
        _text.text = value.ToString();
    }
}
