using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Wolf.States
{
    public class Move : Enemies.States.Generics.Move<Wolf>
    {
        public Move(Wolf entity, StateMachine<Wolf> stateMachine, string animBoolName, Enemies.States.Generics.Data.D_Move stateData) : base(entity, stateMachine, animBoolName, stateData)
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