using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DirtyPersonModel : MonoBehaviour, IPunObservable
{
    public int MaxHp => _maxHp;
    public int HP => _hp;
    public bool CanAttack => _canAttack;

    [SerializeField]
    private int _maxHp;

    private int _hp;
    private bool _canAttack;

    private const int ZERO = 0;

    private void Awake()
    {
        _hp = _maxHp;
        _canAttack = true;
    }

    public void RecoveryHP(int value)
    {
        _hp += value;
    }

    public void ReduceHP(int value)
    {
        _hp -= value;
        if(_hp <= ZERO)
        {
            GameManager.GameOver();
        }
    }

    public void AttackFlagChange(bool flag)
    {
        _canAttack = flag;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(_hp);
            Debug.Log("c" + _hp);
        }
        else
        {
            _hp = (int)stream.ReceiveNext();
            Debug.Log("d" + _hp);
        }
    }
}
