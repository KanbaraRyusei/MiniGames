using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonTransformView))]
public class GarbageCanController : PlayerControllerBase, ICollectable
{
    [SerializeField]
    GarbageCanModel _garbageCanModel;

    [SerializeField]
    Sprite _nomalSprite;

    [SerializeField]
    Sprite _specialSprite;

    SpriteRenderer _sp;

    PhotonView _photonView;

    private void Start()
    {
        _sp = GetComponent<SpriteRenderer>();

        _garbageCanModel.ObserveEveryValueChanged(model => model.CanAttack)
            .Subscribe(flag =>
            {
                SpriteChange(flag);
            });
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
        if (!_garbageCanModel.CanAttack) return;
        PhotonNetwork.Instantiate(_bulletPrefabName, transform.position, Quaternion.identity);
        _ = CoolTime();
    }

    protected override async UniTask CoolTime()
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
