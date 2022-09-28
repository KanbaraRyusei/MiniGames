using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
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
        _ = SetGame();
        _timeManager.TimerStart();
    }

    private async UniTask SetGame()
    {
        await UniTask.DelayFrame(5);
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(_garbageCanPath, _garbageCanPosition, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(_dirtyPersonPath, _dirtyPersonPosition, Quaternion.identity);
        }
    }
}
