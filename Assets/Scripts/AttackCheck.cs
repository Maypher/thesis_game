using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    [SerializeField] private float _checkRadius = 0.3f;
    [SerializeField] private LayerMask _whatIsEnemy;

    public Collider2D[] Check()
    {
        return Physics2D.OverlapCircleAll(transform.position, _checkRadius, _whatIsEnemy);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _checkRadius);
    }
}
