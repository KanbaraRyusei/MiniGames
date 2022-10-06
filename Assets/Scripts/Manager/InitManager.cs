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
    TextChanger _textChanger;

    [SerializeField]
    string _textValue = "OK";

    [SerializeField]
    string _garbageCanPath;

    [SerializeField]
    string _dirtyPersonPath;

    [SerializeField]
    Vector3 _garbageCanPosition;

    [SerializeField]
    Vector3 _dirtyPersonPosition;

    bool _onSpace = false;

    private void Start()
    {
        _ = SetGame();
    }

    private void Update()
    {
        if (_onSpace) return;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _textChanger.TextChange(_textValue);
            GameManager.OnSpace();
            _onSpace = true;
            if (GameManager.CanStartGame)
            {
                GameManager.GameStart();
                _timeManager.TimerStart();
            }
        }
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
