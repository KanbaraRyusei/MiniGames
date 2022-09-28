using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class DirtyPersonPresenter : MonoBehaviour
{
    [SerializeField]
    DirtyPersonView _dirtyPersonView;

    [SerializeField]
    TimeManager _timeManager;

    [SerializeField]
    private float _canAttackTime;

    DirtyPersonModel _dirtyPersonModel;

    private void Start()
    {
        _dirtyPersonModel = FindObjectOfType<DirtyPersonModel>();

        _dirtyPersonView.SetSliderMaxValue(_dirtyPersonModel.MaxHp);

        _dirtyPersonModel.ObserveEveryValueChanged(model => model.HP)
            .Subscribe(value => _dirtyPersonView.SliderValueUpdate(value))
            .AddTo(this);

        _timeManager.ObserveEveryValueChanged(manager => manager.Timer)
            .Subscribe(value =>
            {
                if(_canAttackTime < value)
                {
                    _dirtyPersonModel.AttackFlagChange(false);
                }
            }).AddTo(this);
    }
}
