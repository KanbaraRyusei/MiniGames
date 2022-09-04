using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GarbageCanController : PlayerControllerBase
{
    [SerializeField]
    GarbageCanModel _garbageCanModel;

    private List<GameObject> _bullets;

    protected override void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void Attack()
    {
        if (_isCoolTime) return;
        if (!_garbageCanModel.CanAttack) return;
    }

    protected override async void CoolTime()
    {
        _isCoolTime = true;
        await UniTask.Delay(_coolTime);
        _isCoolTime = false;
    }
}
