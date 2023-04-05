using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Base class for all scripts that use a State Machine
namespace StateMachine
{
    public class Entity : MonoBehaviour
    {
        #region Required classes
        // Let every entity have its own state machine with its own states
        #endregion

        #region Helper variables
        public int FacingDirection { get; private set; } = 1;
        // Instead of using new Vector2() every time it's needed we just set it here
        protected Vector2 velocityWorkspace;
        #endregion

        #region Components
        public Rigidbody2D Rb { get; private set; }
        public Animator Anim { get; private set; } 
        #endregion

        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

            Rb = GetComponent<Rigidbody2D>();
            Anim = GetComponent<Animator>();
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        // Set entity velocity based on facing direction and given velocity
        public virtual void SetVelocityX(float velocity)
        {
            velocityWorkspace.Set(FacingDirection * velocity, Rb.velocity.y);
            Rb.velocity = velocityWorkspace;
        }

        public virtual void SetVelocityY(float velocity)
        {
            velocityWorkspace.Set(Rb.velocity.x, velocity);
            Rb.velocity = velocityWorkspace;
        }

        public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);

            Rb.velocity = velocityWorkspace;
        }

 /*       public virtual bool CheckWall()
        {
            return Physics2D.Raycast(wallCheck.position, transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
        }

        public virtual bool CheckLedge()
        {
            return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
        }*/

        public virtual void Flip()
        {
            FacingDirection *= -1;
            transform.Rotate(0, 180, 0);
        }

        public void TriggerAttack()
        {

        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;

            // Gizmos.DrawLine(wallCheck.position, wallCheck.position + entityData.wallCheckDistance * FacingDirection * Vector3.right);
            // Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + entityData.ledgeCheckDistance * Vector3.down);
        }

        public virtual void DamageHop(float velocity) => SetVelocityY(velocity);

        /*public virtual void TakeDamage(AttackDetails attackDetails)
        {
            lastDamageTime = Time.time;
            currentStunResistance -= attackDetails.stunDamageAmount;

            isStunned = currentStunResistance <= 0;

            currentHealth -= attackDetails.damage;

            // Used for knocking back in the right direction when getting hit
            lastDamageDirection = attackDetails.attackPostion.x > transform.position.x ? -1 : 1;

            DamageHop(entityData.damageHopSpeed);
        }

        public virtual void Kill()
        {
            throw new NotImplementedException();
        }*/
    }
}