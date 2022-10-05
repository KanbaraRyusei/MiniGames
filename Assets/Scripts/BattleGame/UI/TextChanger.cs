using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextChanger : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    public void TextChange(string value)
    {
        _text.text = value;
    }
}
