using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCanController : PlayerControllerBase
{
    [SerializeField]
    GarbageCanModel _garbageCanModel;

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
        if(!_garbageCanModel.CanAttack)
        {
            return;
        }
    }
}
