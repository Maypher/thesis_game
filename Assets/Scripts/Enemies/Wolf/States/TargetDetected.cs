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

            wolf.SetAnimationParameter("warn", true);
            wolf.AudioSource.PlayOneShot(stateData.warnSFX);
        }

        public override void Exit()
        {
            base.Exit();

            wolf.SetAnimationParameter("warn", false);
            wolf.AudioSource.Stop();
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

            if (!wolf.FOV.Check()) stateMachine.ChangeState(wolf.MoveState);
            else if (Time.time >= startTime + stateData.warnTime) 
            {
                if (wolf.AttackRange.Check()) stateMachine.ChangeState(wolf.AssaultState);
                else stateMachine.ChangeState(wolf.ChaseState);
            }
        }
    }
}