using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour
{
    public void OnClick(string sceneName)
    {
        SceneLoder.LoadScene(sceneName);
    }
}
