using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float Timer => _timer;

    private float _timer;
    private float _oldTimer;
    private bool _stopTimer = true;

    private void Update()
    {
        if (_stopTimer) return;
        _timer += Time.deltaTime;
    }

    public void TimerStart()
    {
        _stopTimer = false;
        _timer = _oldTimer;
    }

    public void TimerStop()
    {
        _stopTimer = true;
        _oldTimer = _timer;
    }

    public void TimerReset()
    {
        _stopTimer = true;
        _oldTimer = 0f;
        _timer = 0f;
    }
}
