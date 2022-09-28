using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitManager : MonoBehaviour
{
    [SerializeField]
    TimeManager _timeManager;

    private void Start()
    {
        Init();
        _timeManager.TimerStart();
    }

    private void Init()
    {
        var master = FindObjectOfType<GarbageCanController>().gameObject;
        var enemy = FindObjectOfType<DirtyPersonController>().gameObject;
        SceneManager.MoveGameObjectToScene(master, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(enemy, SceneManager.GetActiveScene());
    }
}
