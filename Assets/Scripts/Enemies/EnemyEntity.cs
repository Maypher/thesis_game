using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public abstract class EnemyEntity : StateMachine.Entity
    {
        [Header("Check variables")]
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private float ledgeCheckDistance;
        [SerializeField] private LayerMask whatIsGround;

        [Header("Check transforms")]
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Transform ledgeCheck;

        [Header("Components")]
        [SerializeField] private AttackCheck attackCheck;
        [SerializeField] private FieldOfView fov;
        
        public AttackCheck AttackCheck { get { return attackCheck; } }
        public FieldOfView FOV { get { return fov; } }

        public virtual bool CheckWall()
        {
            return Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        }

        public virtual bool CheckLedge()
        {
            return Physics2D.Raycast(ledgeCheck.position, Vector2.down, ledgeCheckDistance, whatIsGround);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;

            Gizmos.DrawLine(wallCheck.position, wallCheck.position + wallCheckDistance * FacingDirection * Vector3.right);
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + ledgeCheckDistance * Vector3.down);
        }
    }
}