using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class DirtyPersonController : PlayerControllerBase, IDamage
{
    [SerializeField]
    DirtyPersonModel _dirtyPersonModel;

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
        if (!_dirtyPersonModel.CanAttack) return;
        var bullet = InactiveBulletsSearch();
        if(bullet.Length > 0)
        {
            bullet[0].transform.position = transform.position;
            bullet[0].SetActive(true);
        }
        else
        {
            var newBullet = Instantiate(_bulletPrefab, transform);
            _bullets.Add(newBullet);
        }
        CoolTime();
    }

    protected override async void CoolTime()
    {
        _isCoolTime = true;
        await UniTask.Delay(_coolTime);
        _isCoolTime = false;
    }

    public void OnDamage(int damage)
    {
        _dirtyPersonModel.ReduceHP(damage);
    }
}
