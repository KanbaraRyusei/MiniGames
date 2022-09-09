using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private int _speed = 1;
    
    private Rigidbody2D _rb;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        ShootBullet();
    }

    private void ShootBullet()
    {
        _rb.AddForce(new Vector2(0f, _speed), ForceMode2D.Impulse);
    }
}
