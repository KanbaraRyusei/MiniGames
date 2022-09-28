using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class GarbageCanPresenter : MonoBehaviour
{
    [SerializeField]
    GarbageCanView _garbageCanView;

    [SerializeField]
    TimeManager _timeManager;

    [SerializeField]
    private float _canAttackTime;

    [SerializeField]
    private int _canAttackScore = 10;

    GarbageCanModel _garbageCanModel;

    private void Start()
    {
        _ = GetGarbageCan();
    }

    private async UniTask GetGarbageCan()
    {
        await UniTask.DelayFrame(15);
        _garbageCanModel = FindObjectOfType<GarbageCanModel>();
        SetRx();
    }

    private void SetRx()
    {
        _garbageCanModel.ObserveEveryValueChanged(model => model.Score)
            .Subscribe(value =>
            {
                _garbageCanView.TextValueUpdate(value);
            }).AddTo(this);

        _garbageCanModel.ObserveEveryValueChanged(model => model.Score)
            .Subscribe(value =>
            {
                _garbageCanModel.AttackFlagChange(value >= _canAttackScore);
            }).AddTo(this);

        _timeManager.ObserveEveryValueChanged(manager => manager.Timer)
            .Subscribe(value =>
            {
                _garbageCanModel.AttackFlagChange(_canAttackTime < value);
            }).AddTo(this);
    }
}
