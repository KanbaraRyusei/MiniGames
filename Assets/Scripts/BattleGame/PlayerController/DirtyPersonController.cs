using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonTransformView))]

public class DirtyPersonController : PlayerControllerBase, IDamage
{
    [SerializeField]
    DirtyPersonModel _dirtyPersonModel;

    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    protected override void Update()
    {
        if (!_photonView.IsMine) return;
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
        _ = CoolTime();
    }

    protected override async UniTask CoolTime()
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
