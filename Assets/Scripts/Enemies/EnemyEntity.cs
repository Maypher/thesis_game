using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyEntity : StateMachine.Entity
    {
        [Header("Check variables")]
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private float ledgeCheckDistance;
        [SerializeField] private LayerMask whatIsGround;

        [Header("Check transforms")]
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Transform ledgeCheck;

        public virtual bool CheckWall()
        {
            return Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        }

        public virtual bool CheckLedge()
        {
            return Physics2D.Raycast(ledgeCheck.position, Vector2.down, ledgeCheckDistance, whatIsGround);
        }
    }
}