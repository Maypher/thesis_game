using Player.Raccoon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public abstract class EnemyEntity : StateMachine.Entity, IAttack, IDamageable
    {
        [field: SerializeField] [field: Header("Check transforms")] public Transform WallCheck { get; private set; }
        [field: SerializeField] public Transform LedgeCheck { get; private set; }

        [Header("Check variables")]
        [SerializeField] private LayerMask whatIsGround;
        [field: SerializeField] public float WallCheckDistance { get; private set; }
        [field: SerializeField] public float LedgeCheckDistance { get; private set; }

        [field: SerializeField] [field: Header("Components", order = 1)] public AttackCheck AttackCheck { get; private set; }
        [field: SerializeField] public FieldOfView FOV { get; private set; }

        [Header("Particle systems")]
        [SerializeField] protected ParticleSystem damagePS;
        [SerializeField] protected ParticleSystem deathPS;

        public virtual bool CheckWall() => Physics2D.Raycast(WallCheck.position, transform.right, WallCheckDistance, whatIsGround);

        public virtual bool CheckLedge() => Physics2D.Raycast(LedgeCheck.position, Vector2.down, LedgeCheckDistance, whatIsGround);

        public int DirectionToTarget(GameObject target) => (int) Mathf.Sign((target.transform.localPosition - transform.position).x);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;

            Gizmos.DrawLine(WallCheck.position, WallCheck.position + WallCheckDistance * FacingDirection * Vector3.right);
            Gizmos.DrawLine(LedgeCheck.position, LedgeCheck.position + LedgeCheckDistance * Vector3.down);
        }

        public virtual void Attack()
        {
            throw new System.NotImplementedException();
        }

        public virtual void FinishAttack()
        {
            throw new System.NotImplementedException();
        }

        public virtual void TakeDamage(AttackDetails attackDetails)
        {
            damagePS.Play();
        }

        public virtual void Kill()
        {
            deathPS.transform.parent = null;
            deathPS.Play();
            Destroy(gameObject);
        }
    }
}