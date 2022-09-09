using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class GarbageController : MonoBehaviour
{
    [SerializeField]
    private int _speed = 1;

    [SerializeField]
    private int _amount = 1;

    [SerializeField]
    private string _gameOverTag = "Finish";
    
    private Rigidbody2D _rb;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        ShootBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect(_amount);
            gameObject.SetActive(false);
        }
        if(collision.tag == _gameOverTag)
        {
            gameObject.SetActive(false);
        }
    }

    private void ShootBullet()
    {
        _rb.AddForce(new Vector2(0f, _speed), ForceMode2D.Impulse);
    }
}
