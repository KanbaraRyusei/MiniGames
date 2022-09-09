using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;

public class GarbageCanController : PlayerControllerBase, ICollectable
{
    [SerializeField]
    GarbageCanModel _garbageCanModel;

    [SerializeField]
    Sprite _nomalSprite;

    [SerializeField]
    Sprite _specialSprite;

    SpriteRenderer _sp;

    private void Start()
    {
        _sp = GetComponent<SpriteRenderer>();

        _garbageCanModel.ObserveEveryValueChanged(model => model.CanAttack)
            .Subscribe(flag =>
            {
                SpriteChange(flag);
            });
    }

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

    private void SpriteChange(bool flag)
    {
        if(!flag)
        {
            _sp.sprite = _nomalSprite;
        }
        else
        {
            _sp.sprite = _specialSprite;
        }
    }

    public void Collect(int num)
    {
        _garbageCanModel.AddScore(num);
    }
}
