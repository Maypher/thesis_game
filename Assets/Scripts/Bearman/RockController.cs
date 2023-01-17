using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : StateMachine<RockController>, IDamageable
{
    private int _health;
    [HideInInspector] public Vector2 ThrowForce = Vector2.zero;
    [HideInInspector] public float Torque = -20;

    [HideInInspector] public bool CanBeDamaged;

    /// <summary>
    /// Change the physics2D settings to make this object ignore collision with collider
    /// </summary>
    /// <param name="collider"></param>
    public void IgnoreCollisionWith(Collider2D collider) => Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collider);

    public void SetHealth(int health) => _health = health;

    public bool TakeDamage(int damagePt)
    {
        if (CanBeDamaged)
        {
            _health -= damagePt;
            if (_health <= 0) 
            {
                Kill();
                return true;
            }
        }
        return false;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}