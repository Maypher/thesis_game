using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States
{
    public class Move : Enemies.States.Generics.Move<Wolf>
    {
        private readonly Wolf wolf;
        private float walkTime;

        public Move(Wolf entity, StateMachine<Wolf> stateMachine, Enemies.States.Generics.Data.D_Move stateData, Wolf wolf) : base(entity, stateMachine, stateData)
        {
            this.wolf = wolf;
        }

        public override void Enter()
        {
            base.Enter();

            walkTime = Random.Range(stateData.minWalkTime, stateData.maxWalkTime);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= startTime + walkTime)
            {
                stateMachine.ChangeState(wolf.IdleState);
            }
            else if (entity.CheckWall() || !entity.CheckLedge())
            {
                entity.Flip();
                entity.SetVelocityX(stateData.moveSpeed);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}