using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public abstract class PlayerControllerBase : MonoBehaviour
{
    [SerializeField]
    [Header("スピード")]
    private int _speed;

    [SerializeField]
    [Header("動ける方向")]
    private MoveDirection _moveDirection = MoveDirection.Horizontal;

    [SerializeField]
    [Header("クールタイム")]
    private int _coolTime;

    Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        Move();
        if(Input.GetButton("Jump"))
        {
            Attack();
        }
    }

    protected virtual void Move()
    {
        float h = Input.GetAxisRaw("Horizontal") * _speed;
        float v = Input.GetAxisRaw("Vertical") * _speed;
        switch (_moveDirection)
        {
            case MoveDirection.All:
                break;
            case MoveDirection.Horizontal:
                v = 0f;
                break;
            case MoveDirection.Vertical:
                h = 0f;
                break;
        }
        _rb.velocity = new Vector2(h, v);
    }

    protected abstract void Attack();

    enum MoveDirection
    {
        All,
        Horizontal,
        Vertical
    }
}
