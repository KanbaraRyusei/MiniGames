using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBagController : MonoBehaviour
{
    [SerializeField]
    private int _speed;

    [SerializeField]
    private int _damage;

    [SerializeField]
    private string _gameOverTag = "Finish";

    private Rigidbody2D _rb;

    private void OnEnable()
    {
        _rb = _rb ?? GetComponent<Rigidbody2D>();
        ShootBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IDamage damage))
        {
            damage.OnDamage(_damage);
            Destroy(gameObject);
        }
        if(collision.tag == _gameOverTag)
        {
            Destroy(gameObject);
        }
    }

    private void ShootBullet()
    {
        _rb.AddForce(new Vector2(0f, _speed), ForceMode2D.Impulse);
    }
}
