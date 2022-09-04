using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyPersonModel : MonoBehaviour
{
    public int MaxHp => _maxHp;
    public int HP => _hp;
    public bool CanAttack => _canAttack;

    [SerializeField]
    private int _maxHp;

    private int _hp;
    private bool _canAttack;

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
    }

    public void AttackFlagChange(bool flag)
    {
        _canAttack = flag;
    }
}
