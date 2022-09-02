using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class DirtyPersonPresenter : MonoBehaviour
{
    [SerializeField]
    DirtyPersonModel _dirtyPersonModel;

    [SerializeField]
    DirtyPersonView _dirtyPersonView;

    private void Start()
    {
        _dirtyPersonView.SetSliderMaxValue(_dirtyPersonModel.MaxHp);

        _dirtyPersonModel.ObserveEveryValueChanged(model => model.HP)
            .Subscribe(value => _dirtyPersonView.SliderValueUpdate(value))
            .AddTo(this);
    }
}
