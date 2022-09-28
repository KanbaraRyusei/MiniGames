using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

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
        _garbageCanModel = FindObjectOfType<GarbageCanModel>();

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
