using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameManager
{
    public static bool IsStartGame { get; private set; } = false;
    public static bool IsDebugMode { get; private set; } = false;

    public static Action OnGameOver;

    public static bool CanStartGame => _onSpaceCount >= One;

    private static int _onSpaceCount = 0;

    private static readonly int One = 1;

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

    public static void OnSpace()
    {
        _onSpaceCount++;
    }

    public static void DebugMode()
    {
        IsDebugMode = true;
    }
}
