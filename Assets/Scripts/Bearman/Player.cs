using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Player 
{
    public class Player : StateMachine.Entity, IDamageable
    {


        #region Global variables
        public StateMachine.StateMachine<Player> StateMachine { get; private set; } = new();
       
        [Header("Player variables")]
        public float jumpBufferTime = .4f;
        [field: SerializeField, Min(0)] public int MaxHealth { get; private set; }
        public int Health { get; private set; }
        #endregion

        #region Events
        public Action DamageTaken;
        public Action PlayerDeath;
        #endregion

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
        [SerializeField] private Substates.Data.D_Punch punchData;

        [Header("Player Airborne Data")]
        [SerializeField] private Substates.Data.D_Jump jumpData;
        [SerializeField] private Substates.Data.D_AirMove airMoveData;
        [SerializeField] private Substates.Data.D_Dash dashData;
        [SerializeField] private Substates.Data.D_Damage damageData;
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
        public Substates.Grounded.Punch PunchState { get; private set; }
        public Substates.Grounded.Crouch CrouchState { get; private set; }
        public Substates.Airborne.Jump JumpState { get; private set; }
        public Substates.Airborne.AirMove AirMoveState { get; private set; }
        public Substates.Airborne.Dash DashState { get; private set; }
        public Substates.Airborne.GroundPound GroundpoundState { get; private set; }
        public Substates.Airborne.Damage DamageState { get; private set; }
        #endregion

        #region components
        public AttackCheck AttackCheck { get; private set; }
        public AttackCheck DashAttackCheck { get; private set; }
        public AttackCheck GroundPoundCheck { get; private set; }

        public CinemachineImpulseSource CameraImpulseSource { get; private set; }
        #endregion

        #region control variables
        [HideInInspector] public bool CanJump = true;
        [HideInInspector] public bool CanLand = true;
        [HideInInspector] public bool CanBeDamaged = true;
        #endregion

        #region external references
        [HideInInspector] public GameObject Rock;
        [HideInInspector] public Raccoon.Raccoon Raccoon { get { return raccoon; } set { raccoon = value; DamageTaken?.Invoke(); } }
        [HideInInspector] private Raccoon.Raccoon raccoon;

        public SpriteRenderer SpriteRenderer { get; private set; }
        #endregion


        #region Particles
        [Header("Particles")]
        [SerializeField] private ParticleSystem groundpoundParticles;

        public ParticleSystem GroundPoundParticles { get { return groundpoundParticles; } }
        #endregion

        public override void Awake()
        {
            base.Awake();

            // Instantiated in awake because it needs to be ready for when the UI fetches it
            Health = MaxHealth;

            WalkState = new(this, StateMachine, walkData);
            IdleState = new(this, StateMachine, idleData);
            PickUpRockState = new(this, StateMachine, pickUpRockData);
            HoldRockState = new(this, StateMachine, HoldRockData);
            ThrowRockState = new(this, StateMachine, ThrowRockData);
            GrabRacoonState = new(this, StateMachine);
            AimRaccoonState = new(this, StateMachine, aimRacoonData);
            ThrowRaccoonState = new(this, StateMachine, throwRacoonData);
            CallBackRaccoonState = new(this, StateMachine);
            PunchState = new(this, StateMachine, punchData);
            CrouchState = new(this, StateMachine);

            JumpState = new(this, StateMachine, jumpData);
            AirMoveState = new(this, StateMachine, airMoveData);
            DashState = new(this, StateMachine, dashData);
            GroundpoundState = new(this, StateMachine, groundpoundData);
            DamageState = new(this, StateMachine, damageData);
        }

        public override void Start()
        {
            base.Start();

            AttackCheck = transform.Find("PunchCheck").GetComponent<AttackCheck>();
            DashAttackCheck = transform.Find("DashAttackCheck").GetComponent<AttackCheck>();
            GroundPoundCheck = transform.Find("GroundpoundArea").GetComponent<AttackCheck>();
            SpriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            CameraImpulseSource = GetComponent<CinemachineImpulseSource>();

            GameManager.UserInput.Player.Enable();

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

            SetVelocity(attackDetails.knockbackForce, attackDetails.knockbackAngle, Mathf.Sign(transform.position.x - attackDetails.attackPostion.x));

            StateMachine.ChangeState(DamageState);

            DamageTaken?.Invoke();

            if (Health <= 0) Kill();
        }

        public void Kill()
        {
            PlayerDeath?.Invoke();
            Debug.Log("killed");
        }
    }
}