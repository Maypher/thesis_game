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
        [SerializeField] private Enemies.States.Generics.Data.D_Move moveData;
        [SerializeField] private Enemies.States.Generics.Data.D_Idle IdleData;
        #endregion

        public States.Move MoveState { get; private set; }
        public States.Idle IdleState { get; private set; }

        public override void Awake()
        {
            MoveState = new(this, StateMachine, moveData, this);
            IdleState = new(this, StateMachine, IdleData, this);
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
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            StateMachine.CurrentState.PhysicsUpdate();
        }


    }
}