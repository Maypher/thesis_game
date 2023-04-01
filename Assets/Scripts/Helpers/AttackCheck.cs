using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    [SerializeField] private float _checkRadius = .5f;
    [SerializeField] private LayerMask _whatIsEnemy;

    public Collider2D[] GetEnemies()
    {
        return Physics2D.OverlapCircleAll(transform.position, _checkRadius, _whatIsEnemy);
    }

    public bool Check()
    {
        return Physics2D.OverlapCircleAll(transform.position, _checkRadius, _whatIsEnemy).Length != 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _checkRadius);
    }
}
