using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace Bearman
{
    // Make the controller a state machine and pass itself as a reference 
    public class BearmanCtrl : StateMachine<BearmanCtrl>, IDamageable
    {
        public const string Flex = "flex";
        public const string IsMoving = "isMoving";
        public const string IsCrouching = "isCrouching";
        public const string IsAirborne = "isAirborne";
        public const string HoldingRock = "holdingRock";
        public const string PickUpRock = "pickUpRock";
        public const string ReciveDamage = "takeDamage";
        public const string IsCharging = "isCharging";
        public const string Attack = "attack";
        public const string ChargedAttack = "chargedAttack";
        public const string Aiming = "aiming";
        public const string Shockwave = "shockwave";

        [HideInInspector] public AnimationHandler AnimationHandler;

        [Header("Input")]
        [HideInInspector] public UserInput UserInput;

        [Header("Health")]
        [SerializeField, Min(1)] private int maxHealth = 40;
        private int _health;

        [Header("Airborne state")]
        [HideInInspector] public bool Jumped = false;

        [Header("Punch charge state")]
        [HideInInspector] public float ChargeTime;

        [Header("Ground check")]
        private GroundCheck _groundCheck;
        [HideInInspector]
        public bool IsGrounded
        {
            get { return _groundCheck.Check(); }
        }

        [HideInInspector] public Action AnimationEvent;
        [HideInInspector] public Action FinishAnimationEvent;

        protected override void Awake()
        {
            base.Awake();

            AnimationHandler = GetComponent<AnimationHandler>();
            _groundCheck = transform.Find("GroundCheck").GetComponent<GroundCheck>();

            UserInput = new UserInput();
            UserInput.Player.Enable();
        }

        private void Start() => _health = maxHealth;

        [HideInInspector]
        public bool TakeDamage(int damagePt)
        {
            SetState(typeof(States.Damage));

            _health -= damagePt;
            if (_health <= 0)
            {
                Kill();
                return true;
            }

            return false;
        }

        [HideInInspector]
        public void Kill()
        {
            Destroy(this.gameObject);
        }

        public void CallAnimationEvent() => AnimationEvent?.Invoke();

        public void CallFinishAnimationEvent() => FinishAnimationEvent?.Invoke();
    }
}