using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States
{
    public class TargetDetected : WolfState
    {
        private readonly Data.D_TargetDetected stateData;

        public TargetDetected(Wolf entity, StateMachine<Wolf> stateMachine, Data.D_TargetDetected stateData) : base(entity, stateMachine)
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

            if (Time.time >= startTime + stateData.warnTime) 
            {
                if (wolf.FOV.Check()) stateMachine.ChangeState(wolf.ChaseState);
                else stateMachine.ChangeState(wolf.MoveState);
            }
        }
    }
}