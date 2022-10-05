using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

public class DirtyPersonPresenter : MonoBehaviour
{
    [SerializeField]
    DirtyPersonView _dirtyPersonView;

    [SerializeField]
    TimeManager _timeManager;

    [SerializeField]
    private float _canAttackTime;

    DirtyPersonModel _dirtyPersonModel = null;

    private void Start()
    {
        _ = GetDirtyPerson();
    }

    private async UniTask GetDirtyPerson()
    {
        await UniTask.DelayFrame(15);
        _dirtyPersonModel = FindObjectOfType<DirtyPersonModel>();
        SetRx();
    }

    private void SetRx()
    {
        _dirtyPersonView.SetSliderMaxValue(_dirtyPersonModel.MaxHp);

        _dirtyPersonModel.ObserveEveryValueChanged(model => model.HP)
            .Subscribe(value => _dirtyPersonView.SliderValueUpdate(value))
            .AddTo(this);

        //_dirtyPersonModel.ObserveEveryValueChanged(model => model.CanAttack)

        if (!GameManager.IsDebugMode) return;

        _timeManager.ObserveEveryValueChanged(manager => manager.Timer)
            .Subscribe(value =>
            {
                if (_canAttackTime < value)
                {
                    _dirtyPersonModel.AttackFlagChange(false);
                }
            }).AddTo(this);
    }
}
