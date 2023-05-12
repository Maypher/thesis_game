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

        public Action AnimationEvent;
        public Action FinishAnimation;
        #endregion

        #region Components
        public Rigidbody2D Rb { get; private set; }
        public Animator Anim { get; private set; }
        public GroundCheck GroundCheck { get; private set; }
        #endregion

        public virtual void Awake()
        {

        }

        public virtual void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            Anim = GetComponent<Animator>();
            GroundCheck = GetComponentInChildren<GroundCheck>();

        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        /// <summary>
        /// Set entity velocity based on facing direction and given velocity
        /// </summary>
        /// <param name="velocity">The velocity to set the rigidbody to</param>
        /// <param name="direction">By default the function will use the facing direction but this parameter will overwrite that</param>
        public virtual void SetVelocityX(float velocity, bool ignoreDirection = false)
        {
            if (ignoreDirection) velocityWorkspace.Set(velocity, Rb.velocity.y);
            else velocityWorkspace.Set(FacingDirection * velocity, Rb.velocity.y);
            Rb.velocity = velocityWorkspace;
        }

        public virtual void SetVelocityY(float velocity)
        {
            velocityWorkspace.Set(Rb.velocity.x, velocity);
            Rb.velocity = velocityWorkspace;
        }

        public virtual void SetVelocity(float velocity, Vector2 angle, float direction)
        {
            angle.Normalize();
            velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);

            Rb.velocity = velocityWorkspace;
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

        public void CallAnimationEvent() => AnimationEvent?.Invoke();
        public void CallFinishAnimation() => FinishAnimation?.Invoke();
    }
}