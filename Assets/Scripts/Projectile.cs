using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject _launcher;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private int _damage;

    private Rigidbody2D _rb;

    public Projectile(GameObject launcher, Transform position)
    {
        Instantiate(this.gameObject, position);
        _launcher = launcher;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rb.AddForce(Vector2.left * _speed, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable opponent = other.gameObject.GetComponent<IDamageable>();
        opponent?.TakeDamage(_damage);
    }
}
