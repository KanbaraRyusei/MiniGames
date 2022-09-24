using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InitManager : MonoBehaviour
{
    [SerializeField]
    TimeManager _timeManager;

    [SerializeField]
    string _garbageCanPath;

    [SerializeField]
    string _dirtyPersonPath;

    [SerializeField]
    Vector3 _garbageCanPosition;

    [SerializeField]
    Vector3 _dirtyPersonPosition;

    private void Start()
    {
        PhotonNetwork.Instantiate(_garbageCanPath, _garbageCanPosition, Quaternion.identity);
        _timeManager.TimerStart();
    }
}
