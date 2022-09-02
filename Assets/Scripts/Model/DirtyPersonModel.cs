using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyPersonModel : MonoBehaviour
{
    public int MaxHp => _maxHp;
    public int HP => _hp;

    [SerializeField]
    private int _maxHp;

    private int _hp;

    private void Awake()
    {
        _hp = _maxHp;
    }

    public void RecoveryHP(int value)
    {
        _hp += value;
    }

    public void ReduceHP(int value)
    {
        _hp -= value;
    }
}
