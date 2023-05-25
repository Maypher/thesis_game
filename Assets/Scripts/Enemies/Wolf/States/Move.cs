using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States
{
    public class Move : WolfState
    {
        private readonly Data.D_Move stateData;

        private float walkTime;

        public Move(Wolf entity, StateMachine<Wolf> stateMachine, Data.D_Move stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            walkTime = Random.Range(stateData.minWalkTime, stateData.maxWalkTime);

            entity.SetVelocityX(stateData.moveSpeed);
            wolf.AudioSource.loop = true;
            wolf.AudioSource.clip = stateData.pantingSFX;
            wolf.AudioSource.Play();
        }

        public override void Exit()
        {
            base.Exit();
            wolf.AudioSource.loop = false;
            wolf.AudioSource.Stop();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (wolf.CheckWall() || !wolf.CheckLedge())
            {
                entity.Flip();
                entity.SetVelocityX(stateData.moveSpeed);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void CheckStateChange()
        {
            base.CheckStateChange();

            if (wolf.FOV.Check()) stateMachine.ChangeState(wolf.TargetDetectedState);
            else if (Time.time >= startTime + walkTime) stateMachine.ChangeState(wolf.IdleState);
        }
    }
}