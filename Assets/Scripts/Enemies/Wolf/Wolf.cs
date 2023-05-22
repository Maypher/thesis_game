using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf
{
    public class Wolf : EnemyEntity
    {
        public StateMachine.StateMachine<Wolf> StateMachine { get; private set; } = new StateMachine.StateMachine<Wolf>();

        [Header("Wolf data")]
        [SerializeField] private int maxHealth;
        public int Health { get; private set; }

        #region State Data
        [Header("State Data")]
        [SerializeField] private States.Data.D_Move moveData;
        [SerializeField] private States.Data.D_Idle idleData;
        [SerializeField] private States.Data.D_Chase chaseData;
        [SerializeField] private States.Data.D_TargetDetected targetDetectedData;
        [SerializeField] private States.Data.D_Tired tiredData;
        [SerializeField] private States.Data.D_Assault assaultData;
        [SerializeField] private States.Data.D_LookForTarget lookForTargetData;
        [SerializeField] private States.Data.D_Attack attackData;
        #endregion

        public States.Move MoveState { get; private set; }
        public States.Idle IdleState { get; private set; }
        public States.TargetDetected TargetDetectedState { get; private set; }
        public States.Chase ChaseState { get; private set; }
        public States.Tired TiredState { get; private set; }
        public States.Assault AssaultState { get; private set; }
        public States.LookForTarget LookForTargetState { get; private set; }
        public States.Attack AttackState { get; private set; }

        [field: SerializeField] [field: Header("Wolf Components")] public AttackCheck AttackRange { get; private set; }
        [field: SerializeField] public Transform WolfHead { get; private set; }

        [Header("On collision damage data")]
        [SerializeField] [Tooltip("Attack details for when the player enters in contact with the enemy")] private AttackDetails onCollisionDetails;

        [HideInInspector] public bool CanAttack = true;

        public override void Awake()
        {
            MoveState = new(this, StateMachine, moveData);
            IdleState = new(this, StateMachine, idleData);
            ChaseState = new(this, StateMachine, chaseData);
            TargetDetectedState = new(this, StateMachine, targetDetectedData);
            TiredState = new(this, StateMachine, tiredData);
            AssaultState = new(this, StateMachine, assaultData);
            LookForTargetState = new(this, StateMachine, lookForTargetData);
            AttackState = new(this, StateMachine, attackData);
        }

        public override void Start()
        {
            base.Start();

            StateMachine.Initialize(MoveState);
        }

        public override void Update()
        {
            base.Update();

            StateMachine.CurrentState.LogicUpdate();
            StateMachine.CurrentState.CheckStateChange();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            StateMachine.CurrentState.PhysicsUpdate();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            onCollisionDetails.attackPostion = transform.position;

            if (collision.gameObject.CompareTag("Player")) collision.gameObject.GetComponent<IDamageable>()?.TakeDamage(onCollisionDetails);
        }

        public override void TakeDamage(AttackDetails attackDetails)
        {
            base.TakeDamage(attackDetails);

            Health -= attackDetails.damage;

            SetVelocity(attackDetails.knockbackForce, attackDetails.knockbackAngle, Mathf.Sign(transform.position.x - attackDetails.attackPostion.x));

            if (Health <= 0) Kill();
        }
    }
}