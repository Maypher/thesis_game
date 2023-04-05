using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf
{
    public class Wolf : StateMachine.Entity
    {
        public StateMachine.StateMachine<Wolf> StateMachine { get; private set; } = new StateMachine.StateMachine<Wolf>();

        #region State Data
        [SerializeField] private Enemies.States.Generics.Data.D_Move moveData;
        #endregion

        public States.Move MoveState { get; private set; }

        public override void Awake()
        {
            MoveState = new(this, StateMachine, "move", moveData);
        }

        public override void Start()
        {
            base.Start();

            StateMachine.Initialize(MoveState);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }


        public override void Update()
        {
            base.Update();
        }
    }
}