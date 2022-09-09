using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCanModel : MonoBehaviour
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
}
