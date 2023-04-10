using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.States.Generics
{
    public class Move<T> : State<T> where T : Entity
    {
        protected readonly Data.D_Move stateData;

        public Move(T entity, StateMachine<T> stateMachine, Data.D_Move stateData) : base(entity, stateMachine)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            entity.SetVelocityX(stateData.moveSpeed);
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
