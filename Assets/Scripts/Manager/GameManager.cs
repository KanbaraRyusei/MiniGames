using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameManager
{
    public static Action OnGameOver;

    public static void GameOver()
    {
        OnGameOver?.Invoke();
        SceneLoder.LoadScene("ResultScene");
    }
}
