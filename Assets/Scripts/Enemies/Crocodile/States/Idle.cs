using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Crocodile.States
{
    public class Idle : CrocodileState
    {
        private bool targetOnTop;

        public Idle(Crocodile entity, StateMachine<Crocodile> stateMachine) : base(entity, stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();

            targetOnTop = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            targetOnTop = crocodile.AttackCheck.Check();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (targetOnTop) stateMachine.ChangeState(crocodile.BiteState);
        }


        public override void Exit()
        {
            base.Exit();
        }
    }
}