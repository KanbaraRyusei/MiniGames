using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class DirtyPersonController : PlayerControllerBase
{
    [SerializeField]
    DirtyPersonModel _dirtyPersonModel;

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

    }
}
