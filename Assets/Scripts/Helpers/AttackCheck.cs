using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    [SerializeField] private float _checkRadius = .5f;
    [SerializeField] private LayerMask _whatIsEnemy;

    public Collider2D[] Check()
    {
        return Physics2D.OverlapCircleAll(transform.position, _checkRadius, _whatIsEnemy);
    }
}
