using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf
{
    public class Wolf : EnemyEntity
    {
        public StateMachine.StateMachine<Wolf> StateMachine { get; private set; } = new StateMachine.StateMachine<Wolf>();

        #region State Data
        [Header("State Data")]
        [SerializeField] private States.Data.D_Move moveData;
        [SerializeField] private States.Data.D_Idle idleData;
        [SerializeField] private States.Data.D_Chase chaseData;
        [SerializeField] private States.Data.D_TargetDetected targetDetectedData;
        [SerializeField] private States.Data.D_Tired tiredData;
        [SerializeField] private States.Data.D_Assault assaultData;
        [SerializeField] private States.Data.D_LookForTarget lookForTargetData;
        #endregion

        public States.Move MoveState { get; private set; }
        public States.Idle IdleState { get; private set; }
        public States.TargetDetected TargetDetectedState { get; private set; }
        public States.Chase ChaseState { get; private set; }
        public States.Tired TiredState { get; private set; }
        public States.Assault AssaultState { get; private set; }
        public States.LookForTarget LookForTargetState { get; private set; }

        [field: SerializeField] [field: Header("Wolf Components")] public AttackCheck AttackRange { get; private set; }

        public override void Awake()
        {
            MoveState = new(this, StateMachine, moveData);
            IdleState = new(this, StateMachine, idleData);
            ChaseState = new(this, StateMachine, chaseData);
            TargetDetectedState = new(this, StateMachine, targetDetectedData);
            TiredState = new(this, StateMachine, tiredData);
            AssaultState = new(this, StateMachine, assaultData);
            LookForTargetState = new(this, StateMachine, lookForTargetData);
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
    }
}