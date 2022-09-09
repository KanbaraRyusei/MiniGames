using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GarbageCanView : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _text;

    public void TextValueUpdate(int value)
    {
        _text.text = value.ToString();
    }
}
