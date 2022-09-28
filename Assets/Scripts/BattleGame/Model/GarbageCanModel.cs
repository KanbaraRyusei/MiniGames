using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GarbageCanModel : MonoBehaviour, IPunObservable
{
    public int Score => _score;
    public bool CanAttack => _canAttack;

    private int _score;
    private bool _canAttack;

    private void Awake()
    {
        _score = 0;
        _canAttack = false;
    }

    public void AddScore(int num)
    {
        _score += num;
    }

    public void AttackFlagChange(bool flag)
    {
        _canAttack = flag;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(_score);
            Debug.Log("a" + _score);
        }
        else
        {
            _score = (int)stream.ReceiveNext();
            Debug.Log("b" + _score);
        }
    }
}
