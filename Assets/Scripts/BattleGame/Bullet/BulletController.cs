using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int _speed;
    private float _gravityScale;
    
    private Rigidbody2D _rb;
    private Transform _bullet;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Transform bullet, int speed, float gravityScale)
    {
        _bullet = bullet;
        _speed = speed;
        _gravityScale = gravityScale;
        ShootBullet();
    }

    private void ShootBullet()
    {
        transform.position = _bullet.position;
        _rb.AddForce(new Vector2(0f, _speed), ForceMode2D.Impulse);
        _rb.gravityScale = _gravityScale;
    }
}
