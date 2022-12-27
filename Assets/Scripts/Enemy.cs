using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, IAttack
{
    [SerializeField] private int maxHealth = 100;
    private int health;

    [SerializeField] private float _attackInterval = 2f;
    private float _attackTimer = 0;

    private Transform _launchPos;
    [SerializeField] private GameObject _arrow;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        _launchPos = GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _attackInterval)
        {
            Attack();
            _attackTimer = 0;
        }
    }

    public bool TakeDamage(int damage) 
    {
        health -= damage;
        
        if (health <= 0)
        {
            Kill();
            return true;
        }

        return false;
    }

    public void Kill() => Destroy(this.gameObject);

    public void Attack()
    {
        GameObject arrow = Instantiate(_arrow);
        arrow.GetComponent<Projectile>().OnKill += OnKill;
    }

    public void FinishAttack() {}

    private void OnKill()
    {
        this.GetComponent<SpriteRenderer>().color = Color.green;
    }
}
