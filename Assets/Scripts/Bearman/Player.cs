using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player 
{
    public class Player : StateMachine.Entity, IDamageable
    {

        public StateMachine.StateMachine<Player> StateMachine { get; private set; } = new();
        public UserInput UserInput { get; private set; }

        #region States data
        [Header("Player Grounded Data")]
        [SerializeField] private Substates.Data.D_Walk walkData;
        [SerializeField] private Substates.Data.D_Idle idleData;
        [SerializeField] private Substates.Data.D_PickUpRock pickUpRockData;
        [SerializeField] private Substates.Data.D_HoldRock HoldRockData;
        [SerializeField] private Substates.Data.D_ThrowRock ThrowRockData;
        [SerializeField] private Substates.Data.D_GroundPound groundpoundData;
        [SerializeField] private Substates.Data.D_AimRaccoon aimRacoonData;
        [SerializeField] private Substates.Data.D_ThrowRaccoon throwRacoonData;

        [Header("Player Airborne Data")]
        [SerializeField] private Substates.Data.D_Jump jumpData;
        [SerializeField] private Substates.Data.D_AirMove airMoveData;
        [SerializeField] private Substates.Data.D_Dash DashData;
        #endregion

        #region Global variables
        [Header("Player variables")]
        public float jumpBufferTime = .4f;
        [Min(0)] [SerializeField] private int maxHealth;
        public int Health { get; private set; }
        #endregion

        #region States
        public Substates.Grounded.Walk WalkState { get; private set; }
        public Substates.Grounded.Idle IdleState { get; private set; }
        public Substates.Grounded.PickUpRock PickUpRockState { get; private set; }
        public Substates.Grounded.HoldRock HoldRockState { get; private set; }
        public Substates.Grounded.ThrowRock ThrowRockState { get; private set; }
        public Substates.Grounded.GrabRaccoon GrabRacoonState { get; private set; }
        public Substates.Grounded.AimRaccoon AimRaccoonState { get; private set; }
        public Substates.Grounded.ThrowRaccoon ThrowRaccoonState { get; private set; }
        public Substates.Grounded.CallBackRaccoon CallBackRaccoonState { get; private set; }
        public Substates.Airborne.Jump JumpState { get; private set; }
        public Substates.Airborne.AirMove AirMoveState { get; private set; }
        public Substates.Airborne.Dash DashState { get; private set; }
        public Substates.Airborne.GroundPound GroundpoundState { get; private set; }
        public Substates.Airborne.Damage DamageState { get; private set; }
        #endregion

        #region components
        public AttackCheck AttackCheck { get; private set; }
        #endregion

        #region control variables
        [HideInInspector] public bool CanJump = true;
        [HideInInspector] public bool CanLand = true;
        [HideInInspector] public float TimeInAir = 0f;
        [HideInInspector] public bool CanBeDamaged = true;
        public Action PlayerDeath;
        #endregion

        #region external references
        [HideInInspector] public GameObject Rock;
        [HideInInspector] public Raccoon.Raccoon Raccoon;
        #endregion

        public override void Awake()
        {
            base.Awake();

            WalkState = new(this, StateMachine, walkData);
            IdleState = new(this, StateMachine, idleData);
            PickUpRockState = new(this, StateMachine, pickUpRockData);
            HoldRockState = new(this, StateMachine, HoldRockData);
            ThrowRockState = new(this, StateMachine, ThrowRockData);
            GrabRacoonState = new(this, StateMachine);
            AimRaccoonState = new(this, StateMachine, aimRacoonData);
            ThrowRaccoonState = new(this, StateMachine, throwRacoonData);
            CallBackRaccoonState = new(this, StateMachine);

            JumpState = new(this, StateMachine, jumpData);
            AirMoveState = new(this, StateMachine, airMoveData);
            DashState = new(this, StateMachine, DashData);
            GroundpoundState = new(this, StateMachine, groundpoundData);
            DamageState = new(this, StateMachine);
        }

        public override void Start()
        {
            base.Start();

            Health = maxHealth;
            AttackCheck = GetComponentInChildren<AttackCheck>();

            
            UserInput = new();
            UserInput.Player.Enable();

            StateMachine.Initialize(IdleState);
        }

        public override void Update()
        {
            base.Update();
            (StateMachine.CurrentState as PlayerState).Input();
            StateMachine.CurrentState.LogicUpdate();
            StateMachine.CurrentState.CheckStateChange();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            StateMachine.CurrentState.PhysicsUpdate();
        }

        public void TakeDamage(AttackDetails attackDetails)
        {
            if (!CanBeDamaged) return;

            Health -= attackDetails.damage;

            SetVelocity(attackDetails.knockbackForce.magnitude, attackDetails.knockbackForce, Mathf.Sign(attackDetails.attackPostion.x - transform.position.x));

            StateMachine.ChangeState(DamageState);

            if (Health <= 0) Kill();
        }

        public void Kill()
        {
            PlayerDeath?.Invoke();
            Debug.Log("killed");
        }
    }
}