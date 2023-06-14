using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackCheck : MonoBehaviour
{
    [SerializeField] private float _checkRadius = .5f;
    [SerializeField] private LayerMask _whatIsEnemy;

    [HideInInspector] public Action enemyEnteredAttackArea;

    private bool calledOnStay = false;

    public Collider2D[] GetEnemies()
    {
        return Physics2D.OverlapCircleAll(transform.position, _checkRadius, _whatIsEnemy);
    }

    public bool Check()
    {
        return Physics2D.OverlapCircleAll(transform.position, _checkRadius, _whatIsEnemy).Length != 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_whatIsEnemy.value & (1 << collision.gameObject.layer)) > 0) enemyEnteredAttackArea?.Invoke();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((_whatIsEnemy.value & (1 << collision.gameObject.layer)) > 0 && !calledOnStay)
        {
            if (enemyEnteredAttackArea != null)
            {
                enemyEnteredAttackArea.Invoke();
                calledOnStay = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) => calledOnStay = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _checkRadius);
    }
}
