using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.States.Generics
{
    public class Idle<T> : State<T> where T: Entity
    {
        protected  Data.D_Idle stateData;

        public Idle(T entity, StateMachine<T> stateMachine, Data.D_Idle stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            entity.SetVelocityX(0);
            base.Enter();
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
    }
}