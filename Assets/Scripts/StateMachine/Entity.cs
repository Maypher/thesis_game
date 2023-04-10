using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Base class for all scripts that use a State Machine
namespace StateMachine
{
    public class Entity : MonoBehaviour
    {
        #region Helper variables
        public int FacingDirection { get; private set; } = 1;
        // Instead of using new Vector2() every time it's needed we just set it here
        protected Vector2 velocityWorkspace;

        [Header("Check variables")]
        [SerializeField] private float wallCheckDistance;
        [SerializeField] private float ledgeCheckDistance;
        [SerializeField] private LayerMask whatIsGround;
        #endregion

        #region Components
        public Rigidbody2D Rb { get; private set; }
        public Animator Anim { get; private set; }

        [Header("Check transforms")]
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Transform ledgeCheck;
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

        public virtual bool CheckWall()
        {
            return Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        }

        public virtual bool CheckLedge()
        {
            return Physics2D.Raycast(ledgeCheck.position, Vector2.down, ledgeCheckDistance, whatIsGround);
        }

        public virtual void Flip()
        {
            FacingDirection *= -1;
            transform.Rotate(0, 180, 0);
        }

        public void TriggerAttack()
        {

        }

        /// <summary>
        /// Activates an animation trigger
        /// </summary>
        /// <param name="parameter">The name of the animation parameter</param>
        public void SetAnimationParameter(string parameter)
        {
            Anim.SetTrigger(parameter);
        }

        /// <summary>
        /// Sets an animation bool
        /// </summary>
        /// <param name="parameter">The name of the animation parameter</param>
        /// <param name="value">What to set parameter to</param>
        public void SetAnimationParameter(string parameter, bool value)
        {
            Anim.SetBool(parameter, value);
        }


        /// <summary>
        /// Sets an animation float
        /// </summary>
        /// <param name="parameter">The name of the animation parameter</param>
        /// <param name="value">The value of parameter</param>
        public void SetAnimationParameter(string parameter, float value)
        {
            Anim.SetFloat(parameter, value);
        }

        /// <summary>
        /// Sets an animation integer
        /// </summary>
        /// <param name="parameter">The name of the animation parameter</param>
        /// <param name="value">The value of parameter</param>
        public void SetAnimationParameter(string parameter, int value)
        {
            Anim.SetInteger(parameter, value);
        }



        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;

            Gizmos.DrawLine(wallCheck.position, wallCheck.position + wallCheckDistance * FacingDirection * Vector3.right);
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + ledgeCheckDistance * Vector3.down);
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