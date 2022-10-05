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
        if (!_photonView.IsMine || !GameManager.IsStartGame) return;
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
        PhotonNetwork.Instantiate(_bulletPrefabName, transform.position, Quaternion.identity);
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
