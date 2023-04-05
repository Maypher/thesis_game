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
        [SerializeField] private  Substates.Data.D_Walk stateData;
        #endregion

        #region States
        protected Substates.Grounded.Walk walkState;
        #endregion

        #region components
        public GroundCheck GroundCheck { get; private set; }
        public AttackCheck AttackCheck { get; private set; }
        #endregion

        public override void Awake()
        {
            base.Awake();

            walkState = new(this, StateMachine, "walk", stateData);
        }

        public override void Start()
        {
            base.Start();


            GroundCheck = GetComponentInChildren<GroundCheck>();
            AttackCheck = GetComponentInChildren<AttackCheck>();

            UserInput = new();
            UserInput.Player.Enable();

            StateMachine.Initialize(walkState);
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