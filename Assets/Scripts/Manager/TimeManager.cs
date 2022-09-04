using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float Timer => _timer;

    private float _timer;

    private bool _stopTimer = true;

    private void Update()
    {
        if (_stopTimer) return;
        _timer += Time.deltaTime;
    }

    public void TimerStart()
    {
        _stopTimer = false;
    }

    public void TimerStop()
    {
        _stopTimer = true;
    }

    public void TimerReset()
    {
        _stopTimer = true;
        _timer = 0f;
    }
}
