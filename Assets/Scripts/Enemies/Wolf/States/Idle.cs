using StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Enemies.Wolf.States
{
    public class Idle : WolfState
    {
        private readonly Data.D_Idle stateData;
        private float idleTime;

        public Idle(Wolf entity, StateMachine<Wolf> stateMachine, Data.D_Idle stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            entity.SetVelocityX(0);
            idleTime = UnityEngine.Random.Range(stateData.MinIdleTime, stateData.MaxIdleTime);


            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (wolf.FOV.Check()) stateMachine.ChangeState(wolf.TargetDetectedState);
            else if (Time.time > startTime + idleTime) 
            {
                if (TryFlip()) wolf.Flip();
                stateMachine.ChangeState(wolf.MoveState); 
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (wolf.FOV.Check()) stateMachine.ChangeState(wolf.ChaseState);
        }

        private bool TryFlip() => UnityEngine.Random.value < stateData.flipChance;
    }
}
