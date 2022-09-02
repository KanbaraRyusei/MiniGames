using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GarbageCanPresenter : MonoBehaviour
{
    [SerializeField]
    GarbageCanModel _garbageCanModel;

    [SerializeField]
    GarbageCanView _garbageCanView;

    private void Start()
    {
        _garbageCanView.SetSliderMaxValue(_garbageCanModel.MaxHp);

        _garbageCanModel.ObserveEveryValueChanged(model => model.HP)
            .Subscribe(value =>
            {
                _garbageCanView.SliderValueUpdate(value);
            }).AddTo(this);
    }
}
