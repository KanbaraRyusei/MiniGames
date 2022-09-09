using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitManager : MonoBehaviour
{
    [SerializeField]
    TimeManager _timeManager;

    private void Start()
    {
        _timeManager.TimerStart();
    }
}
