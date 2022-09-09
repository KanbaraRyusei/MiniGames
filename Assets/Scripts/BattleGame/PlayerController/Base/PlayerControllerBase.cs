using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public abstract class PlayerControllerBase : MonoBehaviour
{
    [SerializeField]
    [Header("スピード")]
    protected int _speed;

    [SerializeField]
    [Header("動ける方向")]
    protected MoveDirection _moveDirection = MoveDirection.Horizontal;

    [SerializeField]
    [Header("クールタイム(ミリ秒)")]
    protected int _coolTime;

    [SerializeField]
    [Header("弾のプレハブ")]
    protected GameObject _bulletPrefab;

    protected bool _isCoolTime = false;

    protected Rigidbody2D _rb;

    protected List<GameObject> _bullets = new List<GameObject>();

    protected virtual void Awake()
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

    protected abstract void CoolTime();

    protected GameObject[] InactiveBulletsSearch()
    {
        return _bullets.Where(x => !x.activeInHierarchy)?.ToArray();
    }
}
