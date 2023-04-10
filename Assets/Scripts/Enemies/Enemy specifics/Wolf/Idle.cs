using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States
{
    public class Idle : Enemies.States.Generics.Idle<Wolf>
    {
        private readonly Wolf wolf;

        private float idleTime;

        public Idle(Wolf entity, StateMachine<Wolf> stateMachine, Enemies.States.Generics.Data.D_Idle stateData, Wolf wolf) : base(entity, stateMachine, stateData)
        {
            this.wolf = wolf;
        }

        public override void Enter()
        {
            idleTime = Random.Range(stateData.MinIdleTime, stateData.MaxIdleTime);

            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time > startTime + idleTime)
            {
                stateMachine.ChangeState(wolf.MoveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
