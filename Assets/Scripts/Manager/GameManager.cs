using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameManager
{
    public static bool IsStartGame { get; private set; } = false;

    public static Action OnGameOver;

    public static void GameStart()
    {
        IsStartGame = true;
    }

    public static void GameOver()
    {
        OnGameOver?.Invoke();
        IsStartGame = false;
        SceneLoder.LoadScene("ResultScene");
    }
}
