using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player 
{
    public class Player : StateMachine.Entity
    {

        public StateMachine.StateMachine<Player> StateMachine { get; private set; } = new();
        public UserInput UserInput { get; private set; }

        #region States data
        [Header("PlayerData")]
        [SerializeField] private  Substates.Data.D_Walk walkData;
        [SerializeField] private  Substates.Data.D_Idle idleData;
        #endregion

        #region States
        public Substates.Grounded.Walk WalkState { get; private set; }
        public Substates.Grounded.Idle IdleState { get; private set; }
        #endregion

        #region components
        public GroundCheck GroundCheck { get; private set; }
        public AttackCheck AttackCheck { get; private set; }
        #endregion

        public override void Awake()
        {
            base.Awake();

            WalkState = new(this, StateMachine, "walk", walkData);
            IdleState = new(this, StateMachine, "walk", idleData);
        }

        public override void Start()
        {
            base.Start();


            GroundCheck = GetComponentInChildren<GroundCheck>();
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
    }
}