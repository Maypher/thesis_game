using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.States.Generics
{
    public class Move<T> : State<T> where T : Entity
    {
        public Move(T entity, StateMachine<T> stateMachine, string animBoolName, StateData stateData) : base(entity, stateMachine, animBoolName, stateData)
        {
        }

        public override void Enter()
        {
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
