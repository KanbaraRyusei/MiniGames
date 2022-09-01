using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public abstract class PlayerControllerBase : MonoBehaviour
{
    [SerializeField]
    [Header("スピード")]
    private int _speed;

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
        _rb.velocity = new Vector2(h, v);
    }

    protected abstract void Attack();
}
