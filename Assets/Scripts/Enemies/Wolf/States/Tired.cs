using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States
{
    public class Tired : WolfState
    {
        private readonly Data.D_Tired stateData;
        public Tired(Wolf entity, StateMachine<Wolf> stateMachine, Data.D_Tired stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            wolf.SetVelocityX(0);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (Time.time >= startTime + stateData.tiredTime) stateMachine.ChangeState(wolf.MoveState);
        }
    }
}
